using Avira.Domain.Interfaces;
using Avira.Domain.Notifications;

namespace Avira.Domain;

public class BacklogItem : IExport
{
    private Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    private int StoryPoints { get; }
    private int SprintId { get; }
    private Sprint Sprint { get; }
    public List<Activity> Activities { get; }
    public List<Comment> Comments { get; }

    private readonly ICollection<INotificationListener> _notificationListeners;

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
        _notificationListeners = new List<INotificationListener>();
    }

    public void AddActivity(Activity activity)
    {
        Activities.Add(activity);
    }

    public void AddListener(INotificationListener listener)
    {
        _notificationListeners.Add(listener);
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    // Is only public to use in Program.cs as demonstration.
    public void SendNotification(Notification notification)
    {
        foreach (var notificationListener in _notificationListeners)
        {
            notificationListener.onNotification(notification);
        }
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitBacklogItem(this);
    }
}