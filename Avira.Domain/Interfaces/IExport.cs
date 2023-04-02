namespace Avira.Domain.Interfaces;

//Visitor pattern, this accept method should be given to all classes that can be visited and thus exported
public interface IExport
{
    string Accept(IVisitor visitor);
}