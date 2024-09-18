namespace SitecoreSend.SDK;

public class ClientSwitcher : IDisposable
{
    private static readonly AsyncLocal<string> _value = new();
    private readonly string _originalValue;
    
    public ClientSwitcher(string value)
    {
        _originalValue = _value.Value ?? string.Empty;
        _value.Value = value;
    }

    public static string? Current => _value.Value;

    public void Dispose()
    {
        _value.Value = _originalValue;
    }
}