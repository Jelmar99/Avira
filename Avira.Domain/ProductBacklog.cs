namespace Avira.Domain;

public class ProductBacklog
{
    private Guid Id { get; }
    private Sprint? Sprint { get; }
    private List<BacklogItem> BacklogItems;

    public ProductBacklog(Guid id, Sprint? sprint)
    {
        Id = id;
        Sprint = sprint;
        BacklogItems = new List<BacklogItem>();
    }

    public void AddBacklogItem(BacklogItem backlogItem)
    {
        BacklogItems.Add(backlogItem);
    }

    public List<BacklogItem> GetBacklogItems()
    {
        return BacklogItems.OrderByDescending(item => item.Weight).ToList();
    }
}