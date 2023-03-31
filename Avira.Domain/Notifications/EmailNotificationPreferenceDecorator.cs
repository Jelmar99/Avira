namespace Avira.Domain.Notifications;

public class EmailNotificationPreferenceDecorator : BaseNotificationPreferenceDecorator
{
    public EmailNotificationPreferenceDecorator(INotificationPreference wrappedPreference) : base(wrappedPreference)
    {
    }

    public override void sendNotification(Notification notification)
    {
        base.sendNotification(notification);
        Console.WriteLine(
            $"Notification for {notification.recipient.Name}\t" +
            $"Email\t: '{notification.Message}'\t" +
            $"To: {notification.recipient.Email}");
    }
}