using Avira.Domain;
using Avira.Domain.Builder;
using Avira.Domain.Notifications;

namespace Avira.Test;
[TestFixture]
public class SprintUnitTest
{
    [Test]
    public void SetGoal_Sprint_Finished()
    {
       //Arrange
       var devUser = new UserBuilder().setId(Guid.NewGuid()).setName("Bob").setEmail("Bob@company.com").setPhoneNr("06-87654321").setSlackUsername("@BobbyB").setRole(Role.Developer)
           .addNotificationPreference(NotificationPreferenceType.Email).addNotificationPreference(NotificationPreferenceType.Slack).addNotificationPreference(NotificationPreferenceType.WhatsApp).Build();
       var listDev = new List<User>{devUser};
       var sprint = new Sprint(new Guid(),"sprint 1", new DateTime(2023, 4, 10), new DateTime(2023, 5, 12), listDev);
       //Act
       sprint.IsRelease = true;
       //Assert
       Assert.IsTrue(sprint.IsRelease);
    }

    [Test]
    public void SetBaseAttributes_Sprint_BeforeStartDate()
    {
        //Arrange
        var devUser = new UserBuilder().setId(Guid.NewGuid()).setName("Bob").setEmail("Bob@company.com").setPhoneNr("06-87654321").setSlackUsername("@BobbyB").setRole(Role.Developer)
            .addNotificationPreference(NotificationPreferenceType.Email).addNotificationPreference(NotificationPreferenceType.Slack).addNotificationPreference(NotificationPreferenceType.WhatsApp).Build();
        var listDev = new List<User>{devUser};
        var sprint = new Sprint(new Guid(),"sprint 1", new DateTime(2023, 4, 10), new DateTime(2023, 5, 12), listDev);
        //Act
        sprint.SetName("Sprint 2");
        sprint.SetStartDate(new DateTime(2023, 4,20));
        sprint.SetEndDate(new DateTime(2023, 7,28));
        sprint.SetStatus(Status.Ongoing);
        Assert.Multiple(() =>
        {
            //Assert
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
        var devUser = new UserBuilder().setId(Guid.NewGuid()).setName("Bob").setEmail("Bob@company.com")
            .setPhoneNr("06-87654321").setSlackUsername("@BobbyB").setRole(Role.Developer)
            .addNotificationPreference(NotificationPreferenceType.Email)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .addNotificationPreference(NotificationPreferenceType.WhatsApp).Build();
        var listDev = new List<User> { devUser };
        var sprint = new Sprint(new Guid(), "sprint 1", new DateTime(2023, 2, 10), new DateTime(2023, 3, 12), listDev);
        sprint.CheckIfFinished();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.Throws<Exception>(()=>sprint.SetName("Sprint 2"));
            Assert.Throws<Exception>(()=>sprint.SetStartDate(new DateTime(2023, 4, 20)));
            Assert.Throws<Exception>(()=>sprint.SetEndDate(new DateTime(2023, 7, 28)));
            Assert.Throws<Exception>(()=>sprint.SetStatus(Status.Ongoing));
        });
    }

    [Test]
    public void SetStatus_Sprint_Finished()
    {
        //Arrange
        var devUser = new UserBuilder().setId(Guid.NewGuid()).setName("Bob").setEmail("Bob@company.com")
            .setPhoneNr("06-87654321").setSlackUsername("@BobbyB").setRole(Role.Developer)
            .addNotificationPreference(NotificationPreferenceType.Email)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .addNotificationPreference(NotificationPreferenceType.WhatsApp).Build();
        var listDev = new List<User> { devUser };
        var sprint = new Sprint(new Guid(), "sprint 1", new DateTime(2023, 6, 10), new DateTime(2023, 7, 12), listDev);
        //Act
        sprint.SetStatus(Status.Finished);
        //Assert
        Assert.That(sprint.Status, Is.EqualTo(Status.Finished));
    }
    [Test]
    public void SetStatus_Sprint_Ongoing()
    {
        //Arrange
        var devUser = new UserBuilder().setId(Guid.NewGuid()).setName("Bob").setEmail("Bob@company.com")
            .setPhoneNr("06-87654321").setSlackUsername("@BobbyB").setRole(Role.Developer)
            .addNotificationPreference(NotificationPreferenceType.Email)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .addNotificationPreference(NotificationPreferenceType.WhatsApp).Build();
        var listDev = new List<User> { devUser };
        var sprint = new Sprint(new Guid(), "sprint 1", new DateTime(2023, 6, 10), new DateTime(2023, 7, 12), listDev);
        //Act
        sprint.SetStatus(Status.Ongoing);
        //Assert
        Assert.That(sprint.Status, Is.EqualTo(Status.Ongoing));
    }
    [Test]
    public void SetStatus_Sprint_NotYetStarted()
    {
        //Arrange
        var devUser = new UserBuilder().setId(Guid.NewGuid()).setName("Bob").setEmail("Bob@company.com")
            .setPhoneNr("06-87654321").setSlackUsername("@BobbyB").setRole(Role.Developer)
            .addNotificationPreference(NotificationPreferenceType.Email)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .addNotificationPreference(NotificationPreferenceType.WhatsApp).Build();
        var listDev = new List<User> { devUser };
        var sprint = new Sprint(new Guid(), "sprint 1", new DateTime(2023, 6, 10), new DateTime(2023, 7, 12), listDev);
        //Act
        sprint.SetStatus(Status.NotYetStarted);
        //Assert
        Assert.That(sprint.Status, Is.EqualTo(Status.NotYetStarted));
    }
    
    [Test]
    public void CheckIfFinished_SprintEndDatePassed_StatusSetToFinished()
    {
        // Arrange
        Guid sprintId = Guid.NewGuid();
        string sprintName = "Sprint 1";
        DateTime startDate = DateTime.Now.AddDays(-14);
        DateTime endDate = DateTime.Now.AddDays(-7);
        var developers = new List<User>();

        var sprint = new Sprint(sprintId, sprintName, startDate, endDate, developers);

        // Act
        sprint.CheckIfFinished();

        // Assert
        Assert.That(sprint.Status, Is.EqualTo(Status.Finished));
    }

    [Test]
    public void InitializeRelease_ScrumMaster_CanInitializeRelease()
    {
        // Arrange
        Guid sprintId = Guid.NewGuid();
        string sprintName = "Sprint 1";
        DateTime startDate = DateTime.Now.AddDays(-14);
        DateTime endDate = DateTime.Now.AddDays(-7);
        var developers = new List<User>();
        var scrumMaster = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Piet")
            .setEmail("Piet@mail.com")
            .setPhoneNr("06-12345678")
            .setSlackUsername("@PietjePuk")
            .setRole(Role.ScrumMaster)
            .addNotificationPreference(NotificationPreferenceType.Email)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .addNotificationPreference(NotificationPreferenceType.WhatsApp)
            .Build();

        var sprint = new Sprint(sprintId, sprintName, startDate, endDate, developers);
        sprint.ScrumMaster = scrumMaster;
        sprint.CheckIfFinished();

        // Act
        sprint.InitializeRelease(scrumMaster);

        // Assert
        Assert.IsTrue(sprint.IsRelease);
    }

    [Test]
    public void InitializeRelease_Developer_ThrowsException()
    {
        // Arrange
        Guid sprintId = Guid.NewGuid();
        string sprintName = "Sprint 1";
        DateTime startDate = DateTime.Now.AddDays(-14);
        DateTime endDate = DateTime.Now.AddDays(-7);
        var developers = new List<User>();
        var devUser = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Bob")
            .setEmail("Bob@company.com")
            .setPhoneNr("06-87654321")
            .setSlackUsername("@BobbyB")
            .setRole(Role.Developer)
            .addNotificationPreference(NotificationPreferenceType.Email)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .addNotificationPreference(NotificationPreferenceType.WhatsApp)
            .Build();
        
        var sprint = new Sprint(sprintId, sprintName, startDate, endDate, developers);
        sprint.CheckIfFinished();

        // Act & Assert
        Assert.Throws<Exception>(() => sprint.InitializeRelease(devUser));
    }
}