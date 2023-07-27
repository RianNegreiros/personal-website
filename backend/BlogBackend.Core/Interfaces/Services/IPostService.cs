using BlogBackend.Core.Models;

namespace BlogBackend.Core.Interfaces.Services;
public interface IPostService
{
  Task<Post> CreatePost(PostDto model, User author);
  Task<Post> UpdatePost(string id, PostDto model, User author);
  Task<List<Post>> GetPosts();
  Task<Post> GetPost(string id);
}