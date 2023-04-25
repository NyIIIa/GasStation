using GasStation.Application.Common.Interfaces.Persistence;
using GasStation.Application.Queries.User.Login;
using MediatR;
using ErrorOr;
using GasStation.Application.Common.Behavior;
using GasStation.Application.Common.Interfaces.Authentication;
using Moq;
using Xunit;

namespace GasStation.Application.Tests.User.CommandHandlers;

public class LoginUserCommandHandlerTests
{
    private readonly IPipelineBehavior<LoginUserRequest, ErrorOr<LoginUserResponse>> _pipelineBehavior;
    private readonly Mock<IApplicationDbContext> _dbContext;
    private readonly LoginUserQueryHandler _loginUserQueryHandler;
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGenerator;
    
    public LoginUserCommandHandlerTests()
    {
        #region Configure necessary services for using login user command handler

        _pipelineBehavior = new ValidationBehavior<LoginUserRequest, ErrorOr<LoginUserResponse>>
            (new LoginUserRequestValidator());
        
        _dbContext = new Mock<IApplicationDbContext>();
        UserDbSetPreparation.SetUpUserAndRoleDbSet(_dbContext);

        _jwtTokenGenerator = new Mock<IJwtTokenGenerator>();
        _jwtTokenGenerator.Setup(x => x.GenerateJwtToken(It.IsAny<Domain.Entities.User>())).Returns("Token");
        
        #endregion
        
        _loginUserQueryHandler = new LoginUserQueryHandler(_dbContext.Object, _jwtTokenGenerator.Object);
    }

    [Fact]
    public async Task Should_Login_When_Query_Is_Valid()
    {
        //Arrange
        var loginUserRequest = new LoginUserRequest()
        {
            Login = "kokojambo22",
            Password = "qw123qw123Kokojambo!"
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(loginUserRequest, () => _loginUserQueryHandler.Handle(loginUserRequest, default), default);
        
        //Assert
        Assert.False(errorOr.IsError);
    }

    [Fact]
    public async Task Should_Not_Login_When_Query_Is_Not_Valid()
    {
        //Arrange
        var loginUserRequest = new LoginUserRequest();
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(loginUserRequest, () => _loginUserQueryHandler.Handle(loginUserRequest, default), default);

        //Assert
        Assert.True(errorOr.IsError);
    }

    [Fact]
    public async Task Should_Throw_InvalidCredentials_Error_When_User_Was_Not_Found()
    {
        //Arrange
        var loginUserRequest = new LoginUserRequest()
        {
            Login = "The wrong login",
            Password = "qw123qw123Kokojambo!"
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(loginUserRequest, () => _loginUserQueryHandler.Handle(loginUserRequest, default), default);
        var isInvalidCredentialsError = errorOr.Errors.Any(e => e.Code == "User.InvalidCredentials");
        
        //Assert
        Assert.True(isInvalidCredentialsError);
    }

    [Fact]
    public async Task Should_Throw_InvalidCredentials_Error_When_Password_Is_Invalid()
    {
        //Arrange
        var loginUserRequest = new LoginUserRequest()
        {
            Login = "kokojambo22",
            Password = "The wrong password"
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(loginUserRequest, () => _loginUserQueryHandler.Handle(loginUserRequest, default), default);
        var isInvalidCredentialsError = errorOr.Errors.Any(e => e.Code == "User.InvalidCredentials");

        //Assert
        Assert.True(isInvalidCredentialsError);
    }
}