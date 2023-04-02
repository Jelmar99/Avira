using Avira.Domain.Interfaces;
using Avira.Domain.Notifications;

namespace Avira.Domain;

public class Sprint : IExport
{
    private Guid Id { get; }
    public string Name { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    private readonly List<BacklogItem> _backlogItems;
    public User ScrumMaster { get; }
    public List<User> Developers { get; }
    public bool IsRelease { get; set; }
    public Status Status { get; private set; }

    private readonly Pipeline _pipeline;

    public Sprint(Guid id, string name, DateTime startDate, DateTime endDate, List<User> developers, User scrumMaster)
    {
        Id = id;
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        _backlogItems = new List<BacklogItem>();
        _pipeline = new Pipeline(this);
        Developers = developers;
        ScrumMaster = scrumMaster;
    }

    public void CheckIfFinished()
    {
        if (DateTime.Now > EndDate)
        {
            Status = Status.Finished;
        }
    }

    public void InitializeRelease(User updatedBy)
    {
        if (updatedBy.Role != Role.ScrumMaster)
        {
            throw new Exception("You must be a Scrum Master to initialize a Release.");
        }

        IsRelease = true; // There should be a check if the user that calls this method is the scrum master, but since we dont have login functionality, we will skip this check.
        if (Status == Status.Finished && IsRelease)
        {
            Deploy();
        }
    }

    public void SetStatus(Status status)
    {
        if (DateTime.Now < StartDate)
        {
            Status = status;
        }
        else
        {
            throw new Exception("You can't change the status of a sprint that has already started.");
        }
    }

    public void SetName(string name)
    {
        if (DateTime.Now < StartDate)
        {
            Name = name;
        }
        else
        {
            throw new Exception("You can't change the name of a sprint that has already started.");
        }
    }

    public void SetStartDate(DateTime startDate)
    {
        if (DateTime.Now < StartDate)
        {
            StartDate = startDate;
        }
        else
        {
            throw new Exception("You can't change the start date of a sprint that has already started.");
        }
    }

    public void SetEndDate(DateTime endDate)
    {
        if (DateTime.Now < StartDate)
        {
            EndDate = endDate;
        }
        else
        {
            throw new Exception("You can't change the end date of a sprint that has already started.");
        }
    }

    public List<BacklogItem> GetBacklogItems()
    {
        return _backlogItems.OrderByDescending(item => item.Weight).ToList();
    }

    public void AddBacklogItem(BacklogItem backlogItem)
    {
        if (DateTime.Now < StartDate)
        {
            _backlogItems.Add(backlogItem);
        }
        else
        {
            throw new Exception("You can't add a backlog item to a sprint that has already started.");
        }
    }

    public void RemoveBacklogItem(BacklogItem backlogItem)
    {
        if (DateTime.Now < StartDate)
        {
            _backlogItems.Remove(backlogItem);
        }
        else
        {
            throw new Exception("You can't remove a backlog item from a sprint that has already started.");
        }
    }

    public void Deploy()
    {
        _pipeline.SendNotification(new Notification("Deploying Sprint " + Id));
        _pipeline.Deploy();
    }

    public string Accept(IVisitor visitor)
    {
        // Design pattern: Visitor
        return visitor.VisitSprint(this);
    }
}