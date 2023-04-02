using Avira.Domain;
using Avira.Domain.Adapters;
using Avira.Domain.Builder;
using Avira.Domain.Notifications;

namespace Avira.Test;

[TestFixture]
public class VersionControlAdapterTest
{
    private User _dev1;
    private User _scrumMaster;
    private User _productOwner;
    private ProductBacklog _productBacklog;
    private Sprint _sprint;

    private Project _githubAdapterProject;
    private Project _gitLabAdapterProject;
    private Project _awsCodeAdapteProject;
    private Project _bitBucketAdapProject;
    private Project _msAzureDevOpsProject;

    [SetUp]
    public void Setup()
    {
        // var standardOutput = new StreamWriter(Console.OpenStandardOutput());
        // standardOutput.AutoFlush = true;
        // Console.SetOut(standardOutput);

        _dev1 = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Dev")
            .setEmail("Dev@company.com")
            .setPhoneNr("06-00000000")
            .setSlackUsername("@Developer")
            .setRole(Role.Developer)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .Build();

        _scrumMaster = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Master")
            .setEmail("Master@company.com")
            .setSlackUsername("@Master")
            .setRole(Role.ScrumMaster)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .Build();

        //Arrange
        _productOwner = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("PO")
            .setEmail("PO@company.com")
            .setPhoneNr("06-00000004")
            .setSlackUsername("@PO")
            .setRole(Role.ProductOwner)
            .addNotificationPreference(NotificationPreferenceType.Email)
            .Build();

        var tomorrow = DateTime.Now.AddDays(1);
        _sprint = new Sprint(Guid.NewGuid(), "TestSprint", tomorrow, tomorrow.AddDays(14), new List<User> { _dev1 },
            _scrumMaster);

        //Todo: fix
        _productBacklog = new ProductBacklog(Guid.NewGuid(), _sprint);

        _githubAdapterProject = new Project(Guid.NewGuid(), _productBacklog, new GithubAdapter(), _productOwner);
        _gitLabAdapterProject = new Project(Guid.NewGuid(), _productBacklog, new GitLabAdapter(), _productOwner);
        _awsCodeAdapteProject = new Project(Guid.NewGuid(), _productBacklog, new AWSCodeAdapter(), _productOwner);
        _bitBucketAdapProject = new Project(Guid.NewGuid(), _productBacklog, new BitBucketAdapter(), _productOwner);
        _msAzureDevOpsProject = new Project(Guid.NewGuid(), _productBacklog, new MSAzureDevOpsAdapter(), _productOwner);
    }

    [Test]
    public void VersionControlAdaptersCommit_Ok()
    {
        //Arrange
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        _githubAdapterProject.Commit();
        _gitLabAdapterProject.Commit();
        _awsCodeAdapteProject.Commit();
        _bitBucketAdapProject.Commit();
        _msAzureDevOpsProject.Commit();

        //Assert
        var consoleOutput = stringWriter.ToString();
        Assert.That(consoleOutput,
            Is.EqualTo(
                "Committing to Github!\r\n" +
                "Committing to Gitlab!\r\n" +
                "Committing to AWS!\r\n" +
                "Committing to Bitbucket!\r\n" +
                "Committing to Azure DevOps!\r\n"));
    }

    [Test]
    public void VersionControlAdaptersPush_Ok()
    {
        //Arrange
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        _githubAdapterProject.Push();
        _gitLabAdapterProject.Push();
        _awsCodeAdapteProject.Push();
        _bitBucketAdapProject.Push();
        _msAzureDevOpsProject.Push();

        //Assert
        var consoleOutput = stringWriter.ToString();
        Assert.That(consoleOutput,
            Is.EqualTo(
                "Pushing to Github!\r\n" +
                "Pushing to Gitlab!\r\n" +
                "Pushing to AWS!\r\n" +
                "Pushing to Bitbucket!\r\n" +
                "Pushing to Azure DevOps!\r\n"));
    }

    [Test]
    public void VersionControlAdaptersPull_Ok()
    {
        //Arrange
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        //Act
        _githubAdapterProject.Pull();
        _gitLabAdapterProject.Pull();
        _awsCodeAdapteProject.Pull();
        _bitBucketAdapProject.Pull();
        _msAzureDevOpsProject.Pull();

        //Assert
        var consoleOutput = stringWriter.ToString();
        Assert.That(consoleOutput,
            Is.EqualTo(
                "Pulling from Github!\r\n" +
                "Pulling from Gitlab!\r\n" +
                "Pulling from AWS!\r\n" +
                "Pulling from Bitbucket!\r\n" +
                "Pulling from Azure DevOps!\r\n"));
    }
}