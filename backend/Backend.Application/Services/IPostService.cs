using Backend.Application.Models;
using Backend.Core.Models;

namespace Backend.Application.Services;

public interface IPostService
{
  Task<Post> CreatePost(PostInputModel inputModel, User author);
  Task<Post> UpdatePost(string identifier, UpdatePostModel inputModel, User author);
  Task<List<PostViewModel>> GetPosts();
  Task<PostViewModel?> GetPostById(string id);
  Task<PostViewModel?> GetPostBySlug(string slug);
}