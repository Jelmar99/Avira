namespace Avira.Domain;

public class Activity
{
    private Guid Id { get; set; }
    private string Name { get; set; }
    private ProductBacklog ProductBacklog { get; set; }
    
    public Activity(Guid id, string name, ProductBacklog productBacklog)
    {
        Id = id;
        Name = name;
        ProductBacklog = productBacklog;
    }
}