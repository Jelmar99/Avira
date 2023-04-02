namespace Avira.Domain;

// This is the Visitor interface -> Visitor Pattern
public interface IVisitor
{
    void VisitSprint(Sprint sprint);
    void VisitBacklogItem(BacklogItem backlogItem);
    void VisitComment(Comment comment);
    void VisitActivity(Activity activity);
    void VisitProductBacklog(ProductBacklog productBacklog);
}