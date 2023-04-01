using Avira.Domain.Interfaces;

namespace Avira.Domain;

// Strategy Pattern
public class PlainTextExportStrategy : IExportStrategy 
{
    public string ExportSprint(Sprint sprint)
    {
        return $"Sprint: {sprint.Name}, running from {sprint.StartDate} to {sprint.EndDate}";
    }

    public string ExportBacklogItem(BacklogItem backlogItem)
    {
        return $"-BacklogItem: {backlogItem.Name}, with description: {backlogItem.Description}";
    }

    public string ExportComment(Comment comment)
    {
        return $"--Comment: {comment.Text}";
    }

    public string ExportActivity(Activity activity)
    {
        return $"--Activity: {activity.Name}";
    }

    public string ExportProductBacklog(ProductBacklog productBacklog)
    {
        return $"ProductBacklog: {productBacklog.Id} From sprint: {productBacklog.Sprint.Name}";
    }
}