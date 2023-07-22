using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Persistence.Services;

public class CloudinaryService
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
