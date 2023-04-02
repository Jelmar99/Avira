using Avira.Domain.Notifications;

namespace Avira.Domain.Interfaces;

public interface INotificationListener
{
    // Design pattern: Observer
    void onNotification(Notification notification);
}