using Avira.Domain;
using Avira.Domain.Builder;
using Avira.Domain.Notifications;

namespace Avira.Test;

[TestFixture]
public class ExportUnitTest
{
    [Test]
    public void Export_Sprint_Report_InPlainTextFormat()
    {
        //Act
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

        var testUser = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Kees")
            .setEmail("Kees@company.com")
            .setSlackUsername("@KeesP")
            .setRole(Role.Tester)
            .addNotificationPreference(NotificationPreferenceType.Email)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .Build();
        var productOwner = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Piet")
            .setEmail("Piet@mail.com")
            .setPhoneNr("06-12345678")
            .setSlackUsername("@PietjePuk")
            .setRole(Role.ProductOwner)
            .addNotificationPreference(NotificationPreferenceType.Email)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .addNotificationPreference(NotificationPreferenceType.WhatsApp)
            .Build();

        var listDev = new List<User> { devUser };
        var s = new Sprint(new Guid(), "sprint2", new DateTime(2023, 4, 2), new DateTime(2023, 4, 13), listDev);
        var pb = new ProductBacklog(new Guid(), s);
        var pbi = new BacklogItem(Guid.NewGuid(), "test", "item about a test", 1, 3, s, devUser, testUser);
        var pbi2 = new BacklogItem(Guid.NewGuid(), "andere test", "item about a andere test", 1, 10, s, devUser,
            testUser);
        s.AddBacklogItem(pbi);
        s.AddBacklogItem(pbi2);
        var activity = new Activity(new Guid(), "Maak de hele app", pb);
        var comment = new Comment(new Guid(), "Wat een mooie comment", pbi);
        var reply = new Comment(new Guid(), "wat een stomme actie", pbi);
        comment.ReplyToComment(reply);
        pbi.AddActivity(activity);
        pbi.AddComment(comment);
        var exPlain = new Exporter(new PlainTextExportStrategy());
        //Arrange
        var exportString = s.Accept(exPlain);

        //Assert
        Assert.That(exportString, Is.EqualTo("Sprint: sprint2, running from 2-4-2023 00:00:00 to 13-4-2023 00:00:00\n-BacklogItem: andere test, with description: item about a andere test\n-BacklogItem: test, with description: item about a test\n--Comment: Wat een mooie comment\n--Comment: wat een stomme actie\n--Activity: Maak de hele app"));
    }
    
        [Test]
    public void Export_Sprint_Report_InJSONFormat()
    {
        //Act
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

        var testUser = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Kees")
            .setEmail("Kees@company.com")
            .setSlackUsername("@KeesP")
            .setRole(Role.Tester)
            .addNotificationPreference(NotificationPreferenceType.Email)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .Build();
        var productOwner = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Piet")
            .setEmail("Piet@mail.com")
            .setPhoneNr("06-12345678")
            .setSlackUsername("@PietjePuk")
            .setRole(Role.ProductOwner)
            .addNotificationPreference(NotificationPreferenceType.Email)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .addNotificationPreference(NotificationPreferenceType.WhatsApp)
            .Build();

        var listDev = new List<User> { devUser };
        var s = new Sprint(new Guid(), "sprint2", new DateTime(2023, 4, 2), new DateTime(2023, 4, 13), listDev);
        var exJson = new Exporter(new JSONExportStrategy());
        //Arrange
        var exportString = s.Accept(exJson);

        //Assert
        Assert.That(exportString, Is.EqualTo(exportString));
    }
}