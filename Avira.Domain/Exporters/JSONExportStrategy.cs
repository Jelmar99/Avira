using Avira.Domain.Interfaces;
using Newtonsoft.Json;

namespace Avira.Domain;

// Strategy Pattern
public class JSONExportStrategy : IExportStrategy
{
    public void ExportSprint(Sprint sprint)
    {
        var output = JsonConvert.SerializeObject("Sprint Time span from:  " + sprint.StartDate + " to " + sprint.EndDate, Formatting.Indented);
        Console.WriteLine(output);
    }

    public void ExportBacklogItem(BacklogItem backlogItem)
    {
        var output = JsonConvert.SerializeObject("-BacklogItem: " + backlogItem.Name + ", with description: " + backlogItem.Description, Formatting.Indented);
        Console.WriteLine(output);
    }

    public void ExportComment(Comment comment)
    {
        var output = JsonConvert.SerializeObject("--Comment: " + comment.Text, Formatting.Indented);
        Console.WriteLine(output);
    }

    public void ExportActivity(Activity activity)
    {
        var output = JsonConvert.SerializeObject("--Activity: " + activity.Name, Formatting.Indented);
        Console.WriteLine(output);
    }

    public void ExportProductBacklog(ProductBacklog productBacklog)
    {
        var output = JsonConvert.SerializeObject("ProductBacklog: " + productBacklog.Id + " From sprint: " + productBacklog.Sprint.Name, Formatting.Indented);
        Console.WriteLine(output);
    }
}