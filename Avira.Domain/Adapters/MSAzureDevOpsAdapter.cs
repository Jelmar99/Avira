namespace Avira.Domain.Adapters;

public class MSAzureDevOpsAdapter : IVersionControl
{
    public void Commit()
    {
        Console.WriteLine("Committing to Azure DevOps!");
    }

    public void Push()
    {
        Console.WriteLine("Pushing to Azure DevOps!");
    }

    public void Pull()
    {
        Console.WriteLine("Pulling from Azure DevOps!");
    }
}