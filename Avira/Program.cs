// See https://aka.ms/new-console-template for more information

using Avira.Domain;
using Avira.Domain.Builder;
using Avira.Domain.Notifications;

Console.WriteLine("Hello, World!");

var a = new BacklogItem(Guid.Empty, "", "", 0, 0, new Sprint(Guid.Empty, new DateTime(), new DateTime()));
INotificationPreference notif = new NotificationPreference();
notif = new WhatsAppNotificationPreferenceDecorator(notif);
notif = new EmailNotificationPreferenceDecorator(notif);
notif = new SlackNotificationPreferenceDecorator(notif);

//var u = new User(new Guid(), "Bob");
var u = new UserBuilder()
    .setId(new Guid())
    .setName("Bob")
    .setNotificationPreference(notif)
    .Build();



var n = new Notification("test notification :)");
a.AddListener(u);
a.SendNotification(n);