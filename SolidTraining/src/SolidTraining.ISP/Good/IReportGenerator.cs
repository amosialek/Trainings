using SolidTraining.Common.Models;

namespace SolidTraining.ISP.Good;

/// <summary>
/// GOOD EXAMPLE — Interface Segregation Principle
/// 
/// Focused interface: only report generation.
///
/// ===== EXERCISE =====
/// Define the interface with a single method:
///   Report Generate(ReportRequest request)
/// ===================
/// </summary>
public interface IReportGenerator
{
    /// <summary>Generates a report from the given request parameters.</summary>
    Report Generate(ReportRequest request);
}
