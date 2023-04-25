using GasStation.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MockQueryable.Moq;
using Moq;

namespace GasStation.Application.Tests.Fuel;

public static class FuelDbSetPreparation
{
    public static void SetUpFuelDbSet(Mock<IApplicationDbContext> dbContext)
    {
        var fuelData = new List<Domain.Entities.Fuel>()
        {
            new Domain.Entities.Fuel(){Id = 1, Title = "A98-EU", Price = (decimal) 54.50, Quantity = 500},
            new Domain.Entities.Fuel(){Id = 2, Title = "A93", Price = (decimal) 47.50, Quantity = 300},
            new Domain.Entities.Fuel(){Id = 3, Title = "A95", Price = (decimal) 23.50, Quantity = 400},
            new Domain.Entities.Fuel(){Id = 4, Title = "DieselPlus", Price = (decimal)35.43, Quantity = 563},
        };

        var mockDbSet = fuelData.AsQueryable().BuildMockDbSet();
        
        dbContext.Setup(m => m.Fuels).Returns(mockDbSet.Object);
        
        dbContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);
    }
}