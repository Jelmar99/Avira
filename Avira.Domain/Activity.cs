using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class Activity : IExport
{
    private Guid Id { get; }
    public string Name { get; }

    public bool Done { get; set; }

    public Activity(Guid id, string name)
    {
        Id = id;
        Name = name;
        Done = false;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitActivity(this);
    }
}