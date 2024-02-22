using Backend.Core.Interfaces.CloudServices;

using MailKit.Net.Smtp;

using Microsoft.Extensions.Configuration;

using MimeKit;

namespace Backend.Infrastructure.CloudServices;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    public EmailService(IConfiguration configuration)
    {
        _config = configuration;
    }
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("no-reply", "no-reply@riannegreiros.dev"));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.resend.com", 587, false);
        await client.AuthenticateAsync("resend", _config["ResendKey"]);
        await client.SendAsync(emailMessage);

        await client.DisconnectAsync(true);
    }

    public string GenerateNewPostNotificationTemplate(string postTitle, string postSlug)
    {
        string clientUrl = _config["ClientUrl"];
        string postUrl = $"{clientUrl}/posts/{postSlug}";

        string htmlTemplate = @"
        <!DOCTYPE html>
        <html>
        <head>
            <title>New Post Notification</title>
        </head>
        <body>
            <h1>New Post: {post_title}</h1>
            <p>A new post titled '{post_title}' has been published.</p>
            <p>Click <a href='{post_url}'>here</a> to read the post.</p>
        </body>
        </html>
    ";

        string htmlMessage = htmlTemplate.Replace("{post_title}", postTitle).Replace("{post_url}", postUrl);

        return htmlMessage;
    }

    public string GeneratePostConfirmationTemplate(string postTitle, string postSlug)
    {
        string htmlTemplate = @"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Post Confirmation</title>
            </head>
            <body>
                <h1>Post Confirmation</h1>
                <p>Your post titled '{post_title}' has been successfully created.</p>
                <p>Click <a href='{post_url}'>here</a> to view your post.</p>
            </body>
            </html>
            ";

        string clientUrl = _config["ClientUrl"];
        string postUrl = $"{clientUrl}/posts/{postSlug}";

        string htmlMessage = htmlTemplate.Replace("{post_title}", postTitle).Replace("{post_url}", postUrl);

        return htmlMessage;
    }

    public string GenerateProjectConfirmationTemplate(string projectTitle)
    {
        string htmlTemplate = @"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Project Confirmation</title>
            </head>
            <body>
                <h1>Project Confirmation</h1>
                <p>Your project titled '{post_title}' has been successfully created.</p>
                <p>Click <a href='{post_url}'>here</a> to view your check it out.</p>
            </body>
            </html>
            ";

        string clientUrl = _config["ClientUrl"];
        string postUrl = $"{clientUrl}/projects";

        string htmlMessage = htmlTemplate.Replace("{post_title}", projectTitle).Replace("{post_url}", postUrl);

        return htmlMessage;
    }

    public string GenerateCommentNotificationTemplate(string commenterName, string commentContent, string postSlug)
    {
        string htmlTemplate = @"
        <!DOCTYPE html>
        <html>
        <head>
            <title>Comment Notification</title>
        </head>
        <body>
            <h1>Comment Notification</h1>
            <p>Dear Post Owner,</p>
            <p>{commenter_name} has commented on your post.</p>
            <p>The comment is: '{comment_content}'</p>
            <p>Click <a href='{post_url}'>here</a> to view the comment.</p>
        </body>
        </html>
        ";

        string clientUrl = _config["ClientUrl"];
        string postUrl = $"{clientUrl}/posts/{postSlug}";

        string htmlMessage = htmlTemplate.Replace("{commenter_name}", commenterName)
                                         .Replace("{comment_content}", commentContent)
                                         .Replace("{post_url}", postUrl);

        return htmlMessage;
    }

    public string GenerateReplyTemplate(string commenterName, string commentContent, string postSlug)
    {
        string htmlTemplate = @"
        <!DOCTYPE html>
        <html>
        <head>
            <title>Comment Reply Notification</title>
        </head>
        <body>
            <h1>Comment Reply Notification</h1>
            <p>Dear {commenter_name},</p>
            <p>Someone has replied to your comment: '{comment_content}'.</p>
            <p>Click <a href='{post_url}'>here</a> to view the reply.</p>
        </body>
        </html>
        ";

        string clientUrl = _config["ClientUrl"];
        string postUrl = $"{clientUrl}/posts/{postSlug}";

        string htmlMessage = htmlTemplate.Replace("{commenter_name}", commenterName)
                                         .Replace("{comment_content}", commentContent)
                                         .Replace("{post_url}", postUrl);

        return htmlMessage;
    }
}