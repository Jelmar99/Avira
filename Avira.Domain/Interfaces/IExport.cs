namespace Avira.Domain.Interfaces;

public interface IExport
{
    // Design pattern: Visitor
    string Accept(IVisitor visitor);
}