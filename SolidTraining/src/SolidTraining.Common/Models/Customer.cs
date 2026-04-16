namespace SolidTraining.Common.Models;

public class Customer
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int YearsActive { get; set; }
    public bool IsEmployee { get; set; }
}
