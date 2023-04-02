using Avira.Domain.Interfaces;

namespace Avira.Domain.Adapters;

public class BitBucketAdapter : IVersionControl
{
    // Design pattern: Adapter
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