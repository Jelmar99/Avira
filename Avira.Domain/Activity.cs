using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class Activity : IExport
{
    private Guid Id { get; set; }
    public string Name { get; set; }
    private ProductBacklog ProductBacklog { get; set; }
    
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