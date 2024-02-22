namespace Backend.Application.Services.Interfaces;

public interface ISubscriberService
{
    Task<bool> AddSubscriberAsync(string email);
    Task<List<string>> GetAllSubscribedEmailsAsync();
}
