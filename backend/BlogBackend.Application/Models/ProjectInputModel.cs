using Microsoft.AspNetCore.Http;

namespace BlogBackend.Application.Models;

public class ProjectInputModel
{
    public string Title { get; set; }
    public string Url { get; set; }
    public string Overview { get; set; }
    public IFormFile Image { get; set; }
}
