using Avira.Domain.Interfaces;
using Avira.Domain.VersionControl;

namespace Avira.Domain.Adapters;

public class GitLabAdapter : IVersionControl
{
    // Design pattern: Adapter
    public void Commit()
    {
        GitLabService.CommitToGitLab();
    }

    public void Push()
    {
        GitLabService.GitLabPush();
    }

    public void Pull()
    {
        GitLabService.GitLabPull();
    }
}