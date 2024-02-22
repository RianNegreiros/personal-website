namespace Backend.Application.Services.Interfaces;

public interface ISubscriberService
{
    Task<bool> AddSubscriberAsync(string email);
    Task<bool> UnsubscribeAsync(string email);
    Task<List<string>> GetAllSubscribedEmailsAsync();
}
