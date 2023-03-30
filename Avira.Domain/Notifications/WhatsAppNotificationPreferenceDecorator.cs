namespace Avira.Domain.Notifications;

public class WhatsAppNotificationPreferenceDecorator : BaseNotificationPreferenceDecorator
{
    public WhatsAppNotificationPreferenceDecorator(INotificationPreference wrappedPreference) : base(wrappedPreference)
    {
    }

    public override void sendNotification(Notification notification)
    {
        base.sendNotification(notification);
        Console.WriteLine($"Notification for {notification.recipient.Name}\tWhatsApp: '{notification.Message}'");
    }
}