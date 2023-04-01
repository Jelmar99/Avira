namespace Avira.Domain;

public interface IVisitor
{
    void VisitSprint(Sprint sprint);
    void VisitBacklogItem(BacklogItem backlogItem);
    void VisitComment(Comment comment);
    void VisitActivity(Activity activity);
    void VisitProductBacklog(ProductBacklog productBacklog);
}