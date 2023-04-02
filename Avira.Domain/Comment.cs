using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class Comment : IExport
{
    public Guid Id { get; }
    public string Text { get; }
    public List<Comment> Replies { get; set; }
    private BacklogItem BacklogItem { get; }

    public Comment(Guid id, string text, BacklogItem backlogItem)
    {
        Id = id;
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
        return visitor.VisitComment(this);
    }
}