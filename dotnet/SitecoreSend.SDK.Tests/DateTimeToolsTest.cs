using SitecoreSend.SDK.Tools;

namespace SitecoreSend.SDK.Tests;

public class DateTimeToolsTest
{
    [Theory]
    [InlineData("/Date(1698865249697+0000)/")]
    [InlineData("/Date(1698865249697)/")]
    [InlineData("/Date(1712681004661)/")]
    [InlineData("/Date(1712681004661+0000)/")]
    public void DateTimeTools_Parse_ShouldParse(string input)
    {
        var date = DateTimeTools.Parse(input);
        Assert.NotEqual(date, default);
    }
}