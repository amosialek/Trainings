using NSubstitute;
using SolidTraining.Common.Models;
using SolidTraining.LSP.Good;

namespace SolidTraining.LSP.Tests.Good;

/// <summary>
/// EXERCISE — Write unit tests for the SOLID UserProfileService and UserAdminService.
///
/// Key improvements over the Bad version:
/// - UserProfileService depends on IUserReader → impossible to accidentally call Save/Delete
/// - CachedUserReader implements ONLY IUserReader → no broken methods, no NotSupportedException
/// - The type system PREVENTS the bug instead of letting it slip through to runtime
///
/// NSubstitute patterns in this exercise:
/// - Substitute.For&lt;IUserReader&gt;() — only read methods available to mock
/// - Substitute.For&lt;IUserWriter&gt;() — only write methods available to mock
/// - .Returns() — control what GetById returns
/// </summary>
[TestFixture]
public class UserServiceTests
{
    [Test]
    public void UserProfileService_GetProfile_ReturnsCorrectProfile()
    {
        // TODO: Arrange — create Substitute.For<IUserReader>()
        //       Set up reader.GetById(1).Returns(new User { Id = 1, Name = "Alice", Email = "alice@example.com", Role = "Admin" })
        //       Create UserProfileService(reader)
        // TODO: Act — call service.GetProfile(1)
        // TODO: Assert — profile is not null, profile.Name == "Alice", profile.Email == "alice@example.com"
        //
        // 🎯 Notice: IUserReader has NO Save or Delete methods.
        //    It is IMPOSSIBLE to accidentally test write operations here.
        //    Compare this to the Bad version where CachedReadOnlyUserRepository
        //    threw NotSupportedException — that can't happen with this design.

        throw new NotImplementedException("Implement this test");
    }

    [Test]
    public void UserProfileService_GetProfile_UserNotFound_ReturnsNull()
    {
        // TODO: Arrange — set up reader.GetById(999).Returns((User?)null)
        // TODO: Act — call service.GetProfile(999)
        // TODO: Assert — result is null

        throw new NotImplementedException("Implement this test");
    }

    [Test]
    public void UserProfileService_CanUseBothImplementations()
    {
        // TODO: This test demonstrates LSP compliance:
        //       Both SqlUserRepository and CachedUserReader implement IUserReader,
        //       and both can be substituted without breaking behavior.
        //
        // Arrange — create a Substitute.For<IUserReader>()
        // (This substitute represents ANY IUserReader implementation — LSP at work!)
        //
        // The key insight: unlike the Bad version where CachedReadOnlyUserRepository
        // broke SqlUserRepository's contract, here CachedUserReader ONLY implements
        // IUserReader and fully honors the contract.

        throw new NotImplementedException("Implement this test");
    }

    [Test]
    public void UserAdminService_UpdateUserName_SavesUser()
    {
        // TODO: Arrange — create both Substitute.For<IUserReader>() and Substitute.For<IUserWriter>()
        //       Set up reader.GetById(1).Returns(new User { Id = 1, Name = "OldName" })
        //       Create UserAdminService(reader, writer)
        // TODO: Act — call service.UpdateUserName(1, "NewName")
        // TODO: Assert — writer.Received(1).Save(Arg.Is<User>(u => u.Name == "NewName"))

        throw new NotImplementedException("Implement this test");
    }

    [Test]
    public void UserAdminService_RemoveUser_DeletesUser()
    {
        // TODO: Arrange — set up reader.GetById(1) to return a user
        //       Create UserAdminService(reader, writer)
        // TODO: Act — call service.RemoveUser(1)
        // TODO: Assert — writer.Received(1).Delete(1)

        throw new NotImplementedException("Implement this test");
    }

    [Test]
    public void UserAdminService_UpdateUserName_UserNotFound_ThrowsException()
    {
        // TODO: Arrange — set up reader.GetById(999).Returns((User?)null)
        // TODO: Act & Assert — Assert.Throws<InvalidOperationException>(() => service.UpdateUserName(999, "Name"))

        throw new NotImplementedException("Implement this test");
    }
}
