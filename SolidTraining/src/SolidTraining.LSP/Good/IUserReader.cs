using SolidTraining.Common.Models;

namespace SolidTraining.LSP.Good;

/// <summary>
/// GOOD EXAMPLE — Liskov Substitution Principle
/// 
/// Segregated read-only interface for user data access.
/// Both SqlUserRepository and CachedUserReader implement this,
/// and both can be substituted without breaking any contract.
///
/// ===== EXERCISE =====
/// Define the interface with these methods:
///   User? GetById(int id)
///   IReadOnlyList&lt;User&gt; Search(string query)
/// ===================
/// </summary>
public interface IUserReader
{
    /// <summary>Retrieves a user by their ID, or null if not found.</summary>
    User? GetById(int id);

    /// <summary>Searches for users matching the query string.</summary>
    IReadOnlyList<User> Search(string query);
}
