using Backend.Core.Models;

namespace Backend.Application.Models;

public class CommentViewModel
{
    public string Id { get; set; }
    public string Content { get; set; }
    public User Author { get; set; }
    public DateTime CreatedAt { get; set; }
}
