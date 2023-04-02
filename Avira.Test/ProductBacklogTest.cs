using Avira.Domain;
using Avira.Domain.Builder;
using Avira.Domain.Notifications;

namespace Avira.Test;

[TestFixture]
public class ProductBacklogTest
{
    private User _dev1;
    private User _tester;
    private User _scrumMaster;
    private ProductBacklog _productBacklog;
    private Sprint _sprint;

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

        //Todo: fix
        _productBacklog = new ProductBacklog(Guid.NewGuid(), _sprint);
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
}