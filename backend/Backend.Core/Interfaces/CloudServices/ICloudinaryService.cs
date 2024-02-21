namespace Backend.Core.Interfaces.CloudServices;

public interface ICloudinaryService
{
    Task<string> UploadImageAsync(Stream stream, string fileName);
}