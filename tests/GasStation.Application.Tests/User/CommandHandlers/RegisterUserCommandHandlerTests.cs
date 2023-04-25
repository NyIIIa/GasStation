using AutoMapper;
using GasStation.Application.Commands.Report.Create;
using GasStation.Application.Commands.User.Register;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Application.Common.Behavior;
using GasStation.Application.Mappings;
using GasStation.Application.Tests.Invoice;
using GasStation.Application.Tests.Report;
using Moq;
using Xunit;

namespace GasStation.Application.Tests.User.CommandHandlers;

public class RegisterUserCommandHandlerTests
{
    private readonly IPipelineBehavior<RegisterUserRequest, ErrorOr<RegisterUserResponse>> _pipelineBehavior;
    private readonly Mock<IApplicationDbContext> _dbContext;
    private readonly RegisterUserCommandHandler _registerUserCommandHandler;

    public RegisterUserCommandHandlerTests()
    {
        #region Configure necessary services for using register user command handler

        _pipelineBehavior = new ValidationBehavior<RegisterUserRequest, ErrorOr<RegisterUserResponse>>
            (new RegisterUserRequestValidator());
        
        _dbContext = new Mock<IApplicationDbContext>();
        UserDbSetPreparation.SetUpUserAndRoleDbSet(_dbContext);
        
        #endregion
        
        _registerUserCommandHandler = new RegisterUserCommandHandler(_dbContext.Object);
    }

    [Fact]
    public async Task Should_Register_When_Command_Is_Valid()
    {
        //Arrange
        var registerUserRequest = new RegisterUserRequest()
        {
            Login = "wwwroot",
            Password = "PassW0RD3453",
            RoleTitle = "Admin"
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(registerUserRequest, () => _registerUserCommandHandler.Handle(registerUserRequest, default), default);
        
        //Assert
        Assert.False(errorOr.IsError);
        _dbContext.Verify(u => u.Users.AddAsync(It.IsAny<Domain.Entities.User>(), default), Times.Once);
    }

    [Fact]
    public async Task Should_Not_Register_When_Command_Is_Invalid()
    {
        //Arrange
        var registerUserRequest = new RegisterUserRequest();
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(registerUserRequest, () => _registerUserCommandHandler.Handle(registerUserRequest, default), default);
        
        //Assert
        Assert.True(errorOr.IsError);
        _dbContext.Verify(u => u.Users.AddAsync(It.IsAny<Domain.Entities.User>(), default), Times.Never);
    }

    [Fact]
    public async Task Should_Throw_DuplicateLogin_Error_When_User_Already_Exists()
    {
        //Arrange
        var registerUserRequest = new RegisterUserRequest()
        {
            Login = "kokojambo22",
            Password = "asd44VVSD32",
            RoleTitle = "Admin"
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(registerUserRequest, () => _registerUserCommandHandler.Handle(registerUserRequest, default), default);
        var isDuplicateLoginError = errorOr.Errors.Any(e => e.Code == "User.DuplicateLogin");
        
        //Assert
        Assert.True(isDuplicateLoginError);
    }

    [Fact]
    public async Task Should_Throw_TitleNotFound_Error_When_Is_Not_Found()
    {
        //Arrange
        var registerUserRequest = new RegisterUserRequest()
        {
            Login = "SS32g",
            Password = "sds422E12C",
            RoleTitle = "Manager"
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(registerUserRequest, () => _registerUserCommandHandler.Handle(registerUserRequest, default), default);
        var isTitleNotFoundError = errorOr.Errors.Any(e => e.Code == "Role.NotFound");
        
        //Assert
        Assert.True(isTitleNotFoundError);
    }
}
