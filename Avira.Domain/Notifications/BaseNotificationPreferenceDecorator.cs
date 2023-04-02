namespace Avira.Domain.Notifications;

public class BaseNotificationPreferenceDecorator : INotificationPreference
{
    // Design pattern: Decorator
    private readonly INotificationPreference _wrappedPreference;

    protected BaseNotificationPreferenceDecorator(INotificationPreference wrappedPreference)
    {
        _wrappedPreference = wrappedPreference;
    }

    public virtual void sendNotification(Notification notification)
    {
        _wrappedPreference.sendNotification(notification);
    }
}