using SolidTraining.Common.Models;

namespace SolidTraining.ISP.Good;

/// <summary>
/// Concrete implementation of IReportExporter.
/// Exports reports to PDF and Excel formats.
/// </summary>
public class PdfReportExporter : IReportExporter
{
    public byte[] Export(Report report, ExportFormat format)
    {
        var prefix = format switch
        {
            ExportFormat.Pdf => "[PDF]",
            ExportFormat.Excel => "[EXCEL]",
            ExportFormat.Csv => "[CSV]",
            _ => "[UNKNOWN]"
        };

        var content = $"{prefix} {report.Title}: {report.Content}";
        return System.Text.Encoding.UTF8.GetBytes(content);
    }
}
