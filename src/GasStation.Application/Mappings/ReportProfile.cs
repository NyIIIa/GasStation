using AutoMapper;
using GasStation.Application.Commands.Report.Create;
using GasStation.Application.Commands.Report.Update;
using GasStation.Application.Common.Interfaces.Services;
using GasStation.Application.Queries.Report.GetAll;
using GasStation.Domain.Entities;

namespace GasStation.Application.Mappings;
    
public class ReportProfile : Profile
{
    private readonly IDateTimeService _dateTimeService;

    public ReportProfile(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
        
        //Convert CreateReportRequest to Report
        CreateMap<CreateReportRequest, Report>()
            .ForMember(r => r.TotalPrice,
                opt => opt.MapFrom((src, report) => report.Invoices.Sum(s => s.TotalPrice)))
            .ForMember(r => r.TotalQuantity,
                opt => opt.MapFrom((src, report) => report.Invoices.Sum(s => s.TotalFuelQuantity)));

        //Convert UpdateReportRequest to Report
        CreateMap<UpdateReportRequest, Report>()
            .ForMember(r => r.Title, opt => opt.MapFrom(src => src.NewTitle));
        
        //Convert Report to GetAllReportsResponse
        CreateMap<Report, GetAllReportsResponse>()
            .ForMember(r => r.StartDate,
                opt => opt.MapFrom(src =>
                    (src.StartDate == 0)
                        ? "0"
                        : _dateTimeService.ConvertUnixTimeToDate(src.StartDate).ToString("MM/dd/yyyy")))
            .ForMember(r => r.EndDate,
                opt => opt.MapFrom(src =>
                    (src.EndDate == 0)
                        ? "0"
                        : _dateTimeService.ConvertUnixTimeToDate(src.EndDate).ToString("MM/dd/yyyy")))
            .ForMember(r => r.TransactionType, opt => opt.MapFrom(src => src.TransactionType.ToString()));
    }
}