using BlogBackend.Core.Models;

namespace BlogBackend.Core.Inferfaces.Repositories;

public interface IPostRepository
{
    Task<Post> Create(Post post);
    Task<Post> Update(Post post);
    Task<Post> GetById(string id);
    Task<List<Post>> GetAll();
}
