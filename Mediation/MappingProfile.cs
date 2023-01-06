using AutoMapper;
using CavuTechTest.DAL.Entities;
using CavuTechTest.Models.Request;
using CavuTechTest.Models.Response;

namespace CavuTechTest.Mediation
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookingResponse, Booking>().ReverseMap();
            CreateMap<CreateBookingRequest, Booking>().ReverseMap();
            CreateMap<UpdateBookingRequest, Booking>().ReverseMap();
            CreateMap<UpdateBookingResponse, Booking>().ReverseMap();
            CreateMap<Booking, Models.Booking>().ReverseMap();

        }
    }
}