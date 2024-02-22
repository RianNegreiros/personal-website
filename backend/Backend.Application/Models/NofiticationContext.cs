namespace Backend.Application.Models;

public class NotificationContext
{
    public string UserEmail { get; set; }
    public string Title { get; set; }
    public string UserName { get; set; }
    public string Content { get; set; }
    public string PostSlug { get; set; }
}