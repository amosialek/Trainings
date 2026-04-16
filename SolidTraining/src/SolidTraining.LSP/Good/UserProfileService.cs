using SolidTraining.Common.Models;

namespace SolidTraining.LSP.Good;

/// <summary>
/// Service that only needs to READ user data.
/// Depends on IUserReader — cannot accidentally call Save or Delete.
///
/// This is safe to use with BOTH SqlUserRepository and CachedUserReader,
/// because both fully implement IUserReader without breaking any contract.
///
/// ===== EXERCISE =====
/// Implement GetProfile:
/// 1. Use _reader.GetById(userId) to get the user
/// 2. If user is null, return null
/// 3. Map to a UserProfile with UserId, Name, Email, and DisplayRole
/// ===================
/// </summary>
public class UserProfileService
{
    private readonly IUserReader _reader;

    public UserProfileService(IUserReader reader)
    {
        _reader = reader;
    }

    public UserProfile? GetProfile(int userId)
    {
        // TODO: Get user from _reader.GetById(userId)
        // TODO: If null, return null
        // TODO: Map to UserProfile: UserId, Name, Email, DisplayRole = user.Role
        throw new NotImplementedException("Implement profile retrieval using IUserReader");
    }
}
