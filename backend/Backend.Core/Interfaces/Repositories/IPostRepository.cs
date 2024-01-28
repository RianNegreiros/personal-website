using Backend.Core.Models;

namespace Backend.Core.Interfaces.Repositories;

public interface IPostRepository
{
    Task<Post> Create(Post post);
    Task<Post> Update(Post post);
    Task<Post> GetById(string id);
    Task<Post> GetBySlug(string slug);
    Task<List<Post>> GetAllWithParameters(int pageNumber, int pageSize);
    Task<List<Post>> GetAll();
    Task<int> Count();
    Task Delete(string id);
    Task<List<Post>> GetPostsForUserById(string userId);
}
