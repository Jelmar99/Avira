using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class Comment : IExport
{
    private Guid _id;
    public string Text { get; }
    public List<Comment> Replies { get; }
    private BacklogItem BacklogItem { get; }

    public Comment(Guid id, string text, BacklogItem backlogItem)
    {
        _id = id;
        Text = text;
        BacklogItem = backlogItem;
        Replies = new List<Comment>();
    }
    public void ReplyToComment(Comment comment)
    {
        if (BacklogItem.Phase != BacklogItemPhase.Done)
        {
            Replies.Add(comment);
        }
        else
        {
            throw new Exception("You can't reply to a comment on a Backlog Item that is in the 'Done' phase.");
        }
    }

    public string Accept(IVisitor visitor)
    {
        // Design pattern: Visitor
        return visitor.VisitComment(this);
    }
}