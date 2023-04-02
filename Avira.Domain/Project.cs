using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class Project
{
    private Guid Id;
    private ProductBacklog ProductBacklog;
    private List<Sprint> _sprints;

    private IVersionControl _versionControl;

    public User ProductOwner { get; }

    public Project(Guid id, ProductBacklog productBacklog, IVersionControl adapter, User productOwner)
    {
        Id = id;
        ProductBacklog = productBacklog;
        _versionControl = adapter;
        ProductOwner = productOwner;
        _sprints = new List<Sprint>();
    }

    public void AddSprint(Sprint sprint)
    {
        _sprints.Add(sprint);
    }

    public void Commit()
    {
        _versionControl.Commit();
    }

    public void Push()
    {
        _versionControl.Push();
    }

    public void Pull()
    {
        _versionControl.Pull();
    }
}