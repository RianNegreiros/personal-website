using backend.Core.Interfaces.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace backend.Infrastructure.Services;

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
