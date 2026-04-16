using SolidTraining.Common.Models;

namespace SolidTraining.LSP.Good;

/// <summary>
/// Segregated write interface for user data access.
/// Only implemented by repositories that actually support write operations.
///
/// ===== EXERCISE =====
/// Define the interface with these methods:
///   void Save(User user)
///   void Delete(int id)
/// ===================
/// </summary>
public interface IUserWriter
{
    /// <summary>Saves (inserts or updates) the user.</summary>
    void Save(User user);

    /// <summary>Deletes the user with the specified ID.</summary>
    void Delete(int id);
}
