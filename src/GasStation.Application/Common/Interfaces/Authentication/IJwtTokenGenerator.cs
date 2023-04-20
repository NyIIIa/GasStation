using GasStation.Domain.Entities;

namespace GasStation.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(User user);
}