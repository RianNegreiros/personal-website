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
            // Perform more assertions as needed
        }

        [Fact]
        public async Task UpdatePost_InvalidId_ThrowsPostNotFoundException()
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

            postRepositoryMock.Setup(repo => repo.GetById(It.IsAny<string>()))
                             .ReturnsAsync((Post)null);

            // Act & Assert
            await Assert.ThrowsAsync<PostNotFoundException>(() => postService.UpdatePost("invalid_id", model, author));
        }
    }
}