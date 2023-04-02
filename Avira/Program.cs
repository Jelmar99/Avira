// See https://aka.ms/new-console-template for more information

using Avira.Domain;
using Avira.Domain.Adapters;
using Avira.Domain.Builder;
using Avira.Domain.Exporters;
using Avira.Domain.Notifications;

Console.WriteLine("Hello, World!");

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

var scrumMaster = new UserBuilder()
    .setId(Guid.NewGuid())
    .setName("Henk")
    .setEmail("Henk@mail.com")
    .setSlackUsername("@Henk84")
    .setRole(Role.ScrumMaster)
    .addNotificationPreference(NotificationPreferenceType.Slack)
    .Build();

var listDev = new List<User>{devUser};

var a = new BacklogItem(Guid.NewGuid(), "", "", 0, 0, new Sprint(Guid.NewGuid(),"sprint1", new DateTime(), new DateTime(),listDev, scrumMaster),
    devUser, testUser);
var n = new Notification("A test notification!  :)");
a.AddListener(devUser);
a.SendNotification(n);


var s = new Sprint(new Guid(),"sprint2", new DateTime(3023, 4, 2), new DateTime(3023, 4, 13), listDev, scrumMaster);
var pb = new ProductBacklog(new Guid(), s);
var pbi = new BacklogItem(Guid.NewGuid(), "test", "item about a test", 1, 3, s, devUser, testUser);
var pbi2 = new BacklogItem(Guid.NewGuid(), "andere test", "item about a andere test", 1, 10, s, devUser, testUser);
s.AddBacklogItem(pbi);
s.AddBacklogItem(pbi2);
var activity = new Activity(new Guid(), "Maak de hele app");
var comment = new Comment(new Guid(), "Wat een mooie comment", pbi);
var reply = new Comment(new Guid(), "wat een stomme actie", pbi);
comment.ReplyToComment(reply);
pbi.AddActivity(activity);
pbi.AddComment(comment);
var p1 = new Project(new Guid(), pb, new GithubAdapter(), productOwner);
var p2 = new Project(new Guid(), pb, new GitLabAdapter(), productOwner);
var p3 = new Project(new Guid(), pb, new AWSCodeAdapter(), productOwner);
var p4 = new Project(new Guid(), pb, new BitBucketAdapter(), productOwner);
var p5 = new Project(new Guid(), pb, new MSAzureDevOpsAdapter(), productOwner);
p1.Commit();
p2.Commit();
p3.Commit();
p4.Commit();
p5.Commit();

var p = new Pipeline(s);
p.AddListener(devUser);
s.Deploy();

var exPlain = new Exporter(new PlainTextExportStrategy());
Console.WriteLine(s.Accept(exPlain));

var exJson = new Exporter(new JSONExportStrategy());
Console.WriteLine(s.Accept(exJson));
