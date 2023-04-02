using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class ProductBacklog : IExport
{
    public Guid Id { get; }
    public Sprint? Sprint { get; }
    private List<BacklogItem> BacklogItems;
    
    public ProductBacklog(Guid id, Sprint? sprint)
    {
        Id = id;
        Sprint = sprint;
        BacklogItems = new List<BacklogItem>();
    }

    public void AddBacklogItem(BacklogItem backlogItem)
    {
        BacklogItems.Add(backlogItem);
    }

    public List<BacklogItem> GetBacklogItems()
    {
        return BacklogItems.OrderByDescending(item => item.Weight).ToList();
    }
    public void RemoveBacklogItem(BacklogItem backlogItem)
    {
        if (BacklogItems != null && BacklogItems.Count >= 0)
        {
            BacklogItems?.Remove(backlogItem);
        }
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitProductBacklog(this);
    }
}