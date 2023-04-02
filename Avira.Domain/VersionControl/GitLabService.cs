namespace Avira.Domain.VersionControl;

public static class GitLabService
{
    // Design pattern: Adapter
    public static void CommitToGitLab()
    {
        Console.WriteLine("Committing to Gitlab!");
    }

    public static void GitLabPush()
    {
        Console.WriteLine("Pushing to Gitlab!");
    }

    public static void GitLabPull()
    {
        Console.WriteLine("Pulling from Gitlab!");
    }
}