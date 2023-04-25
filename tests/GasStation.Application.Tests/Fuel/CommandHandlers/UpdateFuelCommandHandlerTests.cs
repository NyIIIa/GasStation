using AutoMapper;
using GasStation.Application.Commands.Fuel.Update;
using GasStation.Application.Common.Behavior;
using GasStation.Application.Common.Interfaces.Persistence;
using GasStation.Application.Common.Interfaces.Services;
using GasStation.Application.Mappings;
using MediatR;
using ErrorOr;
using Moq;
using Xunit;

namespace GasStation.Application.Tests.Fuel.CommandHandlers;

public class UpdateFuelCommandHandlerTests
{
    private readonly IPipelineBehavior<UpdateFuelRequest, ErrorOr<UpdateFuelResponse>> _pipelineBehavior;
    private readonly Mock<IApplicationDbContext> _dbContext;
    private readonly UpdateFuelCommandHandler _updateFuelCommandHandler;
    private readonly Mock<IDateTimeService> _dateTimeService;
    private readonly IMapper _mapper;

    public UpdateFuelCommandHandlerTests()
    {
        #region Configure necessary services for using fuel command handler

        _pipelineBehavior = new ValidationBehavior<UpdateFuelRequest, ErrorOr<UpdateFuelResponse>>
            (new UpdateFuelRequestValidator());
        
        _dbContext = new Mock<IApplicationDbContext>();
        FuelDbSetPreparation.SetUpFuelDbSet(_dbContext);

        _dateTimeService = new Mock<IDateTimeService>();
        _dateTimeService.Setup(d => d.UnixTimeNow).Returns(DateTimeOffset.Now.ToUnixTimeMilliseconds());
        
        _mapper = new MapperConfiguration(cfg =>
            cfg.AddProfile(new FuelProfile(_dateTimeService.Object))).CreateMapper();

        #endregion


        _updateFuelCommandHandler = new UpdateFuelCommandHandler(_dbContext.Object, _mapper);
    }

    [Fact]
    public async Task Should_Update_When_Command_Is_Valid()
    {
        //Arrange
        var updateFuelRequest = new UpdateFuelRequest()
        {
            Id = 1,
            NewPrice = (decimal) 35.74
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(updateFuelRequest, () => _updateFuelCommandHandler.Handle(updateFuelRequest, default), default);

        //Assert
        Assert.False(errorOr.IsError);
        _dbContext.Verify(f => f.Fuels.Update(It.IsAny<Domain.Entities.Fuel>()), Times.Once);
    }
    

    [Fact]
    public async Task Should_Not_Update_When_Command_Is_Invalid()
    {
        //Arrange
        var updateFuelRequest = new UpdateFuelRequest()
        {
            Id = -4,
            NewPrice = (decimal) -35.32
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(updateFuelRequest,
            () => _updateFuelCommandHandler.Handle(updateFuelRequest, default), default);
        
        //Assert
        Assert.True(errorOr.IsError);
        _dbContext.Verify(f => f.Fuels.Update(It.IsAny<Domain.Entities.Fuel>()), Times.Never);
    }

    [Fact]
    public async Task Should_Throw_TitleNotFound_Error_When_Id_Was_Not_Found()
    {
        //Arrange
        var updateFuelRequest = new UpdateFuelRequest()
        {
            Id = 55,
            NewPrice = (decimal)33.55
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(updateFuelRequest,
            () => _updateFuelCommandHandler.Handle(updateFuelRequest, default), default);
        var isTitleNotFoundError = errorOr.Errors.Any(e => e.Code == "Fuel.InvalidTitle");

        //Assert
        Assert.True(isTitleNotFoundError);
    }
}