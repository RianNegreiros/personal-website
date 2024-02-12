using Backend.Application.Services.Interfaces;
using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;

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

  public async Task<List<object>> GetFeed()
  {
    var posts = await _postRepository.GetAll();
    var projects = await _projectsRepository.GetAllProjectsAsync();

    var feedItems = new List<object>();

    feedItems.AddRange(posts);
    feedItems.AddRange(projects);

    feedItems = feedItems
        .OrderByDescending(item =>
            item is Post post ? post.CreatedAt :
            item is Project project ? project.CreatedAt :
            default)
        .ToList();

    return feedItems;
  }
}
