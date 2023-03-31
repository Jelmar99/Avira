namespace Avira.Domain.Adapters;

public class AWSCodeAdapter : IVersionControl
{
    public void Commit()
    {
        Console.WriteLine("Committing to AWS!");
    }

    public void Push()
    {
        Console.WriteLine("Pushing to AWS!");
    }

    public void Pull()
    {
        Console.WriteLine("Pulling from AWS!");
    }
}