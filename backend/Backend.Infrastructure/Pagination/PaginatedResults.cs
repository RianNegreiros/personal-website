using Backend.Core.Interfaces.Pagination;

namespace Backend.Infrastructure.Pagination;

public class PaginatedResults<T> : IPaginatedResults<T>
{
  private readonly int _pageNumber;
  private readonly int _pageSize;
  private readonly int _totalPages;
  private readonly List<T> _items;

  public PaginatedResults(int pageNumber, int pageSize, int totalPages, List<T> items)
  {
    _pageNumber = pageNumber;
    _pageSize = pageSize;
    _totalPages = totalPages;
    _items = items;
  }

  public int PageNumber => _pageNumber;

  public int PageSize => _pageSize;

  public int TotalPages => _totalPages;

  public List<T> Items => _items;
}

