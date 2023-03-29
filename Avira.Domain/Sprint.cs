namespace Avira.Domain;

public class Sprint
{
    private Guid Id { get; set; }
    private DateTime StartDate { get; set; }
    private DateTime EndDate { get; set; }
    private List<BacklogItem>? BacklogItems { get; set; }
    
    public Sprint(Guid id, DateTime startDate, DateTime endDate)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
    }
    
    public void AddBacklogItem(BacklogItem backlogItem)
    {
        BacklogItems?.Add(backlogItem);
    }
    public void RemoveBacklogItem(BacklogItem backlogItem)
    {
        BacklogItems?.Remove(backlogItem);
    }
}