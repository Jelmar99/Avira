﻿using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class Comment : IExport
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public List<Comment> Comments { get; set; }

    public Comment(Guid id, string text)
    {
        Id = id;
        Text = text;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitComment(this);
    }
}