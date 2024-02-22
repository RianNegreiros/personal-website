using Backend.Application.Models;

namespace Backend.Application.Services.Interfaces;

public interface INotificationService
{
    void EnqueueNotification(string notificationType, NotificationContext context);
}