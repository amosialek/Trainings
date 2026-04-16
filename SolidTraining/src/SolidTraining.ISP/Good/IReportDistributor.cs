using SolidTraining.Common.Models;

namespace SolidTraining.ISP.Good;

/// <summary>
/// Focused interface: only report distribution (email, etc.).
///
/// ===== EXERCISE =====
/// Define the interface with a single method:
///   void Send(Report report, string recipientEmail)
/// ===================
/// </summary>
public interface IReportDistributor
{
    /// <summary>Sends the report to the specified recipient.</summary>
    void Send(Report report, string recipientEmail);
}
