using Avira.Domain.Notifications;

namespace Avira.Domain.Builder;

public class UserBuilder
{
    // Design pattern: Builder
    private Guid _id;
    private string? _name;
    private Role _role;
    private INotificationPreference _notificationPreference;
    private string? _email;
    private string? _phoneNr;
    private string? _slackUsername;

    public UserBuilder()
    {
        // Always needs at least a default Notification Preference
        _notificationPreference = new NotificationPreference();
    }

    public UserBuilder setId(Guid id)
    {
        _id = id;
        return this;
    }

    public UserBuilder setName(string name)
    {
        _name = name;
        return this;
    }

    public UserBuilder setRole(Role role)
    {
        _role = role;
        return this;
    }

    public UserBuilder addNotificationPreference(NotificationPreferenceType notificationPreferenceType)
    {
        _notificationPreference = notificationPreferenceType switch
        {
            NotificationPreferenceType.Email => new EmailNotificationPreferenceDecorator(_notificationPreference),
            NotificationPreferenceType.Slack => new SlackNotificationPreferenceDecorator(_notificationPreference),
            NotificationPreferenceType.WhatsApp => new WhatsAppNotificationPreferenceDecorator(_notificationPreference),
            _ => _notificationPreference
        };
        return this;
    }

    public UserBuilder setEmail(string email)
    {
        _email = email;
        return this;
    }

    public UserBuilder setPhoneNr(string phoneNr)
    {
        _phoneNr = phoneNr;
        return this;
    }

    public UserBuilder setSlackUsername(string slackUsername)
    {
        _slackUsername = slackUsername;
        return this;
    }

    public User Build()
    {
        return new User(_id, _name, _role, _notificationPreference, _email, _phoneNr, _slackUsername);
    }
}