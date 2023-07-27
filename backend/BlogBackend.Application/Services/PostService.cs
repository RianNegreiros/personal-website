using BlogBackend.Application.Models;
using BlogBackend.Core.Inferfaces.CloudServices;
using BlogBackend.Core.Models;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace BlogBackend.Application.Services;

public class PostService : IPostService
{
  private readonly IMongoCollection<Post> _postCollection;

  private readonly ICloudinaryService _cloudinaryService;

  public PostService(IMongoDatabase database, ICloudinaryService cloudinaryService)
  {
    _cloudinaryService = cloudinaryService;
    _postCollection = database.GetCollection<Post>("posts");
  }

  public async Task<Post> CreatePost(PostInputModel model, User author)
  {
    var post = new Post
    {
      Title = model.Title,
      Summary = model.Summary,
      Content = model.Content,
      CoverImageUrl = model.CoverImage != null ? await UploadImageAsync(model.CoverImage) : null,
      Author = author
    };

    await _postCollection.InsertOneAsync(post);
    return post;
  }

  public async Task<Post> UpdatePost(string id, PostInputModel model, User author)
  {
    var post = await _postCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    if (post == null)
      throw new Exception("Post not found");

    if (post.Author.Id != author.Id)
      throw new Exception("You are not the author of this post");

    post.Title = model.Title;
    post.Summary = model.Summary;
    post.Content = model.Content;
    post.UpdatedAt = DateTime.Now;

    if (model.CoverImage != null)
    {
      post.CoverImageUrl = await UploadImageAsync(model.CoverImage);
    }

    await _postCollection.ReplaceOneAsync(p => p.Id == id, post);

    return post;
  }

  public async Task<List<PostViewModel>> GetPosts()
  {
    var posts = await _postCollection.Find(_ => true).ToListAsync();

    var postViewModels = posts.Select(post => new PostViewModel
    {
      Id = post.Id,
      Title = post.Title,
      Summary = post.Summary,
      Content = post.Content,
      CoverImageUrl = post.CoverImageUrl
    }).ToList();

    return postViewModels;
  }

  public async Task<PostViewModel> GetPost(string id)
  {
    var post = await _postCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

    if (post == null)
    {
      return null;
    }

    var postViewModel = new PostViewModel
    {
      Id = post.Id,
      Title = post.Title,
      Summary = post.Summary,
      Content = post.Content,
      CoverImageUrl = post.CoverImageUrl
    };

    return postViewModel;
  }

  public async Task<string> UploadImageAsync(IFormFile coverImage)
  {
    if (coverImage == null || coverImage.Length <= 0)
      throw new Exception("Image is required");

    string imageUrl = await _cloudinaryService.UploadImageAsync(coverImage.OpenReadStream(), coverImage.FileName);

    return imageUrl;
  }
}