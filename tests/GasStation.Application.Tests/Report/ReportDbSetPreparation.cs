using GasStation.Application.Common.Interfaces.Persistence;
using MockQueryable.Moq;
using Moq;

namespace GasStation.Application.Tests.Report;

public static class ReportDbSetPreparation
{
    public static void SetUpReportDbSet(Mock<IApplicationDbContext> dbContext)
    {
        var reportData = new List<Domain.Entities.Report>()
        {
            new Domain.Entities.Report()
            {
                Id = 1, 
                Title = "Some title for report",
            }
        };

        var mockDbSet = reportData.AsQueryable().BuildMockDbSet();
        
        dbContext.Setup(m => m.Reports).Returns(mockDbSet.Object);
        
        dbContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);
    }
}