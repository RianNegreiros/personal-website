namespace Backend.Core.Interfaces.Pagination;

public interface IPaginatedResults<T>
{
  int PageNumber { get; }
  int PageSize { get; }
  int TotalPages { get; }
  List<T> Items { get; }
}
