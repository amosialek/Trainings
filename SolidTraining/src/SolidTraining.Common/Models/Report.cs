namespace SolidTraining.Common.Models;

public class Report
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    public ReportType Type { get; set; }
}

public class ReportRequest
{
    public ReportType Type { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string Title { get; set; } = string.Empty;
}

public enum ReportType
{
    Sales,
    Inventory,
    Financial,
    UserActivity
}

public enum ExportFormat
{
    Pdf,
    Excel,
    Csv
}
