using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogBackend.API.Controllers;
using BlogBackend.Application.Models;
using BlogBackend.Application.Services;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using BlogBackend.Core.Models;
using System.Security.Claims;
using System.Collections.Generic;

namespace BlogBackend.Tests.Controllers
{
    [TestClass]
    public class PostControllerTests
    {
        [TestMethod]
        public async Task CreatePost_ValidModel_ReturnsOkResult()
        {
            // Arrange
            var postServiceMock = new Mock<IPostService>();
            var userServiceMock = new Mock<IUserService>();
            var controller = new PostController(postServiceMock.Object, userServiceMock.Object);

            var file = new FormFile(new MemoryStream(), 0, 0, "CoverImage", "cover.jpg");
            var model = new PostInputModel
            {
                Title = "Test Title",
                Content = "Test Content",
                Summary = "Test Summary",
                CoverImage = file
            };

            var user = new User { Id = "user1", UserName = "testuser", Email = "test@example.com" };
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            var expectedPost = new Post
            {
                Id = "post1",
                Title = model.Title,
                Content = model.Content,
                Summary = model.Summary,
                CoverImageUrl = "http://example.com/cover.jpg",
                Author = user
            };
            userServiceMock.Setup(x => x.GetCurrentUser(user.Email)).ReturnsAsync(user);
            postServiceMock.Setup(x => x.CreatePost(It.IsAny<PostInputModel>(), It.IsAny<User>())).ReturnsAsync(expectedPost);

            // Act
            var result = await controller.CreatePost(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<PostViewModel>));

            if (result.Result is OkObjectResult okResult)
            {
                Assert.IsNotNull(okResult.Value);
                Assert.IsInstanceOfType(okResult.Value, typeof(PostViewModel));

                var postViewModel = okResult.Value as PostViewModel;
                Assert.AreEqual(expectedPost.Id, postViewModel.Id);
                Assert.AreEqual(expectedPost.Title, postViewModel.Title);
                Assert.AreEqual(expectedPost.Summary, postViewModel.Summary);
                Assert.AreEqual(expectedPost.Content, postViewModel.Content);
                Assert.AreEqual(expectedPost.CoverImageUrl, postViewModel.CoverImageUrl);
            }
            else if (result.Result is BadRequestObjectResult badRequestResult)
            {
                Assert.Fail($"Request failed with message: {badRequestResult.Value}");
            }
            else
            {
                Assert.Fail($"Unexpected result type: {result.Result.GetType()}");
            }
        }

        [TestMethod]
        public async Task CreatePost_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var postServiceMock = new Mock<IPostService>();
            var userServiceMock = new Mock<IUserService>();
            var controller = new PostController(postServiceMock.Object, userServiceMock.Object);
            var model = new PostInputModel
            {
            };
            controller.ModelState.AddModelError("Title", "Title is required.");

            // Act
            var result = await controller.CreatePost(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<PostViewModel>));
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }
    }
}
