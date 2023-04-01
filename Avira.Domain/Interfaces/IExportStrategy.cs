namespace Avira.Domain.Interfaces;

//Visitor pattern combined with strategy pattern
public interface IExportStrategy
{
    string ExportSprint(Sprint sprint);
    string ExportBacklogItem(BacklogItem backlogItem);
    string ExportComment(Comment comment);
    string ExportActivity(Activity activity);
    string ExportProductBacklog(ProductBacklog productBacklog);
}