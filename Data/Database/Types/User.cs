namespace F1Project.Data.Database.Types;

public class User : DatabaseType
{
    public override string Id { get; init; } = null!;
    public int Subscribed { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Photo { get; set; }
    public int? AuthDate { get; set; }
    public string Hash { get; set; } = null!;

    /*public User(){}
    public User(string id, bool subscribed, string firstName, string lastName, string username, string photo)
    {
        Id = id;
        Subscribed = subscribed;
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Photo = photo;
    }*/
}