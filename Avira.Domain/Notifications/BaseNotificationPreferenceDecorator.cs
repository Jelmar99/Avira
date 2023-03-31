namespace Avira.Domain.Notifications;

public class BaseNotificationPreferenceDecorator : INotificationPreference
{
    private readonly INotificationPreference WrappedPreference;
    
    public BaseNotificationPreferenceDecorator(INotificationPreference wrappedPreference)
    {
        WrappedPreference = wrappedPreference;
    }

    public virtual void sendNotification(Notification notification)
    {
        WrappedPreference.sendNotification(notification);
    }
}