using Avira.Domain;
using Avira.Domain.Adapters;
using Avira.Domain.Builder;
using Avira.Domain.Notifications;

namespace Avira.Test;

[TestFixture]
public class ProjectTest
{
    private User _dev1 = null!;
    private User _scrumMaster = null!;
    private ProductBacklog _productBacklog = null!;
    private Sprint _sprint = null!;

    [SetUp]
    public void Setup()
    {
        _dev1 = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("Dev")
            .setEmail("Dev@company.com")
            .setPhoneNr("06-00000000")
            .setSlackUsername("@Developer")
            .setRole(Role.Developer)
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

        var tomorrow = DateTime.Now.AddDays(1);
        _sprint = new Sprint(Guid.NewGuid(), "TestSprint", tomorrow, tomorrow.AddDays(14), new List<User> { _dev1 },
            _scrumMaster);

        //Todo: fix
        _productBacklog = new ProductBacklog(Guid.NewGuid(), _sprint);
    }

    [Test]
    public void ProjectHasProductOwner()
    {
        //Arrange
        var productOwner = new UserBuilder()
            .setId(Guid.NewGuid())
            .setName("PO")
            .setEmail("PO@company.com")
            .setPhoneNr("06-00000004")
            .setSlackUsername("@PO")
            .addNotificationPreference(NotificationPreferenceType.Email)
            .Build();
        productOwner.RemoveRole();
        productOwner.SetRole(Role.ProductOwner);

        //Act
        var proj = new Project(Guid.NewGuid(), _productBacklog, new GithubAdapter(), productOwner);
        proj.AddSprint(_sprint);

        //Assert
        Assert.That(proj.ProductOwner.Role, Is.EqualTo(Role.ProductOwner));
    }
}