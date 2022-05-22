namespace F1Project.Data;

public class User
{
    public string Id { get; init; } = null!;
    public bool Subscribed { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Photo { get; set; }
    public int? AuthDate { get; set; }
    public string? Hash { get; set; }
    
    public User(){}
    /*public User(string id, bool subscribed, string firstName, string lastName, string username, string photo)
    {
        Id = id;
        Subscribed = subscribed;
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Photo = photo;
    }*/
}