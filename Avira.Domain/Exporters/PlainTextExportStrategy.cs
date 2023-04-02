using Avira.Domain.Interfaces;

namespace Avira.Domain.Exporters;

public class PlainTextExportStrategy : IExportStrategy 
{
    // Design pattern: Strategy
    public string ExportSprint(Sprint sprint)
    {
        return $"Sprint: {sprint.Name}, running from {sprint.StartDate} to {sprint.EndDate}";
    }

    public string ExportBacklogItem(BacklogItem backlogItem)
    {
        return
            $"-BacklogItem: {backlogItem.Name}, with description: {backlogItem.Description}, assigned developer: {backlogItem.Developer.Name}";
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
        return $"ProductBacklog: {productBacklog.Id} From sprint: {productBacklog.Sprint?.Name}";
    }
}