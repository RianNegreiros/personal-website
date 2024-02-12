namespace Backend.Application.Services.Interfaces;

public interface IFeedService
{
  Task<List<object>> GetFeed();
}
