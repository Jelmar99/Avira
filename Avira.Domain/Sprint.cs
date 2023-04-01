using Avira.Domain.Interfaces;
using Avira.Domain.Notifications;

namespace Avira.Domain;

public class Sprint : IExport
{
    private Guid Id { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public List<BacklogItem> BacklogItems;
    public User ScrumMaster { get; set; }

    public List<User> Developers { get; set; }

    public Sprint(Guid id, DateTime startDate, DateTime endDate)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        BacklogItems = new List<BacklogItem>();
    }

    public List<BacklogItem> GetBacklogItems()
    {
        return BacklogItems.OrderByDescending(item => item.Weight).ToList();
    }

    public void AddBacklogItem(BacklogItem backlogItem)
    {
        BacklogItems.Add(backlogItem);
    }

    public void RemoveBacklogItem(BacklogItem backlogItem)
    {
        BacklogItems.Remove(backlogItem);
    }

    public void Deploy(Pipeline pipeline)
    {
        pipeline.SendNotification(new Notification("Deploying Sprint " + Id));
        pipeline.Deploy();
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitSprint(this);
    }
}