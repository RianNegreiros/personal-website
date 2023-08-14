using Backend.Application.Helpers;
using Backend.Application.Models;
using Backend.Core.Exceptions;
using Backend.Core.Inferfaces.Repositories;
using Backend.Core.Models;
using MongoDB.Driver;

namespace Backend.Application.Services
{
  public class PostService : IPostService
  {
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
      _postRepository = postRepository;
    }

    public async Task<Post> CreatePost(PostInputModel model, User author)
    {
      var post = new Post
      {
        Title = model.Title,
        Summary = model.Summary,
        Content = model.Content,
        Slug = SlugHelper.Slugify(model.Title),
        Author = author
      };

      return await _postRepository.Create(post);
    }

    public async Task<Post> UpdatePost(string id, PostInputModel model, User author)
    {
      var post = await _postRepository.GetById(id) ?? throw new PostNotFoundException("Post not found");

      if (post.Author.Id != author.Id)
        throw new AuthorizationException("You are not the author of this post");

      post.Title = model.Title;
      post.Summary = model.Summary;
      post.Content = model.Content;
      post.Slug = SlugHelper.Slugify(model.Title);
      post.UpdatedAt = DateTime.UtcNow;

      return await _postRepository.Update(post);
    }

    public async Task<List<PostViewModel>> GetPosts() => (await _postRepository.GetAll())
        .Select(post => new PostViewModel
        {
          Id = post.Id,
          Title = post.Title,
          Summary = post.Summary,
          Content = post.Content,
          Slug = post.Slug,
          CreatedAt = post.CreatedAt
        }).ToList();

    public async Task<PostViewModel> GetPostById(string id)
    {
      var post = await _postRepository.GetById(id);
      if (post == null)
        return null;

      return new PostViewModel
      {
        Id = post.Id,
        Title = post.Title,
        Summary = post.Summary,
        Content = post.Content,
        Slug = post.Slug,
        CreatedAt = post.CreatedAt
      };
    }

    public async Task<PostViewModel> GetPostBySlug(string slug)
    {
      var post = await _postRepository.GetBySlug(slug);
      if (post == null)
        return null;

      return new PostViewModel
      {
        Id = post.Id,
        Title = post.Title,
        Summary = post.Summary,
        Content = post.Content,
        Slug = post.Slug,
        CreatedAt = post.CreatedAt
      };
    }
  }
}
