using Backend.Application.Models;
using Backend.Application.Models.InputModels;
using Backend.Application.Models.ViewModels;
using Backend.Application.Services.Interfaces;
using Backend.Core.Exceptions;
using Backend.Core.Helpers;
using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;

using MongoDB.Bson;

namespace Backend.Application.Services.Implementations;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Post> CreatePost(PostInputModel model, User author)
    {
        string slug = SlugHelper.Slugify(model.Title);
        if (await _postRepository.GetBySlug(slug) != null)
            throw new PostAlreadyExistsException("Post with this title already exists");

        Post post = new()
        {
            Title = model.Title,
            Summary = model.Summary,
            Content = model.Content,
            Slug = slug,
            CreatedAt = DateTime.UtcNow,
            Author = author
        };

        return await _postRepository.Create(post);
    }

    public async Task<Post> UpdatePost(string identifier, UpdatePostModel model, User author)
    {
        Post post;
        if (ObjectId.TryParse(identifier, out ObjectId objectId))
        {
            post = await _postRepository.GetById(objectId.ToString());
            if (post == null)
                throw new PostNotFoundException("Post with this id does not exist");
        }
        else
        {
            post = await _postRepository.GetBySlug(identifier);
            if (post == null)
                throw new PostNotFoundException("Post with this id does not exist");
        }

        if (post.Author.Id != author.Id)
            throw new AuthorizationException("You are not the author of this post");

        post.Title = model.Title ?? post.Title;
        post.Summary = model.Summary ?? post.Summary;
        post.Content = model.Content ?? post.Content;
        post.Slug = SlugHelper.Slugify(model.Title ?? post.Title);
        post.UpdatedAt = DateTime.UtcNow;

        return await _postRepository.Update(post);
    }

    public async Task<PaginatedResult<PostViewModel>> GetPaginatedPosts(int pageNumber, int pageSize)
    {
        List<Post> posts = await _postRepository.GetAllWithParameters(pageNumber, pageSize);
        int totalCount = await _postRepository.Count();

        return new PaginatedResult<PostViewModel>
        {
            Items = posts.Select(post => new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Summary = post.Summary,
                Slug = post.Slug,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt
            }),
            TotalCount = totalCount,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<List<PostViewModel>> GetPosts()
    {
        List<Post> posts = await _postRepository.GetAll();
        return posts != null
            ? posts.Select(post => new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Summary = post.Summary,
                Slug = post.Slug,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt
            }).ToList()
            : new List<PostViewModel>();
    }

    public async Task<PostViewModel?> GetPostByIdentifier(string identifier)
    {
        Post post = ObjectId.TryParse(identifier, out ObjectId objectId)
            ? await _postRepository.GetById(objectId.ToString())
            : await _postRepository.GetBySlug(identifier);
        return post == null
            ? null
            : new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Summary = post.Summary,
                Content = post.Content,
                Slug = post.Slug,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt
            };
    }

    public async Task<List<PostViewModel>> GetRandomPosts(int count)
    {
        List<Post> posts = await _postRepository.GetRandomPosts(count);
        return posts != null
            ? posts.Select(post => new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Summary = post.Summary,
                Slug = post.Slug,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt
            }).ToList()
            : new List<PostViewModel>();
    }
}