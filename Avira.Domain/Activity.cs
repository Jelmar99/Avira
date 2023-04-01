using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class Activity : IExport
{
    private Guid Id { get; }
    public string Name { get; }
    private ProductBacklog ProductBacklog { get; }

    public Activity(Guid id, string name, ProductBacklog productBacklog)
    {
        Id = id;
        Name = name;
        ProductBacklog = productBacklog;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitActivity(this);
    }
}