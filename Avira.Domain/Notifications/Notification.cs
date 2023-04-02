namespace Avira.Domain.Notifications;

public class Notification
{
    public string Message { get; }
    public User Recipient { get; set; } = null!;

    public Notification(string message)
    {
        Message = message;
    }
}