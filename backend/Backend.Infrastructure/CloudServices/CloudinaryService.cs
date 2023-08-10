using Backend.Core.CloudServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;

namespace Backend.Infrastructure.CloudServices;

public class CloudinaryService : ICloudinaryService
{
  private readonly Cloudinary _cloudinary;

  public CloudinaryService(IConfiguration configuration)
  {
    var account = new Account(
        configuration["Cloudinary:CloudName"],
        configuration["Cloudinary:ApiKey"],
        configuration["Cloudinary:ApiSecret"]
    );

    _cloudinary = new Cloudinary(account);
  }

  public async Task<string> UploadImageAsync(Stream stream, string fileName)
  {
    var uploadParams = new ImageUploadParams
    {
      File = new FileDescription(fileName, stream),
      Folder = "blog"
    };

    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

    return uploadResult.SecureUrl.ToString();
  }
}
