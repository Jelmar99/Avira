using Avira.Domain.Interfaces;

namespace Avira.Domain.Exporters;

public class Exporter : IVisitor
{
    // Design pattern: Strategy
    public IExportStrategy ExportStrategy { get; set; }
    
    public Exporter(IExportStrategy exportStrategy)
    {
        ExportStrategy = exportStrategy;
    }
    public string VisitSprint(Sprint sprint)
    {
        var buildString = "";
        buildString += ExportStrategy.ExportSprint(sprint) + "\n";

        return sprint.GetBacklogItems().Aggregate(buildString, (current, item) => current + item.Accept(this));
    }

    public string VisitBacklogItem(BacklogItem backlogItem)
    {
        var buildString = "";
        buildString += ExportStrategy.ExportBacklogItem(backlogItem) + "\n";
        buildString = backlogItem.Comments.Aggregate(buildString, (current, comment) => current + comment.Accept(this));

        return backlogItem.Activities.Aggregate(buildString, (current, activity) => current + activity.Accept(this));
    }

    public string VisitComment(Comment comment)
    {
        var buildString = "";
        buildString += ExportStrategy.ExportComment(comment)+ "\n";

        return comment.Replies.Aggregate(buildString, (current, item) => current + item.Accept(this));
    }

    public string VisitActivity(Activity activity)
    {
        return ExportStrategy.ExportActivity(activity);
    }

    public string VisitProductBacklog(ProductBacklog productBacklog)
    {
        var buildString = "";
        buildString += ExportStrategy.ExportProductBacklog(productBacklog) + "\n";

        return productBacklog.GetBacklogItems().Aggregate(buildString, (current, item) => current + item.Accept(this));
    }
}