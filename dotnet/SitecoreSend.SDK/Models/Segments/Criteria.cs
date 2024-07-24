namespace SitecoreSend.SDK;

public class Criteria
{
    public int ID { get; set; }
    public int SegmentID { get; set; }
    public int Field { get; set; }
    public Guid? CustomFieldID { get; set; }
    public ComparerCriteria Comparer { get; set; }
    public string? Value { get; set; }
    public string? DateFrom { get; set; }
    public string? DateTo { get; set; }
    public object? Properties { get; set; }
    public object? Subscriteria { get; set; }
}