using Microsoft.AspNetCore.Http;

namespace Backend.Application.Models.InputModels;

public class ProjectInputModel
{
    public string AuthorId { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string Overview { get; set; }
    public IFormFile Image { get; set; }
}
