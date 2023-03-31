namespace Avira.Domain;

public class PlainTextExporter : IVisitor
{
    public void VisitSprint(Sprint sprint)
    {
        Console.WriteLine("Sprint Time span from:  " + sprint.StartDate + " to " + sprint.EndDate);
        foreach (var item in sprint.BacklogItems)
        {
            item.Accept(this);
        }
    }

    public void VisitBacklogItem(BacklogItem backlogItem)
    {
        Console.WriteLine("-BacklogItem: " + backlogItem.Name + ", with description: " + backlogItem.Description);
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
        Console.WriteLine("--Comment: " + comment.Text);
    }

    public void VisitActivity(Activity activity)
    {
        Console.WriteLine("--Activity: " + activity.Name);
    }
}