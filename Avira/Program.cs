// See https://aka.ms/new-console-template for more information

using Avira.Domain;
using Avira.Domain.Adapters;

Console.WriteLine("Hello, World!");

var a = new BacklogItem(Guid.Empty, "", "", 0, 0, new Sprint(Guid.Empty, new DateTime(), new DateTime()));
var u = new User(new Guid(), "Bob");
var n = new Notification("test notification :)");
a.AddListener(u);
a.SendNotification(n);
a.SendNotification(n);
a.SendNotification(n);
a.SendNotification(n);

var s = new Sprint(new Guid(), new DateTime(2023, 3, 30), new DateTime(2023, 4, 13));
var pb = new ProductBacklog(new Guid(), s);
var p = new Project(new Guid(), pb, new GithubAdapter());
p.Commit();