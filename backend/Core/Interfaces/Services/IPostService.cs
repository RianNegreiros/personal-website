using backend.API.DTOs;
using backend.Core.Models;

namespace backend.Core.Interfaces.Services;
public interface IPostService
{
  Task<Post> CreatePost(PostDto model, User author);
  Task<Post> UpdatePost(string id, PostDto model, User author);
  Task<List<Post>> GetPosts();
  Task<Post> GetPost(string id);
}