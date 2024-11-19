using SitecoreSend.SDK.Tools;
using SitecoreSend.SDK.Transactional;
using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests;

public class TransactionalEmailTest(ITestOutputHelper testOutputHelper)
{
    private readonly ISendClient _send = TestsApp.SendFactory(testOutputHelper);
    [Fact]
    public async Task Send_OnInvalidRequest_ShouldReturnError()
    {
        using (new ClientSwitcher("Transactional"))
        {
            var request = EmailRequestBuilder.StartWithCampaign(Guid.Empty)
                .AddPersonalization(new Personalization("test@mail.com"))
                .Build();
            var result = await _send.Transactional.Send(request);
            Assert.False(result?.Success);

            request = EmailRequestBuilder.StartWithCampaign(Guid.NewGuid())
                .AddPersonalization(new Personalization("test@mail.com"))
                .Build();

            result = await _send.Transactional.Send(request);
            Assert.False(result?.Success);

            request = EmailRequestBuilder.StartWithCampaign(Guid.NewGuid())
                .AddPersonalization(new Personalization("test@mail.com"))
                .AddContent(new EmailContent() {Type = "text/html", WebLocation = "https://google.com"})
                .AddContent(new EmailContent() {Type = "text/html", WebLocation = "https://google.com"})
                .Build();

            result = await _send.Transactional.Send(request);
            Assert.False(result?.Success);
        }
    }

    [Fact]
    public async Task Send_OnDefaultTemplate_ShouldSendEmail()
    {
        using (new ClientSwitcher("Transactional"))
        {
            var campaign = Guid.Parse(TestsApp.Configuration.GetSection("SitecoreSend:TransactionalCampaignId").Value!);
            var testEmail = TestsApp.Configuration.GetSection("SitecoreSend:TestEmail").Value!;

            var request = EmailRequestBuilder.StartWithCampaign(campaign)
                .AddPersonalization(new Personalization(testEmail, "Igor Zharikov")
                {
                    Substitutions = new Dictionary<string, string>()
                    {
                        {"orderNumber", "123456"},
                        {"paymentMethod", "PayTest"},
                        {"total", "123.00 USD"},
                    },
                })
                .AddAttachment(new EmailAttachment()
                {
                    Content = AttachmentTool.StreamToBase64(File.OpenRead("icon.png")),
                    Type = "image/png",
                    FileName = "icon.png",
                })
                .Build();

            var result = await _send.Transactional.Send(request);

            Assert.NotNull(result?.Data);
            Assert.True(result.Success);
            Assert.True(result.Data.TotalAccepted > 0);
        }
    }
}