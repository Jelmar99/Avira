using Avira.Domain.Interfaces;

namespace Avira.Domain.Adapters;

public class GithubAdapter : IVersionControl
{
    // Design pattern: Adapter
    public void Commit()
    {
        Console.WriteLine("Committing to Github!");
    }

    public void Push()
    {
        Console.WriteLine("Pushing to Github!");
    }

    public void Pull()
    {
        Console.WriteLine("Pulling from Github!");
    }
    
}