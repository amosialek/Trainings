using SolidTraining.Common.Models;

namespace SolidTraining.ISP.Good;

/// <summary>
/// Focused interface: only report scheduling.
///
/// ===== EXERCISE =====
/// Define the interface with a single method:
///   void Schedule(Report report, string cronExpression)
/// ===================
/// </summary>
public interface IReportScheduler
{
    /// <summary>Schedules the report for recurring generation.</summary>
    void Schedule(Report report, string cronExpression);
}
