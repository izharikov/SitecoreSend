namespace SitecoreSend.SDK.Transactional;

public class Personalization
{
    public Personalization()
    {
        To = new List<Recipient>();
    }

    public Personalization(Recipient recipient)
    {
        To = new List<Recipient> {recipient};
    }

    public Personalization(string email, string? name = null)
    {
        To = new List<Recipient>
        {
            new Recipient()
            {
                Email = email,
                Name = name,
            },
        };
    }

    public List<Recipient> To { get; set; }
    public Dictionary<string, string> Substitutions { get; set; } = new();
}