using AutoMapper;
using GasStation.Application.Commands.Invoice.Delete;
using GasStation.Application.Commands.Invoice.Update;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Application.Common.Behavior;
using GasStation.Application.Common.Interfaces.Services;
using GasStation.Application.Mappings;
using GasStation.Application.Tests.Fuel;
using Moq;
using Xunit;

namespace GasStation.Application.Tests.Invoice.CommandHandlers;

public class DeleteInvoiceCommandHandlerTests
{
    private readonly IPipelineBehavior<DeleteInvoiceRequest, ErrorOr<DeleteInvoiceResponse>> _pipelineBehavior;
    private readonly Mock<IApplicationDbContext> _dbContext;
    private readonly DeleteInvoiceCommandHandler _deleteInvoiceCommandHandler;

    public DeleteInvoiceCommandHandlerTests()
    {
        #region Configure necessary services for using invoice command handler

        _pipelineBehavior = new ValidationBehavior<DeleteInvoiceRequest, ErrorOr<DeleteInvoiceResponse>>
            (new DeleteInvoiceRequestValidator());
        
        _dbContext = new Mock<IApplicationDbContext>();
        InvoiceDbSetPreparation.SetUpInvoiceDbSet(_dbContext);
        FuelDbSetPreparation.SetUpFuelDbSet(_dbContext);
        
        #endregion

        _deleteInvoiceCommandHandler = new DeleteInvoiceCommandHandler(_dbContext.Object);
    }

    [Fact]
    public async Task Should_Delete_When_Command_Is_Valid()
    {
        //Arrange
        var deleteInvoiceRequest = new DeleteInvoiceRequest()
        {
            Id = 1,
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(deleteInvoiceRequest, () => _deleteInvoiceCommandHandler.Handle(deleteInvoiceRequest, default), default);

        //Assert
        Assert.False(errorOr.IsError);
        _dbContext.Verify(i => i.Invoices.Remove(It.IsAny<Domain.Entities.Invoice>()), Times.Once);
    }

    [Fact]
    public async Task Should_Not_Delete_When_Command_Is_Invalid()
    {
        //Arrange
        var deleteInvoiceRequest = new DeleteInvoiceRequest();
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(deleteInvoiceRequest, () => _deleteInvoiceCommandHandler.Handle(deleteInvoiceRequest, default), default);

        //Assert
        Assert.True(errorOr.IsError);
        _dbContext.Verify(i => i.Invoices.Remove(It.IsAny<Domain.Entities.Invoice>()), Times.Never);
    }

    [Fact]
    public async Task Should_Throw_IdNotFound_Error_When_Invoice_Was_Not_Found()
    {
        //Arrange
        var deleteInvoiceRequest = new DeleteInvoiceRequest()
        {
            Id = 54
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(deleteInvoiceRequest, () => _deleteInvoiceCommandHandler.Handle(deleteInvoiceRequest, default), default);
        var isTitleNotFoundError = errorOr.Errors.Any(e => e.Code == "Invoice.InvalidTitle");

        //Assert
        Assert.True(isTitleNotFoundError);
    }
}