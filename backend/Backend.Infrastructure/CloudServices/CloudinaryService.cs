using Backend.Core.Interfaces.CloudServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;

namespace Backend.Infrastructure.CloudServices;

public class CloudinaryService : ICloudinaryService
{
  private readonly Cloudinary _cloudinary;

  public CloudinaryService(IConfiguration configuration)
  {
    Account account = new(
        configuration["Cloudinary:CloudName"],
        configuration["Cloudinary:ApiKey"],
        configuration["Cloudinary:ApiSecret"]
    );

    _cloudinary = new Cloudinary(account);
  }

  public async Task<string> UploadImageAsync(Stream stream, string fileName)
  {
    ImageUploadParams uploadParams = new()
    {
      File = new FileDescription(fileName, stream),
      Folder = "blog"
    };

    ImageUploadResult uploadResult = await _cloudinary.UploadAsync(uploadParams);

    return uploadResult.SecureUrl.ToString();
  }
}
