﻿namespace Avira.Domain;

public class Project
{
    private Guid Id { get; }
    private ProductBacklog ProductBacklog { get; }
    private List<Sprint>? Sprints { get; set; }

    private IVersionControl VersionControl;

    private User ProductOwner { get; set; }

    public Project(Guid id, ProductBacklog productBacklog, IVersionControl adapter)
    {
        Id = id;
        ProductBacklog = productBacklog;
        VersionControl = adapter;
    }

    public void AddSprint(Sprint sprint)
    {
        Sprints?.Add(sprint);
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