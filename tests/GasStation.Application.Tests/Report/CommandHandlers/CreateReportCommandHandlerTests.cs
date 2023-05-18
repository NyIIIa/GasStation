using AutoMapper;
using GasStation.Application.Commands.Report.Create;
using GasStation.Application.Common.Interfaces.Persistence;
using MediatR;
using ErrorOr;
using GasStation.Application.Common.Behavior;
using GasStation.Application.Common.Interfaces.Services;
using GasStation.Application.Mappings;
using GasStation.Application.Tests.Invoice;
using GasStation.Domain.Enums;
using GasStation.Infrastructure.Services;
using Moq;
using Xunit;

namespace GasStation.Application.Tests.Report.CommandHandlers;

public class CreateReportCommandHandlerTests
{
    private readonly IPipelineBehavior<CreateReportRequest, ErrorOr<CreateReportResponse>> _pipelineBehavior;
    private readonly Mock<IApplicationDbContext> _dbContext;
    private readonly CreateReportCommandHandler _createReportCommandHandler;
    private readonly IDateTimeService _dateTimeService;
    private readonly IMapper _mapper;
    
    public CreateReportCommandHandlerTests()
    {
        #region Configure necessary services for using report command handler

        _pipelineBehavior = new ValidationBehavior<CreateReportRequest, ErrorOr<CreateReportResponse>>
            (new CreateReportRequestValidator());
        _dateTimeService = new DateTimeService();
        _dbContext = new Mock<IApplicationDbContext>();
        ReportDbSetPreparation.SetUpReportDbSet(_dbContext);
        InvoiceDbSetPreparation.SetUpInvoiceDbSet(_dbContext);

        _mapper = new MapperConfiguration(cfg =>
            cfg.AddProfile(new ReportProfile(_dateTimeService))).CreateMapper();

        #endregion
        
        _createReportCommandHandler = new CreateReportCommandHandler(_dbContext.Object, _mapper);
    }

    [Fact]
    public async Task Should_Create_When_Command_Is_Valid()
    {
        //Arrange
        var createReportRequest = new CreateReportRequest()
        {
            Title = "Some unique title for report",
            StartDate = 0,
            EndDate = 0,
            TransactionType = TransactionType.Buy
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(createReportRequest, () => _createReportCommandHandler.Handle(createReportRequest, default), default);

        //Assert
        Assert.False(errorOr.IsError);
        _dbContext.Verify(r => r.Reports.AddAsync(It.IsAny<Domain.Entities.Report>(), default), Times.Once);
    }

    [Fact]
    public async Task Should_Not_Create_When_Command_Is_Invalid()
    {
        //Arrange
        var createReportRequest = new CreateReportRequest();
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(createReportRequest, () => _createReportCommandHandler.Handle(createReportRequest, default), default);
        
        //Assert
        Assert.True(errorOr.IsError);
        _dbContext.Verify(r => r.Reports.AddAsync(It.IsAny<Domain.Entities.Report>(), default), Times.Never);
    }

    [Fact]
    public async Task Should_Throw_DuplicateTitle_Error_When_Report_Already_Exists()
    {
        //Arrange
        var createReportRequest = new CreateReportRequest()
        {
            Title = "Some title for report"
        };
        
        //Act
        var errorOr = await _pipelineBehavior.Handle(createReportRequest, () => _createReportCommandHandler.Handle(createReportRequest, default), default);
        var isDuplicateTitleError = errorOr.Errors.Any(e => e.Code == "Report.DuplicateTitle");

        //Assert
        Assert.True(isDuplicateTitleError);
    }
}