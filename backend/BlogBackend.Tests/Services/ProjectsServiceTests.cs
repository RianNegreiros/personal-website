using System.Collections.Generic;
using System.Threading.Tasks;
using BlogBackend.Application.Services;
using BlogBackend.Core.Inferfaces;
using BlogBackend.Core.Models;
using Moq;
using Xunit;

namespace BlogBackend.Tests.Services
{
    public class ProjectsServiceTests
    {
        [Fact]
        public async Task GetProjects_ShouldReturnListOfProjects()
        {
            // Arrange
            var mockRepository = new Mock<IProjectsRepository>();
            mockRepository.Setup(repo => repo.GetAllProjectsAsync()).ReturnsAsync(GetTestProjects());
            var projectsService = new ProjectsService(mockRepository.Object);

            // Act
            var result = await projectsService.GetProjects();

            // Assert
            Assert.Equal(2, result.Count); // Adjust this based on your test data
        }

        [Fact]
        public async Task CreateProject_ShouldCreateNewProject()
        {
            // Arrange
            var mockRepository = new Mock<IProjectsRepository>();
            var projectsService = new ProjectsService(mockRepository.Object);
            var newProject = new Project
            {
                Title = "New Project",
                Overview = "This is a new project.",
                ImageUrl = "image.jpg",
                Url = "https://example.com"
            };

            // Act
            var result = await projectsService.CreateProject(newProject);

            // Assert
            mockRepository.Verify(repo => repo.CreateProjectAsync(It.IsAny<Project>()), Times.Once);
        }

        // You can write similar tests for the other methods

        private List<Project> GetTestProjects()
        {
            return new List<Project>
            {
                new Project { Id = "1", Title = "Project 1", Overview = "Overview 1", ImageUrl = "image1.jpg", Url = "https://project1.com" },
                new Project { Id = "2", Title = "Project 2", Overview = "Overview 2", ImageUrl = "image2.jpg", Url = "https://project2.com" }
            };
        }
    }
}