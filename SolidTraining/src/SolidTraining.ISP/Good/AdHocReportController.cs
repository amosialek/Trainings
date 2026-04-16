using SolidTraining.Common.Models;

namespace SolidTraining.ISP.Good;

/// <summary>
/// GOOD EXAMPLE — Interface Segregation Principle
/// 
/// This controller only depends on the interfaces it actually uses:
/// IReportGenerator and IReportExporter.
///
/// It does NOT depend on IReportScheduler, IReportDistributor, or any other concern.
///
/// Benefits:
/// - Tests only need to create 2 mocks instead of mocking a fat 6-method interface
/// - Changes to scheduling or distribution don't affect this controller at all
/// - Clear, focused dependencies make the code self-documenting
///
/// ===== EXERCISE =====
/// Implement GenerateAndExportPdf:
/// 1. Use _generator.Generate(request) to create the report
/// 2. Use _exporter.Export(report, ExportFormat.Pdf) to export it
/// 3. Return the exported bytes
/// ===================
/// </summary>
public class AdHocReportController
{
    private readonly IReportGenerator _generator;
    private readonly IReportExporter _exporter;

    public AdHocReportController(IReportGenerator generator, IReportExporter exporter)
    {
        _generator = generator;
        _exporter = exporter;
    }

    public byte[] GenerateAndExportPdf(ReportRequest request)
    {
        // TODO: Step 1 — Generate the report using _generator.Generate(request)

        // TODO: Step 2 — Export to PDF using _exporter.Export(report, ExportFormat.Pdf)

        // TODO: Step 3 — Return the exported bytes

        throw new NotImplementedException("Implement report generation and PDF export");
    }
}
