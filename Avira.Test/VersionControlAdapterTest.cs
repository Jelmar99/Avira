using Avira.Domain;
using Avira.Domain.Adapters;
using Avira.Domain.Builder;
using Avira.Domain.Notifications;

namespace Avira.Test;

[TestFixture]
public class VersionControlAdapterTest
{
    private User _dev1 = null!;
    private User _scrumMaster = null!;
    private User _productOwner = null!;
    private ProductBacklog _productBacklog = null!;
    private Sprint _sprint = null!;

    private Project _githubAdapterProject = null!;
    private Project _gitLabAdapterProject = null!;
    private Project _awsCodeAdapteProject = null!;
    private Project _bitBucketAdapProject = null!;
    private Project _msAzureDevOpsProject = null!;

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
                "Committing to Github!\n" +
                "Committing to Gitlab!\n" +
                "Committing to AWS!\n" +
                "Committing to Bitbucket!\n" +
                "Committing to Azure DevOps!\n"));
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
                "Pushing to Github!\n" +
                "Pushing to Gitlab!\n" +
                "Pushing to AWS!\n" +
                "Pushing to Bitbucket!\n" +
                "Pushing to Azure DevOps!\n"));
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
                "Pulling from Github!\n" +
                "Pulling from Gitlab!\n" +
                "Pulling from AWS!\n" +
                "Pulling from Bitbucket!\n" +
                "Pulling from Azure DevOps!\n"));
    }
}