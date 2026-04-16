using SolidTraining.Common.Models;

namespace SolidTraining.ISP.Bad;

/// <summary>
/// BAD EXAMPLE — Interface Segregation Principle Violation
/// 
/// This "fat" interface forces ALL implementers and ALL consumers to depend on
/// ALL 6 methods, even if they only need 1 or 2.
///
/// Problems:
/// - AdHocReportController only needs Generate + ExportToPdf, but depends on everything
/// - Testing AdHocReportController requires mocking all 6 methods
/// - Adding a new method (e.g., ExportToCsv) forces ALL consumers to be recompiled
/// - Implementers may throw NotSupportedException for methods they don't support
/// </summary>
public interface IReportService
{
    Report Generate(ReportRequest request);
    byte[] ExportToPdf(Report report);
    byte[] ExportToExcel(Report report);
    void Schedule(Report report, string cronExpression);
    void EmailReport(Report report, string recipientEmail);
    IEnumerable<Report> GetHistory(DateTime from, DateTime to);
}
