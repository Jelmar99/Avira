namespace Avira.Domain.Interfaces;

//Visitor pattern combined with strategy pattern
public interface IExportStrategy
{
    void ExportSprint(Sprint sprint);
    void ExportBacklogItem(BacklogItem backlogItem);
    void ExportComment(Comment comment);
    void ExportActivity(Activity activity);
    void ExportProductBacklog(ProductBacklog productBacklog);
}