using SolidTraining.Common.Models;
using System.Reflection.Emit;

namespace SolidTraining.ISP.Bad;

/// <summary>
/// This controller only needs to Generate a report and Export it to PDF.
/// Yet it depends on the entire IReportService with 6 methods — including scheduling,
/// emailing, Excel export, and history retrieval that it never uses.
///
/// In tests, you'd have to mock ALL 6 methods on IReportService just to test
/// the 2 methods this controller actually cares about.
/// </summary>
public class AdHocReportController
{
    private readonly IReportService _reportService;

    public AdHocReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    public byte[] GenerateAndExportPdf(ReportRequest request)
    {
        var report = _reportService.Generate(request);
        if (report is null) throw new NullReferenceException("generated report is null");
        var pdfBytes = _reportService.ExportToPdf(report);
        if (pdfBytes.Length == 0)
            throw new Exception("exported report is empty");

        return pdfBytes;
    }
}
