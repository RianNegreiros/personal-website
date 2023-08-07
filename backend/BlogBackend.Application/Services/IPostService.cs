using BlogBackend.Application.Models;
using BlogBackend.Core.Models;
using Microsoft.AspNetCore.Http;

namespace BlogBackend.Application.Services;

public interface IPostService
{
  Task<Post> CreatePost(PostInputModel inputModel, User author);
  Task<Post> UpdatePost(string id, PostInputModel inputModel, User author);
  Task<List<PostViewModel>> GetPosts();
  Task<PostViewModel> GetPost(string id);
  Task<string> UploadImageAsync(IFormFile coverImage);
}