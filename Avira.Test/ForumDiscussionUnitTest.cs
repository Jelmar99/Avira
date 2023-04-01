using Avira.Domain;
using Avira.Domain.Builder;
using Avira.Domain.Notifications;

namespace Avira.Test;

[TestFixture]
public class ForumDiscussionUnitTest
{
    [Test]
    public void Test_AddComment()
    {
        // Arrange
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
        var listDev = new List<User>{devUser};
        
        var scrumMaster = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Master")
            .setEmail("Master@company.com")
            .setSlackUsername("@Master")
            .setRole(Role.ScrumMaster)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .Build();
        
        var sprint = new Sprint(new Guid(),"sprint 1", new DateTime(2023, 4, 10), new DateTime(2023, 5, 12), listDev, scrumMaster);
        var backlogItem = new BacklogItem(Guid.NewGuid(), "Test Backlog Item", "Description", 5, 2, sprint, devUser, testUser);
        var comment = new Comment(Guid.NewGuid(), "This is a comment",backlogItem);

        // Act
        backlogItem.AddComment(comment);

        // Assert
        Assert.That(backlogItem.Comments, Does.Contain(comment));
    }
    
    [Test]
    public void Test_AddComment_ToDoneBacklogItem()
    {
        // Arrange
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
        var listDev = new List<User>{devUser};
        
        var scrumMaster = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Master")
            .setEmail("Master@company.com")
            .setSlackUsername("@Master")
            .setRole(Role.ScrumMaster)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .Build();
        
        var sprint = new Sprint(new Guid(),"sprint 1", new DateTime(2023, 4, 10), new DateTime(2023, 5, 12), listDev, scrumMaster);
        var backlogItem = new BacklogItem(Guid.NewGuid(), "Test Backlog Item", "Description", 5, 2, sprint, devUser, testUser);
        var comment = new Comment(Guid.NewGuid(), "This is a comment",backlogItem);

        // Act
        backlogItem.UpdatePhase(BacklogItemPhase.Done, devUser);

        // Assert
        Assert.Throws<Exception>(() => backlogItem.AddComment(comment));
    }
    
    [Test]
    public void TestAddCommentReply()
    {
        // Arrange
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
        var listDev = new List<User>{devUser};
        
        var scrumMaster = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Master")
            .setEmail("Master@company.com")
            .setSlackUsername("@Master")
            .setRole(Role.ScrumMaster)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .Build();

        var sprint = new Sprint(new Guid(),"sprint 1", new DateTime(2023, 4, 10), new DateTime(2023, 5, 12), listDev, scrumMaster);
        var backlogItem = new BacklogItem(Guid.NewGuid(), "Test Backlog Item", "Description", 5, 2, sprint, devUser, testUser);
        var comment = new Comment(Guid.NewGuid(), "This is a comment", backlogItem);
        var reply = new Comment(Guid.NewGuid(), "This is a reply", backlogItem);

        // Act
        backlogItem.AddComment(comment);
        comment.ReplyToComment(reply);

        // Assert
        Assert.That(comment.Replies, Does.Contain(reply));
    }
    
    [Test]
    public void CompletedBacklogItem_DiscussionLocked_NoNewMessagesOrThreadsAllowed()
    {
        // Arrange
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
        var listDev = new List<User>{devUser};
        
        var scrumMaster = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Master")
            .setEmail("Master@company.com")
            .setSlackUsername("@Master")
            .setRole(Role.ScrumMaster)
            .addNotificationPreference(NotificationPreferenceType.Slack)
            .Build();
        
        var sprint = new Sprint(new Guid(),"sprint 1", new DateTime(2023, 4, 10), new DateTime(2023, 5, 12), listDev, scrumMaster);
        var backlogItem = new BacklogItem(Guid.NewGuid(), "Test Backlog Item", "Description", 3, 2, sprint, devUser, testUser);

        var comment1 = new Comment(Guid.NewGuid(), "Test Comment 1", backlogItem);
        var comment2 = new Comment(Guid.NewGuid(), "Test Comment 2",backlogItem);

        backlogItem.AddComment(comment1);
        backlogItem.AddComment(comment2);
        
        // Act
        backlogItem.UpdatePhase(BacklogItemPhase.Done, devUser);
        var ex1 = Assert.Throws<Exception>(() => backlogItem.AddComment(new Comment(Guid.NewGuid(), "Test Comment 3",backlogItem)));
        var ex2 = Assert.Throws<Exception>(() => comment1.ReplyToComment(new Comment(Guid.NewGuid(), "Test Reply",backlogItem)));
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(ex1?.Message, Is.EqualTo("You can't add a comment to a Backlog Item that is already Done."));
            Assert.That(ex2?.Message, Is.EqualTo("You can't reply to a comment on a Backlog Item that is in the 'Done' phase."));
        });
    }
}
