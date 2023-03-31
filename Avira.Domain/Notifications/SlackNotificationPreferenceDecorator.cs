namespace Avira.Domain.Notifications;

public class SlackNotificationPreferenceDecorator : BaseNotificationPreferenceDecorator
{
    public SlackNotificationPreferenceDecorator(INotificationPreference wrappedPreference) : base(wrappedPreference)
    {
    }

    public override void sendNotification(Notification notification)
    {
        base.sendNotification(notification);
        Console.WriteLine(
            $"Notification for {notification.recipient.Name}\t" +
            $"Slack\t: '{notification.Message}'\t" +
            $"To: {notification.recipient.SlackUsername}");
    }
}