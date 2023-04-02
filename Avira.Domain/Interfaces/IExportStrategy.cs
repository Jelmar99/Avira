namespace Avira.Domain.Interfaces;

public interface IExportStrategy
{
    // Design pattern: Strategy
    string ExportSprint(Sprint sprint);
    string ExportBacklogItem(BacklogItem backlogItem);
    string ExportComment(Comment comment);
    string ExportActivity(Activity activity);
    string ExportProductBacklog(ProductBacklog productBacklog);
}