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
    public string VisitSprint(Sprint sprint)
    {
        var buildString = "";
        buildString += ExportStrategy.ExportSprint(sprint) + "\n";
        foreach (var item in sprint.GetBacklogItems())
        {
            buildString += item.Accept(this);
        }

        return buildString;
    }

    public string VisitBacklogItem(BacklogItem backlogItem)
    {
        var buildString = "";
        buildString += ExportStrategy.ExportBacklogItem(backlogItem) + "\n";
        foreach (var comment in backlogItem.Comments)
        {
            buildString += comment.Accept(this);
        }

        foreach (var activity in backlogItem.Activities)
        {
            buildString += activity.Accept(this);
        }

        return buildString;
    }

    public string VisitComment(Comment comment)
    {
        var buildString = "";
        buildString += ExportStrategy.ExportComment(comment)+ "\n";
        foreach (var item in comment.Replies)
        {
            buildString += item.Accept(this);
        }

        return buildString;
    }

    public string VisitActivity(Activity activity)
    {
        return ExportStrategy.ExportActivity(activity);
    }

    public string VisitProductBacklog(ProductBacklog productBacklog)
    {
        var buildString = "";
        buildString += ExportStrategy.ExportProductBacklog(productBacklog) + "\n";
        foreach (var item in productBacklog.GetBacklogItems())
        {
            buildString += item.Accept(this);
        }

        return buildString;
    }
}