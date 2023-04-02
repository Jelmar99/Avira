using Avira.Domain;
using Avira.Domain.Builder;
using Avira.Domain.Notifications;

namespace Avira.Test;

[TestFixture]
public class NotificationTest
{
    private User _dev1 = null!;
    private User _tester = null!;
    private User _scrumMaster = null!;
    private Sprint _sprint = null!;

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
}