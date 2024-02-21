using System;

using Backend.Application.Services.Implementations;
using Backend.Core.Models;

using Microsoft.Extensions.Configuration;

using Moq;

using Xunit;

namespace Backend.Tests.Application.Services
{
    public class TokenServiceTests
    {
        [Fact]
        public void GenerateJwtToken_ValidUser_ReturnsToken()
        {
            // Arrange
            var user = new User
            {
                Id = "testid",
                Email = "test@example.com",
                UserName = "testuser"
            };

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config["JwtConfig:SecretKey"]).Returns("averylongsupersecretkeywithmorethan128bits");
            configurationMock.Setup(config => config["JwtConfig:Issuer"]).Returns("testissuer");
            configurationMock.Setup(config => config["JwtConfig:Audience"]).Returns("testaudience");

            var tokenService = new TokenService(configurationMock.Object);

            // Act
            var tokenExpiresTime = DateTime.UtcNow.AddMinutes(30);
            var token = tokenService.GenerateJwtToken(user, tokenExpiresTime);

            // Assert
            Assert.NotNull(token);
        }
    }
}