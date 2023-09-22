namespace Backend.Application.Models;

public class UpdatePostModel
{
    public string AuthorId { get; set; }
    public string? Title { get; set; }
    public string? Summary { get; set; }
    public string? Content { get; set; }
}
