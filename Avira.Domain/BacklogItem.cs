namespace Avira.Domain;

public class BacklogItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int StoryPoints { get; set; }
    public int SprintId { get; set; }
    public Sprint Sprint { get; set; }
    public List<Activity> Activities { get; set; }
}