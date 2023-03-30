namespace GasStation.Domain.Entities;

public class Role
{
    public int Id { get; set; } 
    public string Title { get; set; } = null!;
    public User User { get; set; } = null!;
}