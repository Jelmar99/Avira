using Avira.Domain.Interfaces;
using Avira.Domain.Notifications;

namespace Avira.Domain;

public class BacklogItem
{
    private Guid Id { get; set; }
    private string Name { get; set; }
    private string Description { get; set; }
    private int StoryPoints { get; set; }
    private int SprintId { get; set; }
    private Sprint Sprint { get; set; }
    private List<Activity>? Activities { get; set; }
    private ICollection<INotificationListener> notificationListeners = new List<INotificationListener>(); // TODO: move to constructor

    public BacklogItem(Guid id, string name, string description, int storyPoints, int sprintId, Sprint sprint)
    {
        Id = id;
        Name = name;
        Description = description;
        StoryPoints = storyPoints;
        SprintId = sprintId;
        Sprint = sprint;
    }
    
    public void AddActivity(Activity activity)
    {
        Activities?.Add(activity);
    }
    
    public void RemoveActivity(Activity activity)
    {
        Activities?.Remove(activity);
    }

    public void AddListener(INotificationListener listener)
    {
        notificationListeners.Add(listener);
    }

    //TODO: Make private 
    public void SendNotification(Notification notification)
    {
        foreach (var notificationListener in notificationListeners)
        {
            notificationListener.onNotification(notification);
        }
    }
}