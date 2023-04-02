namespace Avira.Domain.Notifications;

public interface INotificationPreference
{
    // Design pattern: Decorator
    void sendNotification(Notification notification);
}