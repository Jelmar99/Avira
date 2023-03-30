namespace Avira.Domain.Notifications;

public class SlackNotificationPreferenceDecorator : BaseNotificationPreferenceDecorator
{
    public SlackNotificationPreferenceDecorator(INotificationPreference wrappedPreference) : base(wrappedPreference)
    {
    }

    public override void sendNotification(Notification notification)
    {
        base.sendNotification(notification);
        Console.WriteLine($"Notification for {notification.recipient.Name}\tSlack\t: '{notification.Message}'");
    }
}