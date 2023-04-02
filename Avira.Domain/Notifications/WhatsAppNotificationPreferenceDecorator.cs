namespace Avira.Domain.Notifications;

public class WhatsAppNotificationPreferenceDecorator : BaseNotificationPreferenceDecorator
{
    // Design pattern: Decorator
    public WhatsAppNotificationPreferenceDecorator(INotificationPreference wrappedPreference) : base(wrappedPreference)
    {
    }

    public override void sendNotification(Notification notification)
    {
        base.sendNotification(notification);
        Console.WriteLine(
            $"Notification for {notification.Recipient.Name}\t" +
            $"WhatsApp: '{notification.Message}'\t" +
            $"To: {notification.Recipient.PhoneNr}");
    }
}