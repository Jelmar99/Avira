using Avira.Domain.Interfaces;

namespace Avira.Domain;

//Strategy for choosing which export --> Strategy pattern
public class Exporter : IVisitor
{
    public IExportStrategy ExportStrategy { get; set; }
    
    public Exporter(IExportStrategy exportStrategy)
    {
        ExportStrategy = exportStrategy;
    }
    public void VisitSprint(Sprint sprint)
    {
        ExportStrategy.ExportSprint(sprint);
        foreach (var item in sprint.GetBacklogItems())
        {
            item.Accept(this);
        }
    }

    public void VisitBacklogItem(BacklogItem backlogItem)
    {
        ExportStrategy.ExportBacklogItem(backlogItem);
        foreach (var comment in backlogItem.Comments)
        {
            comment.Accept(this);
        }

        foreach (var activity in backlogItem.Activities)
        {
            activity.Accept(this);
        }
    }

    public void VisitComment(Comment comment)
    {
        ExportStrategy.ExportComment(comment);
    }

    public void VisitActivity(Activity activity)
    {
        ExportStrategy.ExportActivity(activity);
    }

    public void VisitProductBacklog(ProductBacklog productBacklog)
    {
        ExportStrategy.ExportProductBacklog(productBacklog);
        foreach (var item in productBacklog.GetBacklogItems())
        {
            item.Accept(this);
        }
    }
}