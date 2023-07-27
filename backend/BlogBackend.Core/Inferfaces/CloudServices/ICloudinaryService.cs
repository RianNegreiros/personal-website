namespace BlogBackend.Core.Inferfaces.CloudServices;

public interface ICloudinaryService
{
  Task<string> UploadImageAsync(Stream stream, string fileName);
}