using Backend.Core.Models;

namespace Backend.Core.Interfaces.Repositories;

public interface IPostRepository
{
    Task<Post> Create(Post post);
    Task<Post> Update(Post post);
    Task<Post> GetById(string id);
    Task<Post> GetBySlug(string slug);
    Task<List<Post>> GetAll(int pageNumber, int pageSize);
    Task<int> Count();
}
