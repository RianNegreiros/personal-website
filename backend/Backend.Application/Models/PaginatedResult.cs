namespace Backend.Application.Models;

public class PaginatedResult<T>
{
  public IEnumerable<T> Items { get; set; } = new List<T>();
  public int TotalCount { get; set; }
  public int CurrentPage { get; set; }
  public int PageSize { get; set; }
  public bool HasNextPage => (CurrentPage * PageSize) < TotalCount;
}

