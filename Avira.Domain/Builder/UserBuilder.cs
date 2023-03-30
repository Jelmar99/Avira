using Avira.Domain.Notifications;

namespace Avira.Domain.Builder;

public class UserBuilder
{
    private Guid Id;
    private string Name;
    private Role Role;
    private INotificationPreference NotificationPreference;
    private string Email;
    private string PhoneNr;
    private string SlackUsername;

    public UserBuilder()
    {
        // Always needs at least a default Notification Preference
        NotificationPreference = new NotificationPreference();
    }

    public UserBuilder setId(Guid id)
    {
        Id = id;
        return this;
    }

    public UserBuilder setName(string name)
    {
        Name = name;
        return this;
    }

    public UserBuilder setRole(Role role)
    {
        Role = role;
        return this;
    }

    public UserBuilder setNotificationPreference(INotificationPreference notificationPreference)
    {
        NotificationPreference = notificationPreference;
        return this;
    }

    public UserBuilder setEmail(string email)
    {
        Email = email;
        return this;
    }

    public UserBuilder setPhoneNr(string phoneNr)
    {
        PhoneNr = phoneNr;
        return this;
    }

    public UserBuilder setSlackUsername(string slackUsername)
    {
        SlackUsername = slackUsername;
        return this;
    }

    public User Build()
    {
        return new User(Id, Name, Role, NotificationPreference, Email, PhoneNr, SlackUsername);
    }
}