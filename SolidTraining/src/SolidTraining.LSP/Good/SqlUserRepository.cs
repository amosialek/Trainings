using SolidTraining.Common.Models;

namespace SolidTraining.LSP.Good;

/// <summary>
/// Full SQL repository that implements BOTH IUserReader and IUserWriter.
/// This makes sense — the SQL database supports both reading and writing.
///
/// ===== EXERCISE =====
/// Implement all methods from both IUserReader and IUserWriter.
/// (Simulated implementations are fine — just return dummy data.)
/// ===================
/// </summary>
public class SqlUserRepository : IUserReader, IUserWriter
{
    public User? GetById(int id)
    {
        // TODO: Simulate a database read — return a User with the given id
        throw new NotImplementedException();
    }

    public IReadOnlyList<User> Search(string query)
    {
        // TODO: Simulate a database search — return a list of matching Users
        throw new NotImplementedException();
    }

    public void Save(User user)
    {
        // TODO: Simulate a database save
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        // TODO: Simulate a database delete
        throw new NotImplementedException();
    }
}
