using SitecoreSend.SDK.Tests.Http;
using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests;

public class MailingListServiceTests(ITestOutputHelper testOutputHelper)
{

    private readonly IMailingListService _service = new MailingListService(TestsApp.ApiConfiguration, CustomHttpFactory.Create(testOutputHelper));

    [Fact]
    public async Task MailingList_OnValidList_CreatesList()
    {
        var result = await _service.CreateMailingList(new MailingListRequest
        {
            Name = "Test Name",
        });
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Data);
        var getListResult = await _service.GetMailingList(result.Data);
        Assert.NotNull(getListResult?.Data);
        Assert.Equal(result.Data, getListResult.Data.ID);
        Assert.Equal("Test Name", getListResult.Data.Name);

        var updateListResult = await _service.UpdateMailingList(getListResult.Data.ID, new MailingListRequest()
        {
            Name = "Test Name 2",
            ConfirmationPage = "http://localhost/confirm",
            RedirectAfterUnsubscribePage = "http://localhost/redirect",
        });

        Assert.NotNull(updateListResult);
        Assert.NotEqual(Guid.Empty, updateListResult.Data);
        Assert.Equal(getListResult.Data.ID, updateListResult.Data);

        getListResult = await _service.GetMailingList<MailingList>(updateListResult.Data);
        Assert.NotNull(getListResult?.Data);
        Assert.Equal(result.Data, getListResult.Data.ID);
        Assert.Equal("Test Name 2", getListResult.Data.Name);

        // cleanup
        await DeleteListAndAssertDeleted(result.Data);
    }

    [Fact]
    public async Task CustomFields_OnCreateUpdateDelete_ShouldPerformOperations()
    {
        var id = Guid.Parse(TestsApp.Configuration.GetSection("SitecoreSend:TestListId").Value!);
        var listResult = await _service.GetMailingList(id);
        Assert.NotNull(listResult?.Data);

        // Create a few custom fields
        var createTextField = await _service.CreateCustomField(id, new CustomFieldDefinitionRequest()
        {
            Name = "TextField",
            CustomFieldType = CustomFieldType.Text,
        });
        Assert.NotNull(createTextField);
        Assert.NotEqual(Guid.Empty, createTextField.Data);
            
        var createDropdownField = await _service.CreateCustomField(id, new CustomFieldDefinitionRequest()
        {
            Name = "DropDownField",
            CustomFieldType = CustomFieldType.SingleSelectDropdown,
            Options = ["Option1", "Option2"],
        });
        Assert.NotNull(createDropdownField);
        Assert.NotEqual(Guid.Empty, createDropdownField.Data);
            
        // get list and validate data in created fields
        listResult = await _service.GetMailingList(id);
        Assert.NotNull(listResult?.Data?.CustomFieldsDefinition);
            
        var textField =
            listResult.Data.CustomFieldsDefinition.FirstOrDefault(x => x.ID == createTextField.Data);
        Assert.NotNull(textField);
        Assert.Equal("TextField", textField.Name);
        Assert.Equal(CustomFieldType.Text, textField.Type);
            
        var dropdownField =
            listResult.Data.CustomFieldsDefinition.FirstOrDefault(x => x.ID == createDropdownField.Data);
        Assert.NotNull(dropdownField);
        Assert.Equal("DropDownField", dropdownField.Name);
        Assert.Equal(CustomFieldType.SingleSelectDropdown, dropdownField.Type);
        Assert.Equal(["Option1", "Option2"], dropdownField.Options);
            
        // update field and validate
        var updateTextField = _service.UpdateCustomField(id, textField.ID, new CustomFieldDefinitionRequest()
        {
            Name = "TextField2",
        });
        Assert.NotNull(updateTextField);
            
        listResult = await _service.GetMailingList(id);
        Assert.NotNull(listResult?.Data?.CustomFieldsDefinition);
            
        textField =
            listResult.Data.CustomFieldsDefinition.FirstOrDefault(x => x.ID == textField.ID);
        Assert.NotNull(textField);
        Assert.Equal("TextField2", textField.Name);
            
        // delete and validate
        await _service.RemoveCustomField(id, textField.ID);
        await _service.RemoveCustomField(id, dropdownField.ID);
            
        listResult = await _service.GetMailingList(id);
        Assert.NotNull(listResult?.Data);
        Assert.Null(listResult.Data.CustomFieldsDefinition.FirstOrDefault(x => x.ID == textField.ID));
        Assert.Null(listResult.Data.CustomFieldsDefinition.FirstOrDefault(x => x.ID == dropdownField.ID));
    }

    private async Task DeleteListAndAssertDeleted(Guid id)
    {
        var removeList = await _service.DeleteMailingList(id);
        Assert.True(removeList?.Success);
        var getListAfterDeleteResult = await _service.GetMailingList(id);
        Assert.NotNull(getListAfterDeleteResult);
        Assert.Null(getListAfterDeleteResult.Data);
        Assert.Equal(KnownErrors.LIST_NOT_FOUND, getListAfterDeleteResult.Error);
    }

    [Fact]
    public async Task GetAllMailingLists_OnValidApiKey_ShouldReturnLists()
    {
        var lists = await _service.GetAllMailingLists();
    }
}