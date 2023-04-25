using AutoMapper;
using GasStation.Application.Commands.Fuel.Create;
using GasStation.Application.Common.Interfaces.Persistence;
using GasStation.Application.Common.Interfaces.Services;
using MediatR;
using ErrorOr;
using GasStation.Application.Common.Behavior;
using GasStation.Application.Mappings;
using Moq;
using Xunit;

namespace GasStation.Application.Tests.Fuel.CommandHandlers;

public class CreateFuelCommandHandlerTests
{
    private readonly IPipelineBehavior<CreateFuelRequest, ErrorOr<CreateFuelResponse>> _pipelineBehavior;
    private readonly Mock<IApplicationDbContext> _dbContext;
    private readonly CreateFuelCommandHandler _createFuelCommandHandler;
    private readonly Mock<IDateTimeService> _dateTimeService;
    private readonly IMapper _mapper;

    public CreateFuelCommandHandlerTests()
    {
        #region Configure necessary services for using fuel command handler

        _pipelineBehavior = new ValidationBehavior<CreateFuelRequest, ErrorOr<CreateFuelResponse>>
            (new CreateFuelRequestValidator());
        
        _dbContext = new Mock<IApplicationDbContext>();
        FuelDbSetPreparation.SetUpFuelDbSet(_dbContext);

        _dateTimeService = new Mock<IDateTimeService>();
        _mapper = new MapperConfiguration(cfg =>
            cfg.AddProfile(new FuelProfile(_dateTimeService.Object))).CreateMapper();

        #endregion


        _createFuelCommandHandler = new CreateFuelCommandHandler(_dbContext.Object, _mapper);
    }

    [Fact]
    public async Task Should_Create_When_Command_Is_Valid()
    {
        //Arrange
        var createFuelRequest = new CreateFuelRequest()
        {
            Title = "A95-EU",
            Price = (decimal) 44.50,
            Quantity = 100
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(createFuelRequest, () => _createFuelCommandHandler.Handle(createFuelRequest, default), default);
        
        //Assert
        Assert.False(errorOr.IsError); 
        _dbContext.Verify(f => f.Fuels.AddAsync(It.IsAny<Domain.Entities.Fuel>(),default), Times.Once);
    }
    
    [Fact]
    public async Task Should_Not_Create_When_Command_Is_Invalid()
    {
        //Arrange
        var createFuelRequest = new CreateFuelRequest()
        {
            Title = "",
            Price = (decimal) -3.4,
            Quantity = -55.5
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(createFuelRequest, () => _createFuelCommandHandler.Handle(createFuelRequest, default), default);
        
        //Assert
        Assert.True(errorOr.IsError); 
        _dbContext.Verify(f => f.Fuels.AddAsync(It.IsAny<Domain.Entities.Fuel>(),default), Times.Never);
    }

    [Fact]
    public async Task Should_Throw_DuplicateTitle_Error_When_Title_Already_Exists()
    {
        //Arrange
        var createFuelRequest = new CreateFuelRequest()
        {
            Title = "A93",
            Price = (decimal) 82.50,
            Quantity = 100
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(createFuelRequest, () => _createFuelCommandHandler.Handle(createFuelRequest, default), default);
        var isDuplicateTitleError = errorOr.Errors.Any(e => e.Code == "Fuel.DuplicateTitle");

        //Assert;
        Assert.True(isDuplicateTitleError);
    }
}