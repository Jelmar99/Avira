using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class Comment : IExport
{
    public Guid Id { get; }
    public string Text { get; }
    public List<Comment> Replies { get; set; }

    public Comment(Guid id, string text)
    {
        Id = id;
        Text = text;
        Replies = new List<Comment>();
    }
    public void ReplyToComment(Comment comment)
    {
        Replies.Add(comment);
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitComment(this);
    }
}