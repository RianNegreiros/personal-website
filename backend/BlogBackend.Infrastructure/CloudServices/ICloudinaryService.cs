namespace BlogBackend.Infrastructure.CloudServices;

public interface ICloudinaryService
{
  Task<string> UploadImageAsync(Stream stream, string fileName);
}