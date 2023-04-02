namespace Avira.Domain.VersionControl;

public class GitLabService
{
    // Design pattern: Adapter
    public void CommitToGitLab()
    {
        Console.WriteLine("Committing to Gitlab!");
    }

    public void GitLabPush()
    {
        Console.WriteLine("Pushing to Gitlab!");
    }

    public void GitLabPull()
    {
        Console.WriteLine("Pulling from Gitlab!");
    }
}