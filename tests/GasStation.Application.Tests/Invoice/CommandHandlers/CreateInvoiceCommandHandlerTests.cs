using AutoMapper;
using GasStation.Application.Commands.Fuel.Create;
using GasStation.Application.Commands.Invoice.Create;
using GasStation.Application.Common.Interfaces.Persistence;
using GasStation.Application.Common.Interfaces.Services;
using MediatR;
using ErrorOr;
using GasStation.Application.Common.Behavior;
using GasStation.Application.Mappings;
using GasStation.Application.Tests.Fuel;
using GasStation.Domain.Enums;
using Moq;
using Xunit;

namespace GasStation.Application.Tests.Invoice.CommandHandlers;

public class CreateInvoiceCommandHandlerTests
{
    private readonly IPipelineBehavior<CreateInvoiceRequest, ErrorOr<CreateInvoiceResponse>> _pipelineBehavior;
    private readonly Mock<IApplicationDbContext> _dbContext;
    private readonly CreateInvoiceCommandHandler _createInvoiceCommandHandler;
    private readonly Mock<IDateTimeService> _dateTimeService;
    private readonly IMapper _mapper;

    public CreateInvoiceCommandHandlerTests()
    {
        #region Configure necessary services for using invoice command handler

        _pipelineBehavior = new ValidationBehavior<CreateInvoiceRequest, ErrorOr<CreateInvoiceResponse>>
            (new CreateInvoiceRequestValidator());
        
        _dbContext = new Mock<IApplicationDbContext>();
        InvoiceDbSetPreparation.SetUpInvoiceDbSet(_dbContext);
        FuelDbSetPreparation.SetUpFuelDbSet(_dbContext);
        
        _dateTimeService = new Mock<IDateTimeService>();
        _dateTimeService.Setup(d => d.UnixTimeNow).Returns(DateTimeOffset.Now.ToUnixTimeMilliseconds());
        
        _mapper = new MapperConfiguration(cfg =>
            cfg.AddProfile(new InvoiceProfile(_dateTimeService.Object))).CreateMapper();

        #endregion

        _createInvoiceCommandHandler = new CreateInvoiceCommandHandler(_dbContext.Object, _mapper);
    }

    [Fact]
    public async Task Should_Create_When_Command_Is_Valid()
    {
        //Arrange
        var createInvoiceRequest = new CreateInvoiceRequest()
        {
            Title = "DieselPlus Fuel Invoice",
            Consumer = "our GasStation",
            Provider = "GasStation RLS",
            FuelTitle = "DieselPlus",
            TotalFuelQuantity = 50,
            TransactionType = TransactionType.Sell
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(createInvoiceRequest, () => _createInvoiceCommandHandler.Handle(createInvoiceRequest, default), default);
        
        //Assert
        Assert.False(errorOr.IsError);
        _dbContext.Verify(i => i.Invoices.AddAsync(It.IsAny<Domain.Entities.Invoice>(), default), Times.Once);
    }

    [Fact]
    public async Task Should_Not_Create_When_Command_Is_Invalid()
    {
        //Arrange
        var createInvoiceRequest = new CreateInvoiceRequest();
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(createInvoiceRequest, () => _createInvoiceCommandHandler.Handle(createInvoiceRequest, default), default);
        
        //Assert
        Assert.True(errorOr.IsError);
        _dbContext.Verify(i => i.Invoices.AddAsync(It.IsAny<Domain.Entities.Invoice>(), default), Times.Never);
    }

    [Fact]
    public async Task Should_Throw_DuplicateTitle_Error_When_Invoice_Already_Exists()
    {
        //Arrange
        var createInvoiceRequest = new CreateInvoiceRequest()
        {
            Title = "Fuel Invoice # 1",
            Consumer = "our GasStation",
            Provider = "GasStation RLS",
            FuelTitle = "DieselPlus",
            TotalFuelQuantity = 80,
            TransactionType = TransactionType.Sell
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(createInvoiceRequest, () => _createInvoiceCommandHandler.Handle(createInvoiceRequest, default), default);
        var isDuplicateTitleError = errorOr.Errors.Any(e => e.Code == "Invoice.DuplicateTitle");
        
        //Assert
        Assert.True(isDuplicateTitleError);
    }

    [Fact]
    public async Task Should_Throw_TitleNotFound_Error_When_Fuel_Was_Not_Found()
    {
        //Arrange
        var createInvoiceRequest = new CreateInvoiceRequest()
        {
            Title = "Fleet Fueling Invoice",
            Consumer = "our GasStation",
            Provider = "GasStation RLS",
            FuelTitle = "A95-pulls",
            TotalFuelQuantity = 80,
            TransactionType = TransactionType.Buy
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(createInvoiceRequest, () => _createInvoiceCommandHandler.Handle(createInvoiceRequest, default), default);
        var isFuelTitleNotFoundError = errorOr.Errors.Any(e => e.Code == "Fuel.InvalidTitle");
        
        //Assert
        Assert.True(isFuelTitleNotFoundError);
    }
}