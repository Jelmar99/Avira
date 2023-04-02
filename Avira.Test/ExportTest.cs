using Avira.Domain;
using Avira.Domain.Builder;
using Avira.Domain.Exporters;
using Avira.Domain.Notifications;

namespace Avira.Test;

[TestFixture]
public class ExportTest
{
    private User _devUser = null!;
    private User _testUser = null!;
    private User _scrumMaster = null!;
    private ProductBacklog _productBacklog = null!;
    private Sprint _sprint = null!;

    [SetUp]
    public void Setup()
    {
        _devUser = new UserBuilder()
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

        _testUser = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Kees")
            .setEmail("Kees@company.com")
            .setSlackUsername("@KeesP")
            .setRole(Role.Tester)
            .addNotificationPreference(NotificationPreferenceType.Email)
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


        _sprint = new Sprint(new Guid(), "sprint2", new DateTime(4023, 4, 2), new DateTime(4023, 4, 13),
            new List<User> { _devUser }, _scrumMaster);
        _productBacklog = new ProductBacklog(new Guid("1cf5572b-3bfe-4e52-92f2-128391dab4b4"), _sprint);
        
        var pbi = new BacklogItem(Guid.NewGuid(), "test", "item about a test", 1, 3, _sprint, _devUser, _testUser);
        var pbi2 = new BacklogItem(Guid.NewGuid(), "andere test", "item about a andere test", 1, 10, _sprint, _devUser,
            _testUser);

        var activity = new Activity(new Guid(), "Maak de hele app");
        var comment = new Comment(new Guid(), "Wat een mooie comment", pbi);
        var reply = new Comment(new Guid(), "wat een stomme actie", pbi);
        
        comment.ReplyToComment(reply);
        pbi.AddActivity(activity);
        pbi.AddComment(comment);

        _sprint.AddBacklogItem(pbi);
        _sprint.AddBacklogItem(pbi2);

        _productBacklog.AddBacklogItem(pbi);
        _productBacklog.AddBacklogItem(pbi2);
    }

    [Test]
    public void Export_Sprint_Report_InPlainTextFormat()
    {
        //Arrange
        var exPlain = new Exporter(new PlainTextExportStrategy());

        //Act
        var exportString = _sprint.Accept(exPlain);

        //Assert
        Assert.That(exportString,
            Is.EqualTo(
                "Sprint: sprint2, running from 04/02/4023 00:00:00 to 04/13/4023 00:00:00\n-BacklogItem: andere test, with description: item about a andere test, assigned developer: Bob\n-BacklogItem: test, with description: item about a test, assigned developer: Bob\n--Comment: Wat een mooie comment\n--Comment: wat een stomme actie\n--Activity: Maak de hele app"));
    }

    [Test]
    public void Export_Sprint_Report_InJSONFormat()
    {
        //Arrange
        var exJson = new Exporter(new JSONExportStrategy());

        //Act
        var exportString = _sprint.Accept(exJson);

        //Assert
        Assert.That(exportString,
            Is.EqualTo(
                "\"Sprint Time span from:  04/02/4023 00:00:00 to 04/13/4023 00:00:00\"\n\"-BacklogItem: andere test, with description: item about a andere testassigned developer: Bob\"\n\"-BacklogItem: test, with description: item about a testassigned developer: Bob\"\n\"--Comment: Wat een mooie comment\"\n\"--Comment: wat een stomme actie\"\n\"--Activity: Maak de hele app\""));
    }

    [Test]
    public void Export_ProductBacklog_Report_InPlainTextFormat()
    {
        //Arrange
        var exPlain = new Exporter(new PlainTextExportStrategy());

        //Act
        var exportString = _productBacklog.Accept(exPlain);

        //Assert
        Assert.That(exportString,
            Is.EqualTo(
                "ProductBacklog: 1cf5572b-3bfe-4e52-92f2-128391dab4b4 From sprint: sprint2\n-BacklogItem: andere test, with description: item about a andere test, assigned developer: Bob\n-BacklogItem: test, with description: item about a test, assigned developer: Bob\n--Comment: Wat een mooie comment\n--Comment: wat een stomme actie\n--Activity: Maak de hele app"
            ));
    }
}