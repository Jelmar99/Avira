using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class Activity : IExport
{
    private Guid _id;
    public string Name { get; }

    public bool Done { get; set; }

    public Activity(Guid id, string name)
    {
        _id = id;
        Name = name;
        Done = false;
    }

    public string Accept(IVisitor visitor)
    {
        // Design pattern: Visitor
        return visitor.VisitActivity(this);
    }
}