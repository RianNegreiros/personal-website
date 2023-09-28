using System.Threading.Tasks;
using Backend.Application.Models;
using Backend.Application.Services;
using Backend.Core.Exceptions;
using Backend.Core.Inferfaces.Repositories;
using Backend.Core.Models;
using Moq;
using Xunit;

namespace Backend.Tests.Application.Services
{
    public class PostServiceTests
    {
        [Fact]
        public async Task CreatePost_ValidInput_ReturnsCreatedPost()
        {
            // Arrange
            var postRepositoryMock = new Mock<IPostRepository>();
            var postService = new PostService(postRepositoryMock.Object);
            var model = new PostInputModel
            {
                AuthorId = "test_author_id",
                Title = "Test Title",
                Summary = "Test Summary",
                Content = "Test Content"
            };
            var author = new User
            {
                Id = "test_author_id",
                UserName = "test_username",
                Email = "test_email"
            };

            postRepositoryMock.Setup(repo => repo.Create(It.IsAny<Post>()))
                             .ReturnsAsync(new Post
                             {
                                 Id = "test_id",
                                 Author = author,
                                 Title = "Test Title",
                                 Summary = "Test Summary",
                                 Content = "Test Content"
                             });

            // Act
            var result = await postService.CreatePost(model, author);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdatePost_InvalidId_ThrowsNullReferenceException()
        {
            // Arrange
            var postRepositoryMock = new Mock<IPostRepository>();
            var postService = new PostService(postRepositoryMock.Object);

            UpdatePostModel model = new()
            {
                AuthorId = "test_author_id",
                Title = "Test Title",
                Summary = "Test Summary",
                Content = "Test Content"
            };

            User author = new()
            {
                Id = "test_author_id",
                UserName = "test_username",
                Email = "test_email"
            };

            postRepositoryMock.Setup(repo => repo.Update(It.IsAny<Post>()))
                .ThrowsAsync(new PostNotFoundException("Post with this id does not exist"));

            // Act
            var exception = await Assert.ThrowsAsync<PostNotFoundException>(() => postService.UpdatePost("test_id", model, author));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PostNotFoundException>(exception);
        }
    }
}