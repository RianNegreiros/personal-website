using backend.API.DTOs;
using backend.Core.Interfaces.Services;
using backend.Core.Models;
using MongoDB.Driver;

namespace backend.Persistence.Services;

public class PostService : IPostService
{
  private readonly IMongoCollection<Post> _postCollection;

  public PostService(IMongoDatabase database)
  {
    _postCollection = database.GetCollection<Post>("posts");
  }

  public async Task<Post> CreatePost(PostDto model, string authorId)
  {
    var filePath = model.File != null ? SaveFile(model.File) : null;

    var post = new Post
    {
      Title = model.Title,
      Summary = model.Summary,
      Content = model.Content,
      Cover = filePath,
      AuthorId = authorId
    };

    await _postCollection.InsertOneAsync(post);
    return post;
  }

  public async Task<Post> UpdatePost(string id, PostDto model, string authorId)
  {
    var post = await _postCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    if (post == null)
      return null;

    if (!IsUserTheAuthor(post.AuthorId, authorId))
      return null;

    if (model.File != null)
    {
      DeleteFile(post.Cover);
      post.Cover = SaveFile(model.File);
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
      .SortByDescending(p => p.CreatedAt)
      .ToListAsync();
  }

  public async Task<Post> GetPost(string id)
  {
    return await _postCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
  }

  private string SaveFile(IFormFile file)
  {
    // TODO
    return null;
  }

  private void DeleteFile(string filePath)
  {
    // TODO
  }

  private bool IsUserTheAuthor(string postAuthorId, string requestingUserId)
  {
    // Your implementation to check if the requesting user is the author of the post
    // Replace this with your actual implementation to verify the user's authority
    return postAuthorId == requestingUserId;
  }
}