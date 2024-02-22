using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;

using MongoDB.Driver;

namespace Backend.Infrastructure.Repositories;

public class SubscriberRepository : ISubscriberRepository
{
    private readonly IMongoCollection<Subscriber> _subscribers;

    public SubscriberRepository(IMongoDatabase database)
    {
        _subscribers = database.GetCollection<Subscriber>("subscribers");
    }

    public async Task<Subscriber> GetByEmailAsync(string email)
    {
        return await _subscribers.Find(x => x.Email == email).FirstOrDefaultAsync();
    }

    public async Task InsertAsync(Subscriber subscriber)
    {
        await _subscribers.InsertOneAsync(subscriber);
    }

    public async Task UpdateAsync(string email, UpdateDefinition<Subscriber> update)
    {
        await _subscribers.UpdateOneAsync(x => x.Email == email, update);
    }

    public async Task<List<string>> GetAllSubscribedEmailsAsync()
    {
        var filter = Builders<Subscriber>.Filter.Eq(subscriber => subscriber.IsSubscribed, true);
        var subscribers = await _subscribers.Find(filter).ToListAsync();
        return subscribers.Select(subscriber => subscriber.Email).ToList();
    }
}