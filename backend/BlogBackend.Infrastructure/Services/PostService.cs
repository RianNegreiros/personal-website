using BlogBackend.Core.Interfaces.Services;
using BlogBackend.Core.Models;
using MongoDB.Driver;

namespace BlogBackend.Infrastructure.Services;

public class PostService : IPostService
{
  private readonly IMongoCollection<Post> _postCollection;

  private readonly ICloudinaryService _cloudinaryService;

  public PostService(IMongoDatabase database, ICloudinaryService cloudinaryService)
  {
    _cloudinaryService = cloudinaryService;
    _postCollection = database.GetCollection<Post>("posts");
  }

  public async Task<Post> CreatePost(PostDto model, User author)
  {
    var post = new Post
    {
      Title = model.Title,
      Summary = model.Summary,
      Content = model.Content,
      Cover = model.CoverImage != null ? await UploadImageAsync(model.CoverImage) : null,
      Author = author
    };

    await _postCollection.InsertOneAsync(post);
    return post;
  }

  public async Task<Post> UpdatePost(string id, PostDto model, User author)
  {

    var post = await _postCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    if (post == null)
      throw new Exception("Post not found");

    if (post.Author.Id != author.Id)
      throw new Exception("You are not the author of this post");

    if (model.CoverImage != null)
    {
      post.Cover = await UploadImageAsync(model.CoverImage);
    }

    post.Title = model.Title;
    post.Summary = model.Summary;
    post.Content = model.Content;
    post.UpdatedAt = DateTime.Now;

    await _postCollection.ReplaceOneAsync(p => p.Id == id, post);
    return post;
  }

  public async Task<List<Post>> GetPosts()
  {
    return await _postCollection.Find(_ => true)
      .ToListAsync();
  }

  public async Task<Post> GetPost(string id)
  {
    return await _postCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
  }

  public async Task<string> UploadImageAsync(IFormFile coverImage)
  {
    if (coverImage == null || coverImage.Length <= 0)
      throw new Exception("Image is required");

    string imageUrl = await _cloudinaryService.UploadImageAsync(coverImage.OpenReadStream(), coverImage.FileName);

    return imageUrl;
  }
}