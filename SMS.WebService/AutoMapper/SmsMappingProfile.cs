using AutoMapper;
using SMS.Shared.Enums;
using SMS.Shared.Models;
using SMS.WebService.Dtos.Request;
using SMS.WebService.Dtos.Response;

namespace SMS.WebService.AutoMapper
{
    public class SmsMappingProfile : Profile
    {
        public SmsMappingProfile()
        {
            CreateMap<Sms, GetAllSmsesResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())); // from database entity to response model
            CreateMap<Sms, GetSmsByIdResponse>() // from database entity to response model
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
