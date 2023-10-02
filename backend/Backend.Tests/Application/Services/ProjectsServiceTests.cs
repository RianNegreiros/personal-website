using System.IO;
using System.Threading.Tasks;
using Backend.Application.Models;
using Backend.Application.Services;
using Backend.Core.Interfaces.CloudServices;
using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Backend.Tests.Application.Services
{
    public class ProjectsServiceTests
    {
        [Fact]
        public async Task CreateProject_ValidInput_ReturnsCreatedProject()
        {
            // Arrange
            var projectInputModel = new ProjectInputModel
            {
                Title = "Test Project",
                Overview = "Test overview",
                Image = new FormFile(new MemoryStream(new byte[0]), 0, 0, "Image", "test.jpg"),
                Url = "http://example.com"
            };

            var cloudinaryServiceMock = new Mock<ICloudinaryService>();
            cloudinaryServiceMock.Setup(service => service.UploadImageAsync(It.IsAny<Stream>(), It.IsAny<string>()))
                                 .ReturnsAsync("http://image-url.com/test.jpg");

            var projectsRepositoryMock = new Mock<IProjectsRepository>();

            var projectsService = new ProjectsService(projectsRepositoryMock.Object, cloudinaryServiceMock.Object);

            // Act
            var result = await projectsService.CreateProject(projectInputModel);

            // Assert
            Assert.Equal("Test Project", result.Title);
            Assert.Equal("http://image-url.com/test.jpg", result.ImageUrl);
            Assert.Equal("http://example.com", result.Url);

            projectsRepositoryMock.Verify(repo => repo.CreateProjectAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProject_ValidId_CallsRepositoryDelete()
        {
            // Arrange
            var projectId = "test-id";
            var projectsRepositoryMock = new Mock<IProjectsRepository>();
            var cloudinaryServiceMock = new Mock<ICloudinaryService>();
            var projectsService = new ProjectsService(projectsRepositoryMock.Object, cloudinaryServiceMock.Object);

            // Act
            await projectsService.DeleteProject(projectId);

            // Assert
            projectsRepositoryMock.Verify(repo => repo.DeleteProjectAsync(projectId), Times.Once);
        }
    }
}