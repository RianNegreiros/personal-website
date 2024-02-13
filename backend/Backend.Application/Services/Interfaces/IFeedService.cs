using Backend.Application.Models.ViewModels;

namespace Backend.Application.Services.Interfaces;

public interface IFeedService
{
  Task<List<FeedItemViewModel>> GetFeed();
}
