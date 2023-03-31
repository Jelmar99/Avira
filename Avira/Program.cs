// See https://aka.ms/new-console-template for more information

using Avira.Domain;
using Avira.Domain.Adapters;
using Avira.Domain.Notifications;

Console.WriteLine("Hello, World!");

var a = new BacklogItem(Guid.Empty, "", "", 0, 0, new Sprint(Guid.Empty, new DateTime(), new DateTime()));
var u = new User(new Guid(), "Bob");

u.NotificationPreference = new WhatsAppNotificationPreferenceDecorator(u.NotificationPreference);
u.NotificationPreference = new EmailNotificationPreferenceDecorator(u.NotificationPreference);
u.NotificationPreference = new SlackNotificationPreferenceDecorator(u.NotificationPreference);

var n = new Notification("test notification :)");

u.SetRole(Role.Developer);

a.AddListener(u);
a.SendNotification(n);
a.SendNotification(n);
a.SendNotification(n);
a.SendNotification(n);

var s = new Sprint(new Guid(), new DateTime(2023, 3, 30), new DateTime(2023, 4, 13));
var pb = new ProductBacklog(new Guid(), s);
var pbi = new BacklogItem(new Guid(), "test", "item about a test", 1, 1, s);
var pbi2 = new BacklogItem(new Guid(), "andere test", "item about a andere test", 1, 1, s);
s.AddBacklogItem(pbi);
s.AddBacklogItem(pbi2);
var activity = new Activity(new Guid(), "Maak de hele app", pb);
var comment = new Comment(new Guid(), "Wat een mooie comment");
pbi.AddActivity(activity);
pbi.AddComment(comment);
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

var exPlain = new Exporter(new PlainTextExportStrategy());
s.Accept(exPlain);

var exJson = new Exporter(new JSONExportStrategy());
s.Accept(exJson);
