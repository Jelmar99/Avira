namespace Avira.Domain;

// This is the Visitor interface -> Visitor Pattern
public interface IVisitor
{
    string VisitSprint(Sprint sprint);
    string VisitBacklogItem(BacklogItem backlogItem);
    string VisitComment(Comment comment);
    string VisitActivity(Activity activity);
    string VisitProductBacklog(ProductBacklog productBacklog);
}