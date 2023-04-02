namespace Avira.Domain.Notifications;

public class SlackNotificationPreferenceDecorator : BaseNotificationPreferenceDecorator
{
    // Design pattern: Decorator
    public SlackNotificationPreferenceDecorator(INotificationPreference wrappedPreference) : base(wrappedPreference)
    {
    }

    public override void sendNotification(Notification notification)
    {
        base.sendNotification(notification);
        Console.WriteLine(
            $"Notification for {notification.Recipient.Name}\t" +
            $"Slack\t: '{notification.Message}'\t" +
            $"To: {notification.Recipient.SlackUsername}");
    }
}