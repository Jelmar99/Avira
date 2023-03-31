namespace Avira.Domain;

public interface IVersionControl
{
    void Commit();
    void Push();
    void Pull();
}