using AutoMapper;
using GasStation.Application.Commands.Report.Create;
using GasStation.Application.Commands.Report.Update;
using GasStation.Application.Queries.Report.GetAll;
using GasStation.Domain.Entities;

namespace GasStation.Application.Mappings;
    
public class ReportProfile : Profile
{
    public ReportProfile()
    {
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
        CreateMap<Report, GetAllReportsResponse>();
    }
}