using SolidTraining.Common.Models;

namespace SolidTraining.ISP.Good;

/// <summary>
/// Concrete implementation of IReportGenerator.
/// Generates sales reports.
/// </summary>
public class SalesReportGenerator : IReportGenerator
{
    public Report Generate(ReportRequest request)
    {
        return new Report
        {
            Title = request.Title,
            Type = request.Type,
            Content = $"Sales report data from {request.From:d} to {request.To:d}",
            GeneratedAt = DateTime.UtcNow
        };
    }
}
