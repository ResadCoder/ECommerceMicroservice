namespace Shared.Pagination;

public class PaginatedResult<TEntity>
    where TEntity : class
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public IEnumerable<TEntity> Data { get; set; }

    public PaginatedResult() { } // Mapster can use this

    public PaginatedResult(int pageNumber, int pageSize, int count, IEnumerable<TEntity> data)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }
}