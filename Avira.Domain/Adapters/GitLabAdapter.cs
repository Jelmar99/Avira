namespace Avira.Domain.Adapters;

public class GitLabAdapter : IVersionControl
{
    public void Commit()
    {
        Console.WriteLine("Committing to Gitlab!");
    }

    public void Push()
    {
        Console.WriteLine("Pushing to Gitlab!");
    }

    public void Pull()
    {
        Console.WriteLine("Pulling from Gitlab!");
    }
}