using Avira.Domain.Interfaces;
using Avira.Domain.Notifications;

namespace Avira.Domain;

public class User : INotificationListener
{
    private Guid Id { get; set; }
    public string Name { get; set; }
    private Role? Role { get; set; }
    public INotificationPreference NotificationPreference { get; set; }

    public User(Guid id, string name)
    {
        Id = id;
        Name = name;
        NotificationPreference = new NotificationPreference();
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