using NSubstitute;
using SolidTraining.Common.Models;
using SolidTraining.ISP.Good;

namespace SolidTraining.ISP.Tests.Good;

/// <summary>
/// EXERCISE — Write unit tests for the SOLID AdHocReportController.
///
/// Compare with the Bad version:
/// - BAD: Mock IReportService with 6 methods, most irrelevant
/// - GOOD: Mock only IReportGenerator + IReportExporter — exactly what the controller needs
///
/// Benefits:
/// - Less setup code — only 2 substitutes instead of 1 fat one
/// - Adding IReportScheduler methods? This test is completely unaffected!
/// - Dependencies are clear: reading the constructor tells you exactly what it does
/// </summary>
[TestFixture]
public class AdHocReportControllerTests
{
    private IReportGenerator _generator;
    private IReportExporter _exporter;
    private AdHocReportController _sut;

    [SetUp]
    public void SetUp()
    {
        _generator = Substitute.For<IReportGenerator>();
        _exporter = Substitute.For<IReportExporter>();
        _sut = new AdHocReportController(_generator, _exporter);
    }

    [Test]
    public void GenerateAndExportPdf_CallsGeneratorWithRequest()
    {
        // TODO: Arrange — create a ReportRequest
        //       Set up _generator.Generate(request).Returns(new Report { ... })
        //       Set up _exporter.Export(...).Returns(new byte[] { 1, 2, 3 })
        // TODO: Act — call _sut.GenerateAndExportPdf(request)
        // TODO: Assert — _generator.Received(1).Generate(request)

        throw new NotImplementedException("Implement this test");
    }

    [Test]
    public void GenerateAndExportPdf_ExportsAsPdf()
    {
        // TODO: Arrange — set up generator and exporter
        // TODO: Act — call _sut.GenerateAndExportPdf(request)
        // TODO: Assert — _exporter.Received(1).Export(Arg.Any<Report>(), ExportFormat.Pdf)
        //
        // Notice: this test only cares about IReportGenerator and IReportExporter.
        // No need to set up scheduling, email distribution, or history retrieval!
        // Compare this with the Bad version's test setup.

        throw new NotImplementedException("Implement this test");
    }

    [Test]
    public void GenerateAndExportPdf_ReturnsExportedBytes()
    {
        // TODO: Arrange — set up generator and exporter
        //       var expectedBytes = new byte[] { 0x25, 0x50, 0x44, 0x46 }; // PDF magic bytes
        //       _exporter.Export(Arg.Any<Report>(), ExportFormat.Pdf).Returns(expectedBytes)
        // TODO: Act — call _sut.GenerateAndExportPdf(request)
        // TODO: Assert — result == expectedBytes

        throw new NotImplementedException("Implement this test");
    }
}
