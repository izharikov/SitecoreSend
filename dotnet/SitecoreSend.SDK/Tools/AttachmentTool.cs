using SitecoreSend.SDK.Transactional;

namespace SitecoreSend.SDK.Tools;

public static class AttachmentTool
{
    public static string StreamToBase64(Stream stream)
    {
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        var bytes = memoryStream.ToArray();
        return Convert.ToBase64String(bytes);
    }
}