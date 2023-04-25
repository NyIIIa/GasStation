using GasStation.Application.Common.Authentication;
using GasStation.Application.Common.Interfaces.Persistence;
using GasStation.Domain.Entities;
using MockQueryable.Moq;
using Moq;

namespace GasStation.Application.Tests.User;

public static class UserDbSetPreparation
{
    public static void SetUpUserAndRoleDbSet(Mock<IApplicationDbContext> dbContext)
    {
        var userData = new List<Domain.Entities.User>()
        {
            new Domain.Entities.User()
            {
                Id = 1,
                Login = "kokojambo22",
                PasswordHash = PasswordHasher.Hash("qw123qw123Kokojambo!")
            }
        };

        var roleData = new List<Role>()
        {
            new Role() { Id = 1, Title = "Admin"},
            new Role() {Id = 2, Title = "User"}
        };

        var mockUserDbSet = userData.AsQueryable().BuildMockDbSet();
        var mockRoleDbSet = roleData.AsQueryable().BuildMockDbSet();
        
        dbContext.Setup(m => m.Users).Returns(mockUserDbSet.Object);
        dbContext.Setup(m => m.Roles).Returns(mockRoleDbSet.Object);
        
        dbContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);
    }
}