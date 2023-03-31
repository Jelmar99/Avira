namespace Avira.Domain.Interfaces;

public interface IExportStrategy
{
    void ExportSprint(Sprint sprint);
    void ExportBacklogItem(BacklogItem backlogItem);
    void ExportComment(Comment comment);
    void ExportActivity(Activity activity);
}