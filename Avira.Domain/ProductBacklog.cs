namespace Avira.Domain;

public class ProductBacklog
{
    private Guid Id { get; }
    private Sprint? Sprint { get; }
    private List<BacklogItem>? BacklogItems { get; set; }
    public ProductBacklog(Guid id, Sprint? sprint)
    {
        Id = id;
        Sprint = sprint;
    }
    public void AddBacklogItem(BacklogItem backlogItem)
    {
        BacklogItems?.Add(backlogItem);
    }
}