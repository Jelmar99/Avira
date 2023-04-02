using Avira.Domain.Interfaces;
using Newtonsoft.Json;

namespace Avira.Domain.Exporters;

public class JSONExportStrategy : IExportStrategy
{
    // Design pattern: Strategy
    public string ExportSprint(Sprint sprint)
    {
        var output = JsonConvert.SerializeObject("Sprint Time span from:  " + sprint.StartDate + " to " + sprint.EndDate, Formatting.Indented);
        return output;
    }

    public string ExportBacklogItem(BacklogItem backlogItem)
    {
        var output = JsonConvert.SerializeObject("-BacklogItem: " + backlogItem.Name + ", with description: " + backlogItem.Description + "assigned developer: " + backlogItem.Developer.Name, Formatting.Indented);
        return output;
    }

    public string ExportComment(Comment comment)
    {
        var output = JsonConvert.SerializeObject("--Comment: " + comment.Text, Formatting.Indented);
        return output;
    }

    public string ExportActivity(Activity activity)
    {
        var output = JsonConvert.SerializeObject("--Activity: " + activity.Name, Formatting.Indented);
        return output;
    }

    public string ExportProductBacklog(ProductBacklog productBacklog)
    {
        var output = JsonConvert.SerializeObject("ProductBacklog: " + productBacklog.Id + " From sprint: " + productBacklog.Sprint?.Name, Formatting.Indented);
        return output;
    }
}