namespace SitecoreSend.SDK
{
    public class Paging
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalResults { get; set; }
        public int TotalPageCount { get; set; }
        public object SortExpression { get; set; }
        public bool SortIsAscending { get; set; }
    }
}