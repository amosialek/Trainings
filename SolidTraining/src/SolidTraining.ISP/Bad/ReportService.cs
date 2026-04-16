using SolidTraining.Common.Models;

namespace SolidTraining.ISP.Bad;

/// <summary>
/// Implementation of the fat IReportService interface.
/// Must implement all 6 methods even though different consumers use different subsets.
/// </summary>
public class ReportService : IReportService
{
    public Report Generate(ReportRequest request)
    {
        return new Report
        {
            Title = request.Title,
            Type = request.Type,
            Content = $"Report data from {request.From:d} to {request.To:d}",
            GeneratedAt = DateTime.UtcNow
        };
    }

    public byte[] ExportToPdf(Report report)
    {
        var content = $"[PDF] {report.Title}: {report.Content}";
        return System.Text.Encoding.UTF8.GetBytes(content);
    }

    public byte[] ExportToExcel(Report report)
    {
        var content = $"[EXCEL] {report.Title}: {report.Content}";
        return System.Text.Encoding.UTF8.GetBytes(content);
    }

    public void Schedule(Report report, string cronExpression)
    {
        Console.WriteLine($"[SCHEDULER] Report '{report.Title}' scheduled with cron: {cronExpression}");
    }

    public void EmailReport(Report report, string recipientEmail)
    {
        Console.WriteLine($"[EMAIL] Report '{report.Title}' sent to {recipientEmail}");
    }

    public IEnumerable<Report> GetHistory(DateTime from, DateTime to)
    {
        Console.WriteLine($"[HISTORY] Fetching reports from {from:d} to {to:d}");
        return new List<Report>();
    }
}
