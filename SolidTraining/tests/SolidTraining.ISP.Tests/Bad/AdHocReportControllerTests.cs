using NSubstitute;
using SolidTraining.Common.Models;
using SolidTraining.ISP.Bad;

namespace SolidTraining.ISP.Tests.Bad;

/// <summary>
/// PAIN DEMO — Testing with a fat interface
///
/// To test AdHocReportController (which only uses Generate + ExportToPdf),
/// we must create a substitute for the entire IReportService with ALL 6 methods.
///
/// Notice the bloated setup — most of these setups are irrelevant to the test!
/// If we add a 7th method to IReportService, ALL test files need updating.
/// </summary>
[TestFixture]
public class AdHocReportControllerTests
{
    [Test]
    public void GenerateAndExportPdf_RequiresMockingEntireFatInterface()
    {
        // ❌ We must create a substitute for ALL 6 methods, even though
        // AdHocReportController only uses 2 (Generate + ExportToPdf)
        var fatService = Substitute.For<IReportService>();

        var report = new Report { Title = "Sales Q4", Content = "data" };
        var pdfBytes = new byte[] { 1, 2, 3 };

        // TODO: Set up the substitute for ALL 6 methods, even the irrelevant ones: 
        // example: fatService.MethodName(Arg.Any<ArgumentType>()).Returns(returnValue);
        //  - Generate(...);
        //  - ExportToPdf(...);
        //  - ExportToExcel(...)
        //  - Schedule(...)
        //  - EmailReport(...)
        //  - GetHistory(...)
        //
        // Yet if any of them change signature, this test file must recompile!
        // And if someone adds a 7th method, ALL consumers must be updated.

        var controller = new AdHocReportController(fatService);
        var request = new ReportRequest { Title = "Sales Q4", Type = ReportType.Sales };

        var result = controller.GenerateAndExportPdf(request);

        Assert.That(result, Is.EqualTo(pdfBytes));

        // The test works, but the mock is unnecessarily bloated.
        // In a real codebase with dozens of consumers of IReportService,
        // every change to the interface ripples through ALL test files.
    }
}
