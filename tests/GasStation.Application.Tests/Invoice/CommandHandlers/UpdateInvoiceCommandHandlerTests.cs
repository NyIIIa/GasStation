using AutoMapper;
using GasStation.Application.Commands.Invoice.Update;
using GasStation.Application.Common.Behavior;
using GasStation.Application.Common.Interfaces.Persistence;
using GasStation.Application.Common.Interfaces.Services;
using GasStation.Application.Mappings;
using GasStation.Application.Tests.Fuel;
using MediatR;
using ErrorOr;
using GasStation.Domain.Enums;
using Moq;
using Xunit;

namespace GasStation.Application.Tests.Invoice.CommandHandlers;

public class UpdateInvoiceCommandHandlerTests
{
    private readonly IPipelineBehavior<UpdateInvoiceRequest, ErrorOr<UpdateInvoiceResponse>> _pipelineBehavior;
    private readonly Mock<IApplicationDbContext> _dbContext;
    private readonly UpdateInvoiceCommandHandler _updateInvoiceCommandHandler;
    private readonly Mock<IDateTimeService> _dateTimeService;
    private readonly IMapper _mapper;

    public UpdateInvoiceCommandHandlerTests()
    {
        #region Configure necessary services for using invoice command handler

        _pipelineBehavior = new ValidationBehavior<UpdateInvoiceRequest, ErrorOr<UpdateInvoiceResponse>>
            (new UpdateInvoiceRequestValidator());
        
        _dbContext = new Mock<IApplicationDbContext>();
        InvoiceDbSetPreparation.SetUpInvoiceDbSet(_dbContext);
        FuelDbSetPreparation.SetUpFuelDbSet(_dbContext);
        
        _dateTimeService = new Mock<IDateTimeService>();
        _dateTimeService.Setup(d => d.UnixTimeNow).Returns(DateTimeOffset.Now.ToUnixTimeMilliseconds());
        
        _mapper = new MapperConfiguration(cfg =>
            cfg.AddProfile(new InvoiceProfile(_dateTimeService.Object))).CreateMapper();

        #endregion

        _updateInvoiceCommandHandler = new UpdateInvoiceCommandHandler(_dbContext.Object, _mapper);
    }

    [Fact]
    public async Task Should_Update_When_Command_Is_Valid()
    {
        //Arrange
        var updateInvoiceRequest = new UpdateInvoiceRequest()
        {
            Id = 55,
            NewTitle = "Fuel Purchase Order Invoice",
            Consumer = "our GasStation",
            Provider = "OKO",
            TotalFuelQuantity = 40,
            TransactionType = TransactionType.Buy
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(updateInvoiceRequest, () => _updateInvoiceCommandHandler.Handle(updateInvoiceRequest, default), default);
        
        //Assert
        Assert.False(errorOr.IsError);
        _dbContext.Verify(i => i.Invoices.Update(It.IsAny<Domain.Entities.Invoice>()), Times.Once);
    }

    [Fact]
    public async Task Should_Not_Update_When_Command_Is_Invalid()
    {
        //Arrange
        var updateInvoiceRequest = new UpdateInvoiceRequest();
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(updateInvoiceRequest, () => _updateInvoiceCommandHandler.Handle(updateInvoiceRequest, default), default);

        //Assert
        Assert.True(errorOr.IsError);
        _dbContext.Verify(i => i.Invoices.Update(It.IsAny<Domain.Entities.Invoice>()), Times.Never);
    }

    [Fact]
    public async Task Should_Throw_IdNotFound_Error_When_Invoice_Not_Found()
    {
        //Arrange
        var updateInvoiceRequest = new UpdateInvoiceRequest()
        {
            Id = 45,
            NewTitle = "Fuel Purchase Order Invoice",
            Consumer = "our GasStation",
            Provider = "OKO",
            TotalFuelQuantity = 40,
            TransactionType = TransactionType.Buy
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(updateInvoiceRequest, () => _updateInvoiceCommandHandler.Handle(updateInvoiceRequest, default), default);
        var isTitleNotFoundError = errorOr.Errors.Any(e => e.Code == "Invoice.InvalidTitle");
        
        //Assert
        Assert.True(isTitleNotFoundError);
    }
}