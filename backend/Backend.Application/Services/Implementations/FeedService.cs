using Backend.Application.Models.ViewModels;
using Backend.Application.Services.Interfaces;
using Backend.Core.Interfaces.Repositories;

namespace Backend.Application.Services.Implementations;

public class FeedService : IFeedService
{
  private readonly IPostRepository _postRepository;
  private readonly IProjectsRepository _projectsRepository;

  public FeedService(IPostRepository postRepository, IProjectsRepository projectsRepository)
  {
    _postRepository = postRepository;
    _projectsRepository = projectsRepository;
  }

  public async Task<List<FeedItemViewModel>> GetFeed()
  {
    var posts = await _postRepository.GetAll();
    var projects = await _projectsRepository.GetAllProjectsAsync();

    var feedItems = new List<FeedItemViewModel>();

    feedItems.AddRange(posts.Select(p => new FeedItemViewModel
    {
      Id = p.Id,
      Title = p.Title,
      Summary = p.Summary,
      Content = p.Content,
      Slug = p.Slug,
      CreatedAt = p.CreatedAt,
      UpdatedAt = p.UpdatedAt,
      Type = "Post"
    }));

    feedItems.AddRange(projects.Select(p => new FeedItemViewModel
    {
      Id = p.Id,
      Title = p.Title,
      Url = p.Url,
      ImageUrl = p.ImageUrl,
      Overview = p.Overview,
      CreatedAt = p.CreatedAt,
      UpdatedAt = p.UpdatedAt,
      Type = "Project"
    }));

    feedItems = feedItems
        .OrderByDescending(item => item.CreatedAt)
        .ToList();

    return feedItems;
  }
}