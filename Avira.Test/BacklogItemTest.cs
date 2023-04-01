using Avira.Domain;
using Avira.Domain.Adapters;
using Avira.Domain.Builder;
using Avira.Domain.Notifications;

namespace Avira.Test;

[TestFixture]
public class BacklogItemTest
{
    private User _dev1;
    private User _dev2;
    private User _tester;
    private User _scrumMaster;
    private ProductBacklog _productBacklog;
    private Sprint _sprint;

    [SetUp]
    public void Setup()
    {
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

        var tomorrow = DateTime.Now.AddDays(1);
        _sprint = new Sprint(Guid.NewGuid(), "TestSprint", tomorrow, tomorrow.AddDays(14), new List<User> { _dev1 },
            _scrumMaster);

        //Todo: fix
        _productBacklog = new ProductBacklog(Guid.NewGuid(), _sprint);
    }

    [Test]
    public void DefaultBacklogItemPhaseTodo_Ok()
    {
        //Arrange
        //N/A

        //Act
        var backlogItem = new BacklogItem(Guid.NewGuid(), "TestBacklogItem", "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);

        //Assert
        Assert.That(backlogItem.Phase, Is.EqualTo(BacklogItemPhase.Todo));
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
    public void BacklogItemWithoutActivities_Ok()
    {
        var backlogItem = new BacklogItem(Guid.NewGuid(), "TestBacklogItem", "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);

        //Act
        //AddActivity not called

        //Assert
        Assert.That(backlogItem.Activities, Is.Empty);
    }

    [Test]
    public void ActivityAddedToBacklogItem_Ok()
    {
        //Arrange
        var backlogItem = new BacklogItem(Guid.NewGuid(), "TestBacklogItem", "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);
        var activity1 = new Activity(Guid.NewGuid(), "TestActivity1");
        var activity2 = new Activity(Guid.NewGuid(), "TestActivity2");
        var activity3 = new Activity(Guid.NewGuid(), "TestActivity3");

        //Act
        backlogItem.AddActivity(activity1);
        backlogItem.AddActivity(activity2);

        //Assert
        Assert.That(backlogItem.Activities, Does.Contain(activity1));
        Assert.That(backlogItem.Activities, Does.Contain(activity2));
        Assert.That(backlogItem.Activities, Does.Not.Contain(activity3));
    }

    [Test]
    public void ActivityRemovedFromBacklogItem_Ok()
    {
        //Arrange
        var backlogItem = new BacklogItem(Guid.NewGuid(), "TestBacklogItem", "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);
        var activity1 = new Activity(Guid.NewGuid(), "TestActivity1");
        var activity2 = new Activity(Guid.NewGuid(), "TestActivity2");
        var activity3 = new Activity(Guid.NewGuid(), "TestActivity3");

        backlogItem.AddActivity(activity1);
        backlogItem.AddActivity(activity2);
        backlogItem.AddActivity(activity3);

        //Act
        backlogItem.RemoveActivity(activity2);

        //Assert
        Assert.That(backlogItem.Activities, Does.Contain(activity1));
        Assert.That(backlogItem.Activities, Does.Not.Contain(activity2));
        Assert.That(backlogItem.Activities, Does.Contain(activity3));
    }

    [Test]
    public void BacklogItemAddedToProductBacklog_Ok()
    {
        //Arrange
        var backlogItem1 = new BacklogItem(Guid.NewGuid(), "TestBacklogItem1", "TestBacklogItemDescription1", 1, 10,
            _sprint, _dev1, _tester);
        var backlogItem2 = new BacklogItem(Guid.NewGuid(), "TestBacklogItem2", "TestBacklogItemDescription2", 1, 30,
            _sprint, _dev1, _tester);

        //Act
        _productBacklog.AddBacklogItem(backlogItem1);
        _productBacklog.AddBacklogItem(backlogItem2);

        //Assert
        Assert.That(_productBacklog.GetBacklogItems(), Does.Contain(backlogItem1));
        Assert.That(_productBacklog.GetBacklogItems(), Does.Contain(backlogItem2));
    }

    [Test]
    public void BacklogItemRemovedFromProductBacklog_Ok()
    {
        //Arrange
        var backlogItem1 = new BacklogItem(Guid.NewGuid(), "TestBacklogItem1", "TestBacklogItemDescription1", 1, 10,
            _sprint, _dev1, _tester);
        var backlogItem2 = new BacklogItem(Guid.NewGuid(), "TestBacklogItem2", "TestBacklogItemDescription2", 1, 30,
            _sprint, _dev1, _tester);
        _productBacklog.AddBacklogItem(backlogItem1);
        _productBacklog.AddBacklogItem(backlogItem2);

        //Act
        _productBacklog.RemoveBacklogItem(backlogItem2);

        //Assert
        Assert.That(_productBacklog.GetBacklogItems(), Does.Contain(backlogItem1));
        Assert.That(_productBacklog.GetBacklogItems(), Does.Not.Contain(backlogItem2));
    }

    [Test]
    public void ProductBacklogOrdered_Ok()
    {
        //Arrange
        var backlogItem1 = new BacklogItem(Guid.NewGuid(), "TestBacklogItem1", "TestBacklogItemDescription1", 1, 10,
            _sprint, _dev1, _tester);
        var backlogItem2 = new BacklogItem(Guid.NewGuid(), "TestBacklogItem2", "TestBacklogItemDescription2", 7, 30,
            _sprint, _dev1, _tester);
        var backlogItem3 = new BacklogItem(Guid.NewGuid(), "TestBacklogItem3", "TestBacklogItemDescription3", 3, 5,
            _sprint, _dev1, _tester);
        _productBacklog.AddBacklogItem(backlogItem1);
        _productBacklog.AddBacklogItem(backlogItem2);
        _productBacklog.AddBacklogItem(backlogItem3);

        //Act
        var productBacklogItems = _productBacklog.GetBacklogItems();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(productBacklogItems[0], Is.EqualTo(backlogItem2));
            Assert.That(productBacklogItems[1], Is.EqualTo(backlogItem1));
            Assert.That(productBacklogItems[2], Is.EqualTo(backlogItem3));
        });
    }

    [Test]
    public void BacklogItemPhases_Exist()
    {
        //Arrange
        //N/A

        //Act
        var backlogItemPhases = Enum.GetValues(typeof(BacklogItemPhase))
            .Cast<BacklogItemPhase>()
            .Select(v => v.ToString())
            .ToList();

        //Assert
        Assert.That(backlogItemPhases, Does.Contain("Todo"));
        Assert.That(backlogItemPhases, Does.Contain("Doing"));
        Assert.That(backlogItemPhases, Does.Contain("ReadyForTesting"));
        Assert.That(backlogItemPhases, Does.Contain("Testing"));
        Assert.That(backlogItemPhases, Does.Contain("Tested"));
        Assert.That(backlogItemPhases, Does.Contain("Done"));
    }

    [Test]
    public void BacklogItemPhaseUpdate_Ok()
    {
        //Arrange
        //To read Console.WriteLine
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        var backlogItem = new BacklogItem(new Guid("07d47cf6-06c5-41cc-b5b0-f73577b11787"), "TestBacklogItem",
            "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);

        //Act - Updating Phase
        backlogItem.UpdatePhase(BacklogItemPhase.Tested, _tester);

        //Assert
        Assert.That(backlogItem.Phase, Is.EqualTo(BacklogItemPhase.Tested));

        //Act - Reverting Phase
        backlogItem.UpdatePhase(BacklogItemPhase.Todo, _dev1);

        //Assert
        Assert.That(backlogItem.Phase, Is.EqualTo(BacklogItemPhase.Todo));

        //Notification to ScrumMaster
        //Assert
        var consoleOutput = stringWriter.ToString();
        Assert.That(consoleOutput,
            Is.EqualTo(
                "Notification for Master	Slack	: 'An existing Backlog Item's Phase has been reverted to 'Todo': TestBacklogItem, (07d47cf6-06c5-41cc-b5b0-f73577b11787) by Dev'	To: @Master\r\n"));
    }

    [Test]
    public void BacklogItemPhaseDoneWithActivitiesNotDone_NotOk()
    {
        //Arrange
        var backlogItem = new BacklogItem(Guid.NewGuid(), "TestBacklogItem", "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);
        var activity1 = new Activity(Guid.NewGuid(), "TestActivity1");
        var activity2 = new Activity(Guid.NewGuid(), "TestActivity2");
        var activity3 = new Activity(Guid.NewGuid(), "TestActivity3");
        backlogItem.AddActivity(activity1);
        backlogItem.AddActivity(activity2);
        backlogItem.AddActivity(activity3);
        activity1.Done = true;
        activity2.Done = false; // False!
        activity3.Done = true;

        //Act
        //Assert
        var ex = Assert.Throws<Exception>(() => backlogItem.UpdatePhase(BacklogItemPhase.Done, _dev1));
        Assert.That(ex?.Message, Is.EqualTo("Not every activity contained in this Backlog Item is Done!"));
    }

    [Test]
    public void BacklogItemPhaseDoneWithActivitiesDone_Ok()
    {
        //Arrange
        var backlogItem = new BacklogItem(Guid.NewGuid(), "TestBacklogItem", "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);
        var activity1 = new Activity(Guid.NewGuid(), "TestActivity1");
        var activity2 = new Activity(Guid.NewGuid(), "TestActivity2");
        var activity3 = new Activity(Guid.NewGuid(), "TestActivity3");
        backlogItem.AddActivity(activity1);
        backlogItem.AddActivity(activity2);
        backlogItem.AddActivity(activity3);
        activity1.Done = true;
        activity2.Done = true;
        activity3.Done = true;

        //Act
        backlogItem.UpdatePhase(BacklogItemPhase.Done, _dev1);

        //Assert
        Assert.That(backlogItem.Phase, Is.EqualTo(BacklogItemPhase.Done));
    }

    [Test]
    public void BacklogItemHasOneDeveloper_Ok()
    {
        //Arrange
        var backlogItem = new BacklogItem(Guid.NewGuid(), "TestBacklogItem", "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);

        //Act
        backlogItem.Developer = _dev2;

        //Assert
        Assert.That(backlogItem.Developer, Is.EqualTo(_dev2));
    }

    [Test]
    public void BacklogItemReadyForTestingNotification_Ok()
    {
        //Arrange
        //To read Console.WriteLine
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        var backlogItem = new BacklogItem(new Guid("07d47cf6-06c5-41cc-b5b0-f73577b11788"), "TestBacklogItem",
            "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);

        //Act
        backlogItem.UpdatePhase(BacklogItemPhase.ReadyForTesting, _dev1);

        //Assert
        var consoleOutput = stringWriter.ToString();
        Assert.That(consoleOutput,
            Is.EqualTo(
                "Notification for Tester	Slack	: 'A new Backlog Item has become ready to test: TestBacklogItem (07d47cf6-06c5-41cc-b5b0-f73577b11788)'	To: @Tester\r\n" +
                "Notification for Tester	Email	: 'A new Backlog Item has become ready to test: TestBacklogItem (07d47cf6-06c5-41cc-b5b0-f73577b11788)'	To: Tester@company.com\r\n"));
    }

    [Test]
    public void BacklogItemPhaseToDoneByDeveloper_Ok()
    {
        //Arrange
        var backlogItem = new BacklogItem(Guid.NewGuid(), "TestBacklogItem",
            "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);

        //Act
        backlogItem.UpdatePhase(BacklogItemPhase.Done, _dev1);

        //Assert
        Assert.That(backlogItem.Phase, Is.EqualTo(BacklogItemPhase.Done));
    }

    [Test]
    public void BacklogItemPhaseToDoneByNotDeveloper_NotOk()
    {
        //Arrange
        var backlogItem = new BacklogItem(Guid.NewGuid(), "TestBacklogItem",
            "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);

        //Act
        //Assert
        var ex = Assert.Throws<Exception>(() => backlogItem.UpdatePhase(BacklogItemPhase.Done, _tester));
        Assert.That(ex?.Message, Is.EqualTo("You must be a Developer to update the Phase of a Backlog Item to 'Done'"));
    }

    [Test]
    public void SendNotificationBacklogItem_Ok()
    {
        //Arrange
        //To read Console.WriteLine
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        var backlogItem = new BacklogItem(Guid.NewGuid(), "TestBacklogItem",
            "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);
        backlogItem.AddListener(_dev1);
        backlogItem.AddListener(_tester);
        var notification = new Notification("This notification is a test.");

        //Act
        backlogItem.SendNotification(notification);

        //Assert
        var consoleOutput = stringWriter.ToString();
        Assert.That(consoleOutput,
            Is.EqualTo(
                "Notification for Dev	Slack	: 'This notification is a test.'	To: @Developer\r\n" +
                "Notification for Tester	Slack	: 'This notification is a test.'	To: @Tester\r\n" +
                "Notification for Tester	Email	: 'This notification is a test.'	To: Tester@company.com\r\n"));
    }

    [Test]
    public void BacklogItemPhaseRevertedToDoing_NotOk()
    {
        //Arrange
        var backlogItem = new BacklogItem(Guid.NewGuid(), "TestBacklogItem",
            "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);
        backlogItem.UpdatePhase(BacklogItemPhase.Tested, _dev1);

        //Act
        //Assert
        var ex = Assert.Throws<Exception>(() => backlogItem.UpdatePhase(BacklogItemPhase.Doing, _dev1));
        Assert.That(ex?.Message, Is.EqualTo("The Phase of a Backlog Item is not allowed to be set back to 'Doing'"));
    }

    [Test]
    public void BacklogItemPhaseUpdateSamePhase_NotOk()
    {
        //Arrange
        var backlogItem = new BacklogItem(Guid.NewGuid(), "TestBacklogItem",
            "TestBacklogItemDescription", 1, 10,
            _sprint, _dev1, _tester);

        //Act
        //Assert
        var ex = Assert.Throws<Exception>(() => backlogItem.UpdatePhase(BacklogItemPhase.Todo, _dev1));
        Assert.That(ex?.Message, Is.EqualTo("This Backlog Item already has this phase."));
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

    [Test]
    public void ProjectHasProductOwner()
    {
        //Arrange
        var productOwner = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("PO")
            .setEmail("PO@company.com")
            .setPhoneNr("06-00000004")
            .setSlackUsername("@PO")
            .setRole(Role.ProductOwner)
            .addNotificationPreference(NotificationPreferenceType.Email)
            .Build();
        var proj = new Project(Guid.NewGuid(), _productBacklog, new GithubAdapter(), productOwner);

        //Act
        //N/A

        //Assert
        Assert.That(proj.ProductOwner.Role, Is.EqualTo(Role.ProductOwner));
    }
}