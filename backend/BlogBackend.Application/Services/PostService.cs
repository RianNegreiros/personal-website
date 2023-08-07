using BlogBackend.Application.Models;
using BlogBackend.Core.Exceptions;
using BlogBackend.Core.Inferfaces.CloudServices;
using BlogBackend.Core.Inferfaces.Repositories;
using BlogBackend.Core.Models;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace BlogBackend.Application.Services
{
  public class PostService : IPostService
  {
    private readonly IPostRepository _postRepository;
    private readonly ICloudinaryService _cloudinaryService;

    public PostService(IPostRepository postRepository, ICloudinaryService cloudinaryService)
    {
      _postRepository = postRepository;
      _cloudinaryService = cloudinaryService;
    }

    public async Task<Post> CreatePost(PostInputModel model, User author)
    {
      var post = new Post
      {
        Title = model.Title,
        Summary = model.Summary,
        Content = model.Content,
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
      post.UpdatedAt = DateTime.Now;

      return await _postRepository.Update(post);
    }

    public async Task<List<PostViewModel>> GetPosts() => (await _postRepository.GetAll())
        .Select(post => new PostViewModel
        {
          Id = post.Id,
          Title = post.Title,
          Summary = post.Summary,
          Content = post.Content
        }).ToList();

    public async Task<PostViewModel> GetPost(string id)
    {
      var post = await _postRepository.GetById(id);
      if (post == null)
        return null;

      return new PostViewModel
      {
        Id = post.Id,
        Title = post.Title,
        Summary = post.Summary,
        Content = post.Content
      };
    }

    public async Task<string> UploadImageAsync(IFormFile coverImage)
    {
      if (coverImage == null || coverImage.Length <= 0)
        throw new ImageUploadException("Image is required");

      return await _cloudinaryService.UploadImageAsync(coverImage.OpenReadStream(), coverImage.FileName);
    }
  }
}
