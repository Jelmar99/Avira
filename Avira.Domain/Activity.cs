using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class Activity : IExport
{
    private Guid Id { get; }
    public string Name { get; }
    private ProductBacklog ProductBacklog { get; }

    public bool Done { get; set; }

    public Activity(Guid id, string name, ProductBacklog productBacklog)
    {
        Id = id;
        Name = name;
        ProductBacklog = productBacklog;
        Done = false;
    }

    public string Accept(IVisitor visitor)
    {
        return visitor.VisitActivity(this);
    }
}