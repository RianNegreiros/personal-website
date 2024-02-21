using Backend.Application.Models;
using Backend.Application.Models.InputModels;
using Backend.Application.Models.ViewModels;
using Backend.Core.Models;

namespace Backend.Application.Services.Interfaces;

public interface IPostService
{
    Task<Post> CreatePost(PostInputModel inputModel, User author);
    Task<Post> UpdatePost(string identifier, UpdatePostModel inputModel, User author);
    Task<PaginatedResult<PostViewModel>> GetPaginatedPosts(int pageNumber, int pageSize);
    Task<List<PostViewModel>> GetPosts();
    Task<PostViewModel?> GetPostByIdentifier(string identifier);
    Task<List<PostViewModel>> GetRandomPosts(int count);
}