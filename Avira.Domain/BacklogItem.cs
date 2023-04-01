using Avira.Domain.Interfaces;
using Avira.Domain.Notifications;

namespace Avira.Domain;

public class BacklogItem : IExport
{
    private Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    private int StoryPoints { get; }
    public int Weight { get; }
    private Sprint Sprint { get; }
    public List<Activity> Activities { get; }
    public List<Comment> Comments { get; }

    private readonly ICollection<INotificationListener> _notificationListeners;

    public BacklogItemPhase _phase { get; private set; }

    private User Developer;

    private User Tester;

    public BacklogItem(Guid id, string name, string description, int storyPoints, int weight, Sprint sprint,
        User developer, User tester)
    {
        if (developer.Role != Role.Developer)
        {
            throw new ArgumentException("The added Developer user must have the 'Developer' Role.");
        }

        if (tester.Role != Role.Tester)
        {
            throw new ArgumentException("The added Tester user must have the 'Tester' Role.");
        }

        Id = id;
        Name = name;
        Description = description;
        StoryPoints = storyPoints;
        Weight = weight;
        Sprint = sprint;
        Developer = developer;
        Tester = tester;

        Activities = new List<Activity>();
        Comments = new List<Comment>();
        _notificationListeners = new List<INotificationListener>();

        _phase = BacklogItemPhase.Todo; // Default Phase is "To do"
    }

    public void UpdatePhase(BacklogItemPhase newPhase, User updatedBy)
    {
        // Before Update Checks
        if (newPhase == _phase)
        {
            throw new Exception("This Backlog Item already has this phase.");
        }


        if (newPhase == BacklogItemPhase.Done)
        {
            // Only developers are allowed to update the Phase to Done
            if (updatedBy.Role != Role.Developer)
            {
                throw new Exception("You must be a Developer to update the Phase of a Backlog Item to 'Done'");
            }

            // When updating a Backlog Item to Done all underlying Activities must also be done.
            var activitiesDone = true;
            Activities.ForEach(Act => { activitiesDone = activitiesDone && Act.Done; });
            if (activitiesDone == false)
            {
                throw new Exception("Not every activity contained in this Backlog Item is Done!");
            }
        }

        // Reverting the Phase to the previous Phase "Doing" is not allowed. 
        if (newPhase == BacklogItemPhase.Doing)
        {
            if (_phase != BacklogItemPhase.Todo)
            {
                throw new Exception("The Phase of a Backlog Item is not allowed to be set back to 'Doing'");
            }
        }

        // Update Phase
        _phase = newPhase;

        // After Update Triggers
        if (newPhase == BacklogItemPhase.Todo)
        {
            var notification =
                new Notification($"An existing Backlog Item's Phase has been reverted to 'Todo': {Name}, ({Id})");
            Sprint.ScrumMaster.onNotification(notification);
        }

        if (newPhase == BacklogItemPhase.ReadyForTesting)
        {
            var notification = new Notification($"A new Backlog Item has become ready to test: {Name} ({Id})");
            Tester.onNotification(notification);
        }
    }

    public void AddActivity(Activity activity)
    {
        Activities.Add(activity);
    }
    
    public void RemoveActivity(Activity activity)
    {
        if (Activities.Count >= 0)
        {
            Activities.Remove(activity);
        }
    }

    public void AddListener(INotificationListener listener)
    {
        _notificationListeners.Add(listener);
    }

    public void AddComment(Comment comment)
    {
        if (_phase != BacklogItemPhase.Done)
        {
            Comments.Add(comment);
        }
        else
        {
            throw new Exception("You can't add a comment to a Backlog Item that is already Done.");
        }
    }

    // Is only public to use in Program.cs as demonstration.
    public void SendNotification(Notification notification)
    {
        foreach (var notificationListener in _notificationListeners)
        {
            notificationListener.onNotification(notification);
        }
    }

    public string Accept(IVisitor visitor)
    {
        return visitor.VisitBacklogItem(this);
    }
}