using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class Project
{
    private Guid Id;
    private ProductBacklog ProductBacklog;
    private List<Sprint> _sprints;

    private IVersionControl VersionControl;

    public User ProductOwner { get; }

    public Project(Guid id, ProductBacklog productBacklog, IVersionControl adapter, User productOwner)
    {
        Id = id;
        ProductBacklog = productBacklog;
        VersionControl = adapter;
        ProductOwner = productOwner;
        _sprints = new List<Sprint>();
    }

    public void AddSprint(Sprint sprint)
    {
        _sprints.Add(sprint);
    }

    public void Commit()
    {
        VersionControl.Commit();
    }

    public void Push()
    {
        VersionControl.Push();
    }

    public void Pull()
    {
        VersionControl.Pull();
    }
}