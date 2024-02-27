using Backend.Application.Models.ViewModels;
using Backend.Application.Services.Interfaces;
using Backend.Core.Interfaces.Repositories;

namespace Backend.Application.Services.Implementations;

public class FeedService(IPostRepository postRepository, IProjectsRepository projectsRepository) : IFeedService
{
    private readonly IPostRepository _postRepository = postRepository;
    private readonly IProjectsRepository _projectsRepository = projectsRepository;

    public async Task<List<FeedItemViewModel>> GetFeed()
    {
        var postsTask = _postRepository.GetPostsForFeed();
        var projectsTask = _projectsRepository.GetProjectsForFeedAsync();

        await Task.WhenAll(postsTask, projectsTask);

        var posts = await postsTask;
        var projects = await projectsTask;

        var feedItems = posts.Select(p => new FeedItemViewModel
        {
            Id = p.Id,
            Title = p.Title,
            Summary = p.Summary,
            Slug = p.Slug,
            CreatedAt = p.CreatedAt,
            Type = "Post"
        }).Concat(projects.Select(p => new FeedItemViewModel
        {
            Id = p.Id,
            Title = p.Title,
            Url = p.Url,
            Overview = p.Overview,
            CreatedAt = p.CreatedAt,
            Type = "Project"
        })).ToList();

        feedItems.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));

        return feedItems;
    }
}