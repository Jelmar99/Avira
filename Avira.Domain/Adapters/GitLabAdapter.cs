using Avira.Domain.VersionControl;

namespace Avira.Domain.Adapters;

public class GitLabAdapter : IVersionControl
{
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