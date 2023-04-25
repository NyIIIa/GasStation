using AutoMapper;
using GasStation.Application.Commands.Report.Update;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Application.Common.Behavior;
using GasStation.Application.Mappings;
using Moq;
using Xunit;

namespace GasStation.Application.Tests.Report.CommandHandlers;

public class UpdateReportCommandHandlerTests
{
    private readonly IPipelineBehavior<UpdateReportRequest, ErrorOr<UpdateReportResponse>> _pipelineBehavior;
    private readonly Mock<IApplicationDbContext> _dbContext;
    private readonly UpdateReportCommandHandler _updateReportCommandHandler;
    private readonly IMapper _mapper;

    public UpdateReportCommandHandlerTests()
    {
        #region Configure necessary services for using report command handler

        _pipelineBehavior = new ValidationBehavior<UpdateReportRequest, ErrorOr<UpdateReportResponse>>
            (new UpdateReportRequestValidator());
        
        _dbContext = new Mock<IApplicationDbContext>();
        ReportDbSetPreparation.SetUpReportDbSet(_dbContext);

        _mapper = new MapperConfiguration(cfg =>
            cfg.AddProfile(new ReportProfile())).CreateMapper();

        #endregion
        
        _updateReportCommandHandler = new UpdateReportCommandHandler(_dbContext.Object, _mapper);
    }

    [Fact]
    public async Task Should_Update_When_Command_Is_Valid()
    {
        //Arrange
        var updateReportRequest = new UpdateReportRequest()
        {
            CurrentTitle = "Some title for report",
            NewTitle = "Some new title for report"
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(updateReportRequest, () => _updateReportCommandHandler.Handle(updateReportRequest, default), default);

        //Assert
        Assert.False(errorOr.IsError);
        _dbContext.Verify(r => r.Reports.Update(It.IsAny<Domain.Entities.Report>()), Times.Once);
    }

    [Fact]
    public async Task Should_Not_Update_When_Command_Is_Invalid()
    {
        //Arrange
        var updateReportRequest = new UpdateReportRequest();
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(updateReportRequest, () => _updateReportCommandHandler.Handle(updateReportRequest, default), default);
        
        //Assert
        Assert.True(errorOr.IsError);
        _dbContext.Verify(r => r.Reports.Update(It.IsAny<Domain.Entities.Report>()), Times.Never);
    }

    [Fact]
    public async Task Should_Throw_DuplicateNewTitle_Error_When_New_Title_Already_Exists()
    {
        //Arrange
        var updateReportRequest = new UpdateReportRequest()
        {
            CurrentTitle = "Some title for report",
            NewTitle = "Some title for report"
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(updateReportRequest, () => _updateReportCommandHandler.Handle(updateReportRequest, default), default);
        var isDuplicateNewTitleError = errorOr.Errors.Any(e => e.Code == "Report.DuplicateNewTitle");
        
        //Assert
        Assert.True(isDuplicateNewTitleError);
    }

    [Fact]
    public async Task Should_Throw_TitleNotFound_Error_When_Current_Title_Was_Not_Found()
    {
        //Arrange
        var updateReportRequest = new UpdateReportRequest()
        {
            CurrentTitle = "The wrong title of report",
            NewTitle = "The new title of report"
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(updateReportRequest, () => _updateReportCommandHandler.Handle(updateReportRequest, default), default);
        var isTitleNotFoundError = errorOr.Errors.Any(e => e.Code == "Report.TitleNotFound");

        //Assert
        Assert.True(isTitleNotFoundError);
    }
}