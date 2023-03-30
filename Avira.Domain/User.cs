using Avira.Domain.Interfaces;
using Avira.Domain.Notifications;

namespace Avira.Domain;

public class User : INotificationListener
{
    private Guid Id { get; set; }
    public string Name { get; private set; }
    private Role? Role { get; set; }
    public INotificationPreference NotificationPreference { get; set; }

    public string Email { get; private set; }
    public string PhoneNr { get; private set; }
    public string SlackUsername { get; private set; }

    public User(Guid id, string name, Role role, INotificationPreference notificationPreference, string email,
        string phoneNr, string slackUsername)
    {
        Id = id;
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
        notification.recipient = this;
        NotificationPreference.sendNotification(notification);
    }
}