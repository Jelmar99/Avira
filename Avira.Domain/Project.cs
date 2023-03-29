namespace Avira.Domain;

public class Project
{
    private Guid Id { get; set; }
    private ProductBacklog ProductBacklog { get; set; }
    private List<Sprint>? Sprints { get; set; }
    
    public Project(Guid id, ProductBacklog productBacklog)
    {
        Id = id;
        ProductBacklog = productBacklog;
    }
    public void AddSprint(Sprint sprint)
    {
        Sprints?.Add(sprint);
    }
}