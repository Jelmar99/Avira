using Avira.Domain.Interfaces;
using Avira.Domain.VersionControl;

namespace Avira.Domain.Adapters;

public class GitLabAdapter : IVersionControl
{
    // Design pattern: Adapter
    private readonly GitLabService _service;

    public GitLabAdapter()
    {
        _service = new GitLabService();
    }

    public void Commit()
    {
        _service.CommitToGitLab();
    }

    public void Push()
    {
        _service.GitLabPush();
    }

    public void Pull()
    {
        _service.GitLabPull();
    }
}