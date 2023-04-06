using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Domain.Errors;
using Microsoft.EntityFrameworkCore;


namespace GasStation.Application.Queries.User.Login;

public class LoginUserQueryHandler : IRequestHandler<LoginUserRequest, ErrorOr<LoginUserResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public LoginUserQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ErrorOr<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Login == request.Login, cancellationToken);
        if (user is null)
        {
            return Errors.User.InvalidCredentials;
        }
        
        //verify password
        //generate jwtToken

        return new LoginUserResponse { Token = ""};
    }
}