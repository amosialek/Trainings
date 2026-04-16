using SolidTraining.Common.Models;

namespace SolidTraining.LSP.Good;

/// <summary>
/// Cached read-only implementation that ONLY implements IUserReader.
/// No Save or Delete methods exist — impossible to accidentally call them!
///
/// This respects LSP: any code expecting IUserReader can safely use this class.
/// There is no broken contract, no NotSupportedException — the type system prevents misuse.
///
/// ===== EXERCISE =====
/// Implement GetById and Search with simple caching logic.
/// ===================
/// </summary>
public class CachedUserReader : IUserReader
{
    private readonly Dictionary<int, User> _cache = new();

    public User? GetById(int id)
    {
        // TODO: Check _cache first, return cached user if found.
        // If not cached, create a dummy user, cache it, and return it.
        throw new NotImplementedException();
    }

    public IReadOnlyList<User> Search(string query)
    {
        // TODO: Return a list of users (can be a simple dummy implementation)
        throw new NotImplementedException();
    }
}
