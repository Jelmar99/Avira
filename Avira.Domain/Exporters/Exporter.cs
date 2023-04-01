﻿using Avira.Domain.Interfaces;

namespace Avira.Domain;

public class Exporter : IVisitor
{
    public IExportStrategy ExportStrategy { get; set; }
    
    public Exporter(IExportStrategy exportStrategy)
    {
        ExportStrategy = exportStrategy;
    }
    public void VisitSprint(Sprint sprint)
    {
        ExportStrategy.ExportSprint(sprint);
        foreach (var item in sprint.GetBacklogItems())
        {
            item.Accept(this);
        }
    }

    public void VisitBacklogItem(BacklogItem backlogItem)
    {
        ExportStrategy.ExportBacklogItem(backlogItem);
        foreach (var comment in backlogItem.Comments)
        {
            comment.Accept(this);
        }

        foreach (var activity in backlogItem.Activities)
        {
            activity.Accept(this);
        }
    }

    public void VisitComment(Comment comment)
    {
        ExportStrategy.ExportComment(comment);
    }

    public void VisitActivity(Activity activity)
    {
        ExportStrategy.ExportActivity(activity);
    }
}