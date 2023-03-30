// See https://aka.ms/new-console-template for more information

using Avira.Domain;

Console.WriteLine("Hello, World!");

var a = new BacklogItem(Guid.Empty, "", "", 0, 0, new Sprint(Guid.Empty, new DateTime(), new DateTime()));
var u = new User(new Guid(), "Bob");
var n = new Notification("test notification :)");

u.SetRole(Role.Developer);

a.AddListener(u);
a.SendNotification(n);
a.SendNotification(n);
a.SendNotification(n);
a.SendNotification(n);

var s = new Sprint(new Guid(), new DateTime(2023,3,20), new DateTime(2023,4,7));
var p = new Pipeline(s);
p.AddListener(u);
s.Deploy(p);