using Avira.Domain.Interfaces;
using Avira.Domain.Notifications;

namespace Avira.Domain;

public class User : INotificationListener
{
    private Guid _id;
    public string? Name { get; }
    public Role? Role { get; private set; }
    private INotificationPreference NotificationPreference { get; }
    public string? Email { get; }
    public string? PhoneNr { get; }
    public string? SlackUsername { get; }

    public User(Guid id, string? name, Role role, INotificationPreference notificationPreference, string? email,
        string? phoneNr, string? slackUsername)
    {
        _id = id;
        Name = name;
        Role = role;
        NotificationPreference = notificationPreference;
        Email = email;
        PhoneNr = phoneNr;
        SlackUsername = slackUsername;
    }

    public void SetRole(Role? role)
    {
        Role = role;
    }

    public void RemoveRole()
    {
        Role = null;
    }

    public void onNotification(Notification notification)
    {
        // Design pattern: Observer
        notification.Recipient = this;
        NotificationPreference.sendNotification(notification);
    }
}