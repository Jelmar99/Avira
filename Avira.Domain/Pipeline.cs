using Avira.Domain.Interfaces;
using Avira.Domain.Notifications;

namespace Avira.Domain;

public class Pipeline
{
    private IEnumerable<string> PipelinePhases = new List<string>
        { "Sources", "Package", "Build", "Test", "Analyse", "Deploy", "Utility" };

    private Sprint Sprint;

    private ICollection<INotificationListener> notificationListeners = new List<INotificationListener>();

    public Pipeline(Sprint sprint)
    {
        Sprint = sprint;
    }

    public void Deploy()
    {
        foreach (var phase in PipelinePhases)
        {
            Console.WriteLine(
                $"Executing Phase {phase}\tSprint " +
                $"{Sprint.StartDate.ToShortDateString()} / {Sprint.EndDate.ToShortDateString()}");
        }
    }

    public void AddListener(INotificationListener listener)
    {
        notificationListeners.Add(listener);
    }

    public void SendNotification(Notification notification)
    {
        foreach (var notificationListener in notificationListeners)
        {
            notificationListener.onNotification(notification);
        }
    }
}