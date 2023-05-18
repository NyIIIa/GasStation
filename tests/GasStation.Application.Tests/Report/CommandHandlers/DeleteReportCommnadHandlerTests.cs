using AutoMapper;
using GasStation.Application.Commands.Report.Delete;
using GasStation.Application.Commands.Report.Update;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Application.Common.Behavior;
using Moq;
using Xunit;

namespace GasStation.Application.Tests.Report.CommandHandlers;

public class DeleteReportCommandHandlerTests
{
    private readonly IPipelineBehavior<DeleteReportRequest, ErrorOr<DeleteReportResponse>> _pipelineBehavior;
    private readonly Mock<IApplicationDbContext> _dbContext;
    private readonly DeleteReportCommandHandler _deleteReportCommandHandler;

    public DeleteReportCommandHandlerTests()
    {
        #region Configure necessary services for using report command handler

        _pipelineBehavior = new ValidationBehavior<DeleteReportRequest, ErrorOr<DeleteReportResponse>>
            (new DeleteReportRequestValidator());
        
        _dbContext = new Mock<IApplicationDbContext>();
        ReportDbSetPreparation.SetUpReportDbSet(_dbContext);
        
        #endregion
        
        _deleteReportCommandHandler = new DeleteReportCommandHandler(_dbContext.Object);
    }

    [Fact]
    public async Task Should_Delete_When_Command_Is_Valid()
    {
        //Arrange
        var deleteReportRequest = new DeleteReportRequest()
        {
            Id = 1
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(deleteReportRequest, () => _deleteReportCommandHandler.Handle(deleteReportRequest, default), default);

        //Assert
        Assert.False(errorOr.IsError);
        _dbContext.Verify(r => r.Reports.Remove(It.IsAny<Domain.Entities.Report>()), Times.Once);
    }

    [Fact]
    public async Task Should_Not_Delete_When_Command_Is_Invalid()
    {
        //Arrange
        var deleteReportRequest = new DeleteReportRequest();
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(deleteReportRequest, () => _deleteReportCommandHandler.Handle(deleteReportRequest, default), default);

        //Assert
        Assert.True(errorOr.IsError);
        _dbContext.Verify(r => r.Reports.Remove(It.IsAny<Domain.Entities.Report>()), Times.Never);
    }

    [Fact]
    public async Task Should_Throw_TitleNotFound_Error_When_Title_Was_Not_Found()
    {
        //Arrange
        var deleteReportRequest = new DeleteReportRequest()
        {
            Id = 423
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(deleteReportRequest, () => _deleteReportCommandHandler.Handle(deleteReportRequest, default), default);
        var isNotFoundTitleError = errorOr.Errors.Any(e => e.Code == "Report.TitleNotFound");
        
        //Assert
        Assert.True(isNotFoundTitleError);
    }
}