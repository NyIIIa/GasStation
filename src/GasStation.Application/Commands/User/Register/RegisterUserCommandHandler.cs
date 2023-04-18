using ErrorOr;
using GasStation.Application.Common.Authentication;
using GasStation.Application.Common.Interfaces.Persistence;
using GasStation.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Commands.User.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserRequest, ErrorOr<RegisterUserResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public RegisterUserCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ErrorOr<RegisterUserResponse>> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var isUserExists = _dbContext.Users.Any(u => u.Login == request.Login);
        if (isUserExists)
        {
            return Errors.User.DuplicateLogin;
        }

        var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Title == request.RoleTitle, cancellationToken);
        if (role is null)
        {
            return Errors.Role.TitleNotFound;
        }

        var passwordHash = PasswordHasher.Hash(request.Password);
        
        await _dbContext.Users.AddAsync(new Domain.Entities.User()
        {
            Login = request.Login,
            PasswordHash = passwordHash,
            Role = role
        }, cancellationToken);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0
            ? new RegisterUserResponse {IsCreated = true}
            : Errors.Database.Fail;
    }
}