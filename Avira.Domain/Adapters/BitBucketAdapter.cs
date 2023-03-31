namespace Avira.Domain.Adapters;

public class BitBucketAdapter : IVersionControl
{
    public void Commit()
    {
        Console.WriteLine("Committing to Bitbucket!");
    }

    public void Push()
    {
        Console.WriteLine("Pushing to Bitbucket!");
    }

    public void Pull()
    {
        Console.WriteLine("Pulling from Bitbucket!");
    }
}