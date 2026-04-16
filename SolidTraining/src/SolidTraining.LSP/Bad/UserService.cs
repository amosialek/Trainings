using SolidTraining.Common.Models;

namespace SolidTraining.LSP.Bad;

/// <summary>
/// This service accepts SqlUserRepository and calls Save.
/// It works fine with SqlUserRepository, but if someone injects
/// CachedReadOnlyUserRepository (which IS-A SqlUserRepository),
/// it will throw NotSupportedException at runtime.
///
/// This demonstrates the Liskov Substitution Principle violation:
/// the subtype cannot be substituted for the base type without breaking behavior.
/// </summary>
public class UserService
{
    private readonly SqlUserRepository _repository;

    public UserService(SqlUserRepository repository)
    {
        _repository = repository;
    }

    public User? GetUser(int id)
    {
        return _repository.GetById(id);
    }

    public void UpdateUserName(int id, string newName)
    {
        var user = _repository.GetById(id);
        if (user == null) throw new InvalidOperationException($"User {id} not found");

        user.Name = newName;
        // ❌ This will throw NotSupportedException if _repository is CachedReadOnlyUserRepository!
        _repository.Save(user);
    }

    public void RemoveUser(int id)
    {
        // ❌ Same problem — will throw if read-only repository is injected
        _repository.Delete(id);
    }
}
