using AutoMapper;
using GasStation.Application.Commands.Invoice.Create;
using GasStation.Application.Commands.Invoice.Update;
using GasStation.Application.Common.Interfaces.Services;
using GasStation.Application.Queries.Invoice.GetAll;
using GasStation.Domain.Entities;

namespace GasStation.Application.Mappings;

public class InvoiceProfile : Profile
{
    private readonly IDateTimeService _dateTimeService;

    public InvoiceProfile(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;

        //Convert CreateInvoiceRequest to Invoice
        CreateMap<CreateInvoiceRequest, Invoice>()
            .ForMember(i => i.CreatedDate, opt => opt.MapFrom(src => _dateTimeService.UnixTimeNow))
            .ForMember(i => i.TotalPrice,
                opt => opt.MapFrom((src, invoice) => invoice.Fuel.Price * (decimal) src.TotalFuelQuantity));

        //Convert UpdateInvoiceRequest to Invoice
        CreateMap<UpdateInvoiceRequest, Invoice>()
            .ForMember(i => i.Title, opt => opt.MapFrom(src => src.NewTitle))
            .ForMember(i => i.TotalPrice,
                opt => opt.MapFrom((src, invoice) => invoice.Fuel.Price * (decimal) src.TotalFuelQuantity));

        //Convert Invoice to GetAllInvoicesResponse
        CreateMap<Invoice, GetAllInvoicesResponse>()
            .ForMember(i => i.CreatedDate,
                opt => opt.MapFrom(src =>
                    (src.CreatedDate == 0)
                        ? "0"
                        : _dateTimeService.ConvertUnixTimeToDate(src.CreatedDate).ToString("MM/dd/yyyy")))
            .ForMember(i => i.TransactionType, opt => opt.MapFrom(src => src.TransactionType.ToString()));
    }
}