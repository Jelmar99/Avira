namespace Avira.Domain.Interfaces;

public interface IExport
{
    void Accept(IVisitor visitor);
}