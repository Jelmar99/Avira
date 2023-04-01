using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class ProductBacklog : IExport
{
    public Guid Id { get; }
    public Sprint? Sprint { get; }
    private List<BacklogItem>? BacklogItems { get; set; }
    public ProductBacklog(Guid id, Sprint? sprint)
    {
        Id = id;
        Sprint = sprint;
    }
    public void AddBacklogItem(BacklogItem backlogItem)
    {
        BacklogItems?.Add(backlogItem);
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitProductBacklog(this);
    }
}