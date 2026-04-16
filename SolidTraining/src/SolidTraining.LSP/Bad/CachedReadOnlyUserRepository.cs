using SolidTraining.Common.Models;

namespace SolidTraining.LSP.Bad;

/// <summary>
/// ❌ LISKOV SUBSTITUTION VIOLATION
///
/// This class inherits from SqlUserRepository but BREAKS the contract
/// by throwing NotSupportedException for Save and Delete.
///
/// Any code that receives a SqlUserRepository and calls Save() will 
/// work fine with the base class, but EXPLODE at runtime if this subclass
/// is substituted instead. This is the classic LSP violation.
///
/// The compiler won't catch this — it's a runtime bomb!
/// </summary>
public class CachedReadOnlyUserRepository : SqlUserRepository
{
    private readonly Dictionary<int, User> _cache = new();

    public override User? GetById(int id)
    {
        if (_cache.TryGetValue(id, out var cached))
        {
            Console.WriteLine($"[CACHE HIT] User {id}");
            return cached;
        }

        var user = base.GetById(id);
        if (user != null) _cache[id] = user;
        return user;
    }

    public override void Save(User user)
    {
        // ❌ Breaks the parent class contract — callers don't expect this to throw!
        throw new NotSupportedException("This is a read-only repository! Cannot save.");
    }

    public override void Delete(int id)
    {
        // ❌ Breaks the parent class contract — callers don't expect this to throw!
        throw new NotSupportedException("This is a read-only repository! Cannot delete.");
    }
}
