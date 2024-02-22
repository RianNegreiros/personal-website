namespace Backend.Core.Interfaces.CloudServices;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message);
    string GenerateNewPostNotificationTemplate(string postTitle, string postSlug);
    string GeneratePostConfirmationTemplate(string postTitle, string postSlug);
    string GenerateProjectConfirmationTemplate(string projectTitle);
    string GenerateCommentNotificationTemplate(string commenterName, string commentContent, string postSlug);
    string GenerateReplyTemplate(string commenterName, string commentContent, string postSlug);
}