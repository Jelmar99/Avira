using Avira.Domain.Notifications;

namespace Avira.Domain.Interfaces;

public interface INotificationListener
{
    void onNotification(Notification notification);
}