using Backend.Application.Services.Interfaces;
using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;

using MongoDB.Driver;

namespace Backend.Application.Services.Implementations;

public class SubscriberService : ISubscriberService
{
    private readonly ISubscriberRepository _subscriberRepository;

    public SubscriberService(ISubscriberRepository subscriberRepository)
    {
        _subscriberRepository = subscriberRepository;
    }

    public async Task<bool> AddSubscriberAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be empty", nameof(email));
        }

        var existingSubscriber = await _subscriberRepository.GetByEmailAsync(email);
        if (existingSubscriber != null)
        {
            if (existingSubscriber.IsSubscribed)
            {
                return false;
            }
            else
            {
                var update = Builders<Subscriber>.Update.Set(s => s.IsSubscribed, true);
                await _subscriberRepository.UpdateAsync(email, update);
                return true;
            }
        }

        var subscriber = new Subscriber
        {
            Email = email,
            IsSubscribed = true
        };
        await _subscriberRepository.InsertAsync(subscriber);
        return true;
    }

    public async Task<List<string>> GetAllSubscribedEmailsAsync()
    {
        return await _subscriberRepository.GetAllSubscribedEmailsAsync();
    }

    public async Task<bool> UnsubscribeAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be empty", nameof(email));
        }

        var existingSubscriber = await _subscriberRepository.GetByEmailAsync(email);
        if (existingSubscriber == null || !existingSubscriber.IsSubscribed)
        {
            return false;
        }

        var update = Builders<Subscriber>.Update.Set(s => s.IsSubscribed, false);
        await _subscriberRepository.UpdateAsync(email, update);
        return true;
    }
}