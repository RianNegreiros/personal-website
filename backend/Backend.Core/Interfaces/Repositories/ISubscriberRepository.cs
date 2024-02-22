using Backend.Core.Models;

using MongoDB.Driver;

namespace Backend.Core.Interfaces.Repositories;

public interface ISubscriberRepository
{
    Task<Subscriber> GetByEmailAsync(string email);
    Task InsertAsync(Subscriber subscriber);
    Task UpdateAsync(string email, UpdateDefinition<Subscriber> update);
    Task<List<string>> GetAllSubscribedEmailsAsync();
}