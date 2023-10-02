using Backend.Application.Models;
using Backend.Core.Models;

namespace Backend.Application.Services;

public interface IPostService
{
  Task<Post> CreatePost(PostInputModel inputModel, User author);
  Task<Post> UpdatePost(string identifier, UpdatePostModel inputModel, User author);
  Task<List<PostViewModel>> GetPosts(int pageNumber, int pageSize);
  Task<PostViewModel?> GetPostByIdentifier(string identifier);
}