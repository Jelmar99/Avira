// See https://aka.ms/new-console-template for more information

using Avira.Domain;
using Avira.Domain.Notifications;

Console.WriteLine("Hello, World!");

var a = new BacklogItem(Guid.Empty, "", "", 0, 0, new Sprint(Guid.Empty, new DateTime(), new DateTime()));
var u = new User(new Guid(), "Bob");

u.NotificationPreference = new WhatsAppNotificationPreferenceDecorator(u.NotificationPreference);
u.NotificationPreference = new EmailNotificationPreferenceDecorator(u.NotificationPreference);
u.NotificationPreference = new SlackNotificationPreferenceDecorator(u.NotificationPreference);

var n = new Notification("test notification :)");
a.AddListener(u);
a.SendNotification(n);