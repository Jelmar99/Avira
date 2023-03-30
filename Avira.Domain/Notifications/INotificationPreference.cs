namespace Avira.Domain.Notifications;

public interface INotificationPreference
{
    void sendNotification(Notification notification);
}