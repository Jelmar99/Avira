namespace Avira.Domain;

public interface INotificationListener
{
    void onNotification(Notification notification);
}