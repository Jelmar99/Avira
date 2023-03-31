// See https://aka.ms/new-console-template for more information

using Avira.Domain;
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
a.AddListener(u);
a.SendNotification(n);