using Avira.Domain.Interfaces;
using Avira.Domain.Notifications;

namespace Avira.Domain;

public class BacklogItem : IExport
{
    private Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    private int StoryPoints { get; set; }
    private int SprintId { get; set; }
    private Sprint Sprint { get; set; }
    public List<Activity> Activities { get; set; }
    public List<Comment> Comments { get; set; }
    private ICollection<INotificationListener> notificationListeners = new List<INotificationListener>(); // TODO: move to constructor
    public BacklogItem(Guid id, string name, string description, int storyPoints, int sprintId, Sprint sprint)
    {
        Id = id;
        Name = name;
        Description = description;
        StoryPoints = storyPoints;
        SprintId = sprintId;
        Sprint = sprint;
        Activities = new List<Activity>();
        Comments = new List<Comment>();
    }
    
    public void AddActivity(Activity activity)
    {
        Activities.Add(activity);
    }

    public void AddListener(INotificationListener listener)
    {
        notificationListeners.Add(listener);
    }
    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    //TODO: Make private 
    public void SendNotification(Notification notification)
    {
        foreach (var notificationListener in notificationListeners)
        {
            notificationListener.onNotification(notification);
        }
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitBacklogItem(this);
    }
}