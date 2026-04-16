using SolidTraining.Common.Models;
using SolidTraining.LSP.Bad;

namespace SolidTraining.LSP.Tests.Bad;

/// <summary>
/// PAIN DEMO — Liskov Substitution Principle Violation in action
///
/// Shows that CachedReadOnlyUserRepository can be substituted for SqlUserRepository
/// (it inherits from it), but breaks at runtime when Save or Delete is called.
///
/// The compiler allows it. The type system allows it. But the behavior is broken.
/// This is pure LSP violation — subtypes must be substitutable for their base types.
/// </summary>
[TestFixture]
public class UserRepositoryTests
{
    [Test]
    public void SqlUserRepository_SaveWorks()
    {
        // Base class works fine — Save does what you expect
        var repo = new SqlUserRepository();
        var user = new User { Id = 1, Name = "Alice", Email = "alice@example.com" };

        Assert.DoesNotThrow(() => repo.Save(user));
    }

    [Test]
    public void CachedReadOnlyRepository_SaveThrowsException()
    {
        // ❌ Subclass breaks the contract!
        // CachedReadOnlyUserRepository IS-A SqlUserRepository (inheritance),
        // but calling Save throws NotSupportedException.

        SqlUserRepository repo = new CachedReadOnlyUserRepository(); // Valid assignment — IS-A
        var user = new User { Id = 1, Name = "Alice", Email = "alice@example.com" };

        // This compiles fine, but EXPLODES at runtime:
        Assert.Throws<NotSupportedException>(() => repo.Save(user));
    }

    [Test]
    public void CachedReadOnlyRepository_DeleteThrowsException()
    {
        SqlUserRepository repo = new CachedReadOnlyUserRepository();

        Assert.Throws<NotSupportedException>(() => repo.Delete(1));
    }

    [Test]
    public void UserService_WorksWithBaseClass()
    {
        var repo = new SqlUserRepository();
        var service = new UserService(repo);

        // This works fine with the base class
        Assert.DoesNotThrow(() => service.UpdateUserName(1, "New Name"));
    }

    [Test]
    public void UserService_ExplodesWithSubclass()
    {
        // ❌ THE RUNTIME BOMB
        // UserService accepts SqlUserRepository, so CachedReadOnlyUserRepository fits.
        // But calling UpdateUserName triggers Save → NotSupportedException!
        
        var readOnlyRepo = new CachedReadOnlyUserRepository();
        var service = new UserService(readOnlyRepo); // No compiler error — the type fits!

        // 💥 BOOM! Runtime exception because the subtype broke the base type's contract.
        Assert.Throws<NotSupportedException>(() => service.UpdateUserName(1, "New Name"));

        // In a real codebase, this bug would slip through code review and testing
        // (unless you specifically test with CachedReadOnlyUserRepository),
        // and explode in production.
    }
}
