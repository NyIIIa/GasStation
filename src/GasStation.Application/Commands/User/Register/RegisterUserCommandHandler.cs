using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.User.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserRequest, RegisterUserResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public RegisterUserCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var isUserExists = _dbContext.Users.Any(u => u.Login == request.Login);
        if (isUserExists)
        {
            throw new Exception("The user with specified login already exists!");
        }
        
        //generate user's password hash & password salt
        
        await _dbContext.Users.AddAsync(new Domain.Entities.User()
        {
            Login = request.Login,
            PasswordHash = "",
            PasswordSalt = "",
            Role = await _dbContext.Roles
                .FirstOrDefaultAsync(r => r.Title == request.RoleTitle, cancellationToken)
                ?? throw new Exception("The specified role wasn't found!")
        }, cancellationToken);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0 ? new RegisterUserResponse {IsCreated = true} 
                    : throw new Exception("Something went wrong!");
    }
}