using GasStation.Application.Common.Interfaces.Persistence;
using GasStation.Domain.Enums;
using MockQueryable.Moq;
using Moq;

namespace GasStation.Application.Tests.Invoice;

public static class InvoiceDbSetPreparation
{
    public static void SetUpInvoiceDbSet(Mock<IApplicationDbContext> dbContext)
    {
        var invoiceData = new List<Domain.Entities.Invoice>()
        {
            new Domain.Entities.Invoice()
            {
                Id = 1, 
                Title = "Fuel Invoice # 1",
                Consumer = "our GasStation",
                Provider = "Gas station OKO",
                TotalFuelQuantity = 200,
                TransactionType = TransactionType.Buy,
                Fuel = new Domain.Entities.Fuel()
                    { Id = 4, Title = "DieselPlus", Price = (decimal)35.43, Quantity = 563}
            }
        };

        var mockDbSet = invoiceData.AsQueryable().BuildMockDbSet();
        
        dbContext.Setup(m => m.Invoices).Returns(mockDbSet.Object);
        
        dbContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);
    }
}