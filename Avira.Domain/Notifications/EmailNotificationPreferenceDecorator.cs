namespace Avira.Domain.Notifications;

public class EmailNotificationPreferenceDecorator : BaseNotificationPreferenceDecorator
{
    // Design pattern: Decorator
    public EmailNotificationPreferenceDecorator(INotificationPreference wrappedPreference) : base(wrappedPreference)
    {
    }

    public override void sendNotification(Notification notification)
    {
        base.sendNotification(notification);
        Console.WriteLine(
            $"Notification for {notification.Recipient.Name}\t" +
            $"Email\t: '{notification.Message}'\t" +
            $"To: {notification.Recipient.Email}");
    }
}