namespace Avira.Domain.Interfaces;

public interface IVersionControl
{
    // Design pattern: Adapter
    void Commit();
    void Push();
    void Pull();
}