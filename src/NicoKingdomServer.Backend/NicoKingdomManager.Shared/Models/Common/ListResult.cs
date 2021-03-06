namespace NicoKingdomManager.Shared.Models.Common;

public sealed class ListResult<T> where T : BaseObject
{
    public ListResult(IEnumerable<T> content)
    {
        Content = content;
        TotalCount = content?.Count() ?? 0;
        HasNextPage = false;
    }
    public ListResult(IEnumerable<T> content, int totalCount, bool hasNextPage = false)
    {
        Content = content;
        TotalCount = totalCount;
        HasNextPage = hasNextPage;
    }

    public IEnumerable<T> Content { get; }
    public int TotalCount { get; }
    public bool HasNextPage { get; }
}