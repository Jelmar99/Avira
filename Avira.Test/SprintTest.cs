using Avira.Domain;
using Avira.Domain.Builder;
using Avira.Domain.Notifications;

namespace Avira.Test;

[TestFixture]
public class SprintTest
{
    private User _dev1 = null!;
    private User _dev2 = null!;
    private User _tester = null!;
    private User _scrumMaster = null!;
    private Sprint _sprint = null!;
    public List<User> _listDev;

    [SetUp]
    public void Setup()
    {
        var standardOutput = new StreamWriter(Console.OpenStandardOutput());
        standardOutput.AutoFlush = true;
        Console.SetOut(standardOutput);

        _dev1 = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Dev")
            .setEmail("Dev@company.com")
            .setPhoneNr("06-00000000")
            .setSlackUsername("@Developer")
            .setRole(Role.Developer)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .Build();

        _dev2 = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Dev2")
            .setEmail("Dev2@company.com")
            .setPhoneNr("06-00000002")
            .setSlackUsername("@Developer2")
            .setRole(Role.Developer)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .Build();

        _tester = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Tester")
            .setEmail("Tester@company.com")
            .setPhoneNr("06-00000000")
            .setSlackUsername("@Tester")
            .setRole(Role.Tester)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .addNotificationPreference(NotificationPreferenceType.Email)
            .Build();

        _scrumMaster = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Master")
            .setEmail("Master@company.com")
            .setSlackUsername("@Master")
            .setRole(Role.ScrumMaster)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .Build();

        _listDev = new List<User>
        {
            _dev1,
            _dev2
        };

        var tomorrow = DateTime.Now.AddDays(1);
        _sprint = new Sprint(Guid.NewGuid(), "TestSprint", tomorrow, tomorrow.AddDays(14), new List<User> { _dev1 },
            _scrumMaster);
    }

    [Test]
    public void SetGoal_Sprint_Finished()
    {
        //Arrange
        var tomorrow = DateTime.Now.AddDays(1);
        var sprint = new Sprint(new Guid(), "sprint 1", tomorrow, tomorrow.AddDays(14), _listDev, _scrumMaster);

        //Act
        sprint.IsRelease = true;

        //Assert
        Assert.IsTrue(sprint.IsRelease);
    }

    [Test]
    public void SetBaseAttributes_Sprint_BeforeStartDate()
    {
        //Arrange
        var tomorrow = DateTime.Now.AddDays(1);
        var sprint = new Sprint(new Guid(), "sprint 1", tomorrow, tomorrow.AddDays(14), _listDev, _scrumMaster);

        //Act
        sprint.SetName("Sprint 2");
        sprint.SetStartDate(new DateTime(2023, 4, 20));
        sprint.SetEndDate(new DateTime(2023, 7, 28));
        sprint.SetStatus(Status.Ongoing);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(sprint.Name, Is.EqualTo("Sprint 2"));
            Assert.That(sprint.StartDate, Is.EqualTo(new DateTime(2023, 4, 20)));
            Assert.That(sprint.EndDate, Is.EqualTo(new DateTime(2023, 7, 28)));
            Assert.That(sprint.Status, Is.EqualTo(Status.Ongoing));
        });
    }

    [Test]
    public void SetBaseAttributes_Sprint_AfterStartDate()
    {
        //Arrange
        var sprint = new Sprint(new Guid(), "sprint 1", new DateTime(2023, 2, 10), new DateTime(2023, 3, 12), _listDev,
            _scrumMaster);
        sprint.CheckIfFinished();

        //Act
        //Assert
        Assert.Multiple(() =>
        {
            Assert.Throws<Exception>(() => sprint.SetName("Sprint 2"));
            Assert.Throws<Exception>(() => sprint.SetStartDate(new DateTime(2023, 4, 20)));
            Assert.Throws<Exception>(() => sprint.SetEndDate(new DateTime(2023, 7, 28)));
            Assert.Throws<Exception>(() => sprint.SetStatus(Status.Ongoing));
        });
    }

    [Test]
    public void SetStatus_Sprint_Finished()
    {
        //Arrange
        var tomorrow = DateTime.Now.AddDays(1);
        var sprint = new Sprint(new Guid(), "sprint 1", tomorrow, tomorrow.AddDays(14), _listDev, _scrumMaster);

        //Act
        sprint.SetStatus(Status.Finished);

        //Assert
        Assert.That(sprint.Status, Is.EqualTo(Status.Finished));
    }

    [Test]
    public void SetStatus_Sprint_Ongoing()
    {
        //Arrange
        var tomorrow = DateTime.Now.AddDays(1);
        var sprint = new Sprint(new Guid(), "sprint 1", tomorrow, tomorrow.AddDays(14), _listDev, _scrumMaster);

        //Act
        sprint.SetStatus(Status.Ongoing);

        //Assert
        Assert.That(sprint.Status, Is.EqualTo(Status.Ongoing));
    }

    [Test]
    public void SetStatus_Sprint_NotYetStarted()
    {
        //Arrange
        var tomorrow = DateTime.Now.AddDays(1);
        var sprint = new Sprint(new Guid(), "sprint 1", tomorrow, tomorrow.AddDays(14), _listDev, _scrumMaster);

        //Act
        sprint.SetStatus(Status.NotYetStarted);

        //Assert
        Assert.That(sprint.Status, Is.EqualTo(Status.NotYetStarted));
    }

    [Test]
    public void CheckIfFinished_SprintEndDatePassed_StatusSetToFinished()
    {
        // Arrange
        var startDate = DateTime.Now.AddDays(-14);
        var endDate = startDate.AddDays(7);
        var sprint = new Sprint(Guid.NewGuid(), "Sprint 1", startDate, endDate, _listDev, _scrumMaster);

        // Act
        sprint.CheckIfFinished();

        // Assert
        Assert.That(sprint.Status, Is.EqualTo(Status.Finished));
    }

    [Test]
    public void InitializeRelease_ScrumMaster_CanInitializeRelease()
    {
        // Arrange
        var startDate = DateTime.Now.AddDays(-14);
        var endDate = startDate.AddDays(7);
        var sprint = new Sprint(Guid.NewGuid(), "Sprint 1", startDate, endDate, _listDev, _scrumMaster);
        sprint.CheckIfFinished();

        // Act
        sprint.InitializeRelease(_scrumMaster);

        // Assert
        Assert.That(sprint.IsRelease, Is.True);
    }

    [Test]
    public void InitializeRelease_Developer_ThrowsException()
    {
        // Arrange
        var startDate = DateTime.Now.AddDays(-14);
        var endDate = startDate.AddDays(7);
        var sprint = new Sprint(Guid.NewGuid(), "Sprint 1", startDate, endDate, _listDev, _scrumMaster);
        sprint.CheckIfFinished();

        // Act
        // Assert
        Assert.Throws<Exception>(() => sprint.InitializeRelease(_dev1));
    }

    [Test]
    public void Send_Notification_OnSprintRelease()
    {
        // Arrange
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        var sprint = new Sprint(new Guid(), "sprint2", new DateTime(2023, 4, 2), new DateTime(2023, 4, 13), _listDev,
            _scrumMaster);
        var p = new Pipeline(sprint);

        // Act
        //TODO: Je test hier de console writeline van de sprint deploy waarom addlistener dev? Hoort die hier niet getest te worden? Als ik de naam van de test zo lees
        p.AddListener(_dev1);
        sprint.Deploy();

        // Assert
        var consoleOutput = stringWriter.ToString();
        Assert.That(consoleOutput,
            Is.EqualTo(
                "Executing Phase Sources\tSprint: sprint2 running from 2-4-2023 to 13-4-2023\r\nExecuting Phase Package\tSprint: sprint2 running from 2-4-2023 to 13-4-2023\r\nExecuting Phase Build\tSprint: sprint2 running from 2-4-2023 to 13-4-2023\r\nExecuting Phase Test\tSprint: sprint2 running from 2-4-2023 to 13-4-2023\r\nExecuting Phase Analyse\tSprint: sprint2 running from 2-4-2023 to 13-4-2023\r\nExecuting Phase Deploy\tSprint: sprint2 running from 2-4-2023 to 13-4-2023\r\nExecuting Phase Utility\tSprint: sprint2 running from 2-4-2023 to 13-4-2023\r\n"));
    }

    [Test]
    public void BacklogItemAddedToSprint_Ok()
    {
        //Arrange
        var backlogItem = new BacklogItem(Guid.NewGuid(), "TestBacklogItem", "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);

        //Act
        _sprint.AddBacklogItem(backlogItem);

        //Assert
        Assert.That(_sprint.GetBacklogItems(), Does.Contain(backlogItem));
    }

    [Test]
    public void SprintHasScrumMaster_Ok()
    {
        //Arrange
        //N/A

        //Act
        var scrumMaster = _sprint.ScrumMaster;

        //Assert
        Assert.That(scrumMaster.Role, Is.EqualTo(Role.ScrumMaster));
    }

    [Test]
    public void SprintHasMultipleDevelopers_Ok()
    {
        //Arrange
        //_dev1 already added

        //Act
        _sprint.Developers.Add(_dev2);

        //Assert
        Assert.That(_sprint.Developers, Has.Count.EqualTo(2));
        Assert.That(_sprint.Developers, Does.Contain(_dev1));
        Assert.That(_sprint.Developers, Does.Contain(_dev2));
    }
}