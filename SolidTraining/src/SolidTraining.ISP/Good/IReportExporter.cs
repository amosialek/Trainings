using SolidTraining.Common.Models;

namespace SolidTraining.ISP.Good;

/// <summary>
/// Focused interface: only report exporting.
///
/// ===== EXERCISE =====
/// Define the interface with a single method:
///   byte[] Export(Report report, ExportFormat format)
/// ===================
/// </summary>
public interface IReportExporter
{
    /// <summary>Exports the report in the specified format and returns the byte content.</summary>
    byte[] Export(Report report, ExportFormat format);
}
