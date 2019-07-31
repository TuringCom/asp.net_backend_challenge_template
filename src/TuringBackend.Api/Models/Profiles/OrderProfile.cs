using AutoMapper;

namespace TuringBackend.Models
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Orders, Order>();
        }
    }
}