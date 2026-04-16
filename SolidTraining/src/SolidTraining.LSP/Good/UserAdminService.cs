using SolidTraining.Common.Models;

namespace SolidTraining.LSP.Good;

/// <summary>
/// Service that needs both read AND write access.
/// Depends on both IUserReader and IUserWriter — clear about its requirements.
/// Can only be injected with implementations that support both (e.g., SqlUserRepository).
///
/// Provided as reference — no TODOs.
/// </summary>
public class UserAdminService
{
    private readonly IUserReader _reader;
    private readonly IUserWriter _writer;

    public UserAdminService(IUserReader reader, IUserWriter writer)
    {
        _reader = reader;
        _writer = writer;
    }

    public void UpdateUserName(int id, string newName)
    {
        var user = _reader.GetById(id);
        if (user == null) throw new InvalidOperationException($"User {id} not found");

        user.Name = newName;
        _writer.Save(user);
    }

    public void RemoveUser(int id)
    {
        var user = _reader.GetById(id);
        if (user == null) throw new InvalidOperationException($"User {id} not found");

        _writer.Delete(id);
    }
}
