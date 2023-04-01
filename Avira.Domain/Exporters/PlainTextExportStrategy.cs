using Avira.Domain.Interfaces;

namespace Avira.Domain;

// Strategy Pattern
public class PlainTextExportStrategy : IExportStrategy 
{
    public void ExportSprint(Sprint sprint)
    {
        Console.WriteLine($"Sprint: {sprint.Name}, running from {sprint.StartDate} to {sprint.EndDate}");
    }

    public void ExportBacklogItem(BacklogItem backlogItem)
    {
        Console.WriteLine($"-BacklogItem: {backlogItem.Name}, with description: {backlogItem.Description}");
    }

    public void ExportComment(Comment comment)
    {
        Console.WriteLine($"--Comment: {comment.Text}");
    }

    public void ExportActivity(Activity activity)
    {
        Console.WriteLine($"--Activity: {activity.Name}");
    }

    public void ExportProductBacklog(ProductBacklog productBacklog)
    {
        Console.WriteLine($"ProductBacklog: {productBacklog.Id} From sprint: {productBacklog.Sprint.Name}");
    }
}