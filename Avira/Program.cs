// See https://aka.ms/new-console-template for more information

using Avira.Domain;
using Avira.Domain.Adapters;
using Avira.Domain.Builder;
using Avira.Domain.Notifications;

Console.WriteLine("Hello, World!");

var a = new BacklogItem(Guid.Empty, "", "", 0, 0, new Sprint(Guid.Empty, new DateTime(), new DateTime()));

//var u = new User(new Guid(), "Bob");
var u = new UserBuilder()
    .setId(Guid.Empty)
    .setName("Bob")
    .setEmail("Bob@company.com")
    .setPhoneNr("06-87654321")
    .setSlackUsername("@BobbyB")
    .addNotificationPreference(NotificationPreferenceType.Email)
    .addNotificationPreference(NotificationPreferenceType.Slack)
    .addNotificationPreference(NotificationPreferenceType.WhatsApp)
    .Build();

var n = new Notification("A test notification!  :)");

u.SetRole(Role.Developer);

a.AddListener(u);
a.SendNotification(n);

var s = new Sprint(new Guid(), new DateTime(2023, 3, 30), new DateTime(2023, 4, 13));
var pb = new ProductBacklog(new Guid(), s);
var p1 = new Project(new Guid(), pb, new GithubAdapter());
var p2 = new Project(new Guid(), pb, new GitLabAdapter());
var p3 = new Project(new Guid(), pb, new AWSCodeAdapter());
var p4 = new Project(new Guid(), pb, new BitBucketAdapter());
var p5 = new Project(new Guid(), pb, new MSAzureDevOpsAdapter());
p1.Commit();
p2.Commit();
p3.Commit();
p4.Commit();
p5.Commit();

var p = new Pipeline(s);
p.AddListener(u);
s.Deploy(p);