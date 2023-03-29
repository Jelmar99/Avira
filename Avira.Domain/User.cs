namespace Avira.Domain;

public class User
{
    private Guid Id { get; set; }
    private string Name { get; set; }
    private Role? Role { get; set; }
    
    public User(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public void SetRole(Role? role)
    {
        Role = role;
    }
    public void RemoveRole()
    {
        Role = null;
    }
}