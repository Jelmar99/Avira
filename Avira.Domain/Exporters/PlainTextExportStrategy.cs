using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class PlainTextExportStrategy : IExportStrategy 
{
    public void ExportSprint(Sprint sprint)
    {
        Console.WriteLine("Sprint Time span from:  " + sprint.StartDate + " to " + sprint.EndDate);
    }

    public void ExportBacklogItem(BacklogItem backlogItem)
    {
        Console.WriteLine("-BacklogItem: " + backlogItem.Name + ", with description: " + backlogItem.Description);
    }

    public void ExportComment(Comment comment)
    {
        Console.WriteLine("--Comment: " + comment.Text);
    }

    public void ExportActivity(Activity activity)
    {
        Console.WriteLine("--Activity: " + activity.Name);
    }
}