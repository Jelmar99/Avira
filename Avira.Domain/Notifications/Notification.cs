namespace Avira.Domain.Notifications;

public class Notification
{
    public string Message { get; set; }
    public User recipient { get; set; }

    public Notification(string message)
    {
        Message = message;
    }
}