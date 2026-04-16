using SolidTraining.Common.Models;

namespace SolidTraining.LSP.Bad;

/// <summary>
/// BAD EXAMPLE — Liskov Substitution Principle Violation
///
/// Base class with virtual methods for read AND write operations.
/// The problem: CachedReadOnlyUserRepository inherits from this but throws
/// exceptions for Save and Delete — breaking the behavioral contract.
/// </summary>
public class SqlUserRepository
{
    public virtual User? GetById(int id)
    {
        // Simulates DB read
        Console.WriteLine($"[SQL] SELECT * FROM Users WHERE Id = {id}");
        return new User { Id = id, Name = $"User {id}", Email = $"user{id}@example.com" };
    }

    public virtual List<User> Search(string query)
    {
        Console.WriteLine($"[SQL] SELECT * FROM Users WHERE Name LIKE '%{query}%'");
        return new List<User>
        {
            new() { Id = 1, Name = "Alice", Email = "alice@example.com" },
            new() { Id = 2, Name = "Bob", Email = "bob@example.com" }
        };
    }

    public virtual void Save(User user)
    {
        Console.WriteLine($"[SQL] INSERT/UPDATE Users SET Name='{user.Name}' WHERE Id={user.Id}");
    }

    public virtual void Delete(int id)
    {
        Console.WriteLine($"[SQL] DELETE FROM Users WHERE Id = {id}");
    }
}
