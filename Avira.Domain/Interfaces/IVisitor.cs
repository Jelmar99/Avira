namespace Avira.Domain.Interfaces;

public interface IVisitor
{
    // Design pattern: Visitor
    string VisitSprint(Sprint sprint);
    string VisitBacklogItem(BacklogItem backlogItem);
    string VisitComment(Comment comment);
    string VisitActivity(Activity activity);
    string VisitProductBacklog(ProductBacklog productBacklog);
}