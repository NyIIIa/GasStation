using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Queries.User.Login;

public class LoginUserQueryHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public LoginUserQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Login == request.Login, cancellationToken);
        if (user is null)
        {
            throw new Exception("A user with specified login doesn't exist!");
        }
        
        //verify password
        //generate jwtToken

        return new LoginUserResponse { Token = ""};
    }
}