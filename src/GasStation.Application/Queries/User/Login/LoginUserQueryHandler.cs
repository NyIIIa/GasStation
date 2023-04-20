using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Application.Common.Authentication;
using GasStation.Application.Common.Interfaces.Authentication;
using GasStation.Domain.Errors;
using Microsoft.EntityFrameworkCore;


namespace GasStation.Application.Queries.User.Login;

public class LoginUserQueryHandler : IRequestHandler<LoginUserRequest, ErrorOr<LoginUserResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserQueryHandler(IApplicationDbContext dbContext, IJwtTokenGenerator jwtTokenGenerator)
    {
        _dbContext = dbContext;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public async Task<ErrorOr<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Login == request.Login, cancellationToken);
        if (user is null)
        {
            return Errors.User.InvalidCredentials;
        }

        var isPasswordValid = PasswordHasher.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            return Errors.User.InvalidCredentials;
        }

        var jwtToken = _jwtTokenGenerator.GenerateJwtToken(user);

        return new LoginUserResponse { Token = jwtToken};
    }
}