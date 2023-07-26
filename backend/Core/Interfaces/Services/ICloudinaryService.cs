namespace backend.Core.Interfaces.Services;

public interface ICloudinaryService
{
  Task<string> UploadImageAsync(Stream stream, string fileName);
}
