using AutoMapper;

namespace TuringBackend.Models
{
    public class AttributeProfile : Profile
    {
        public AttributeProfile()
        {
            CreateMap<Attribute, AttributeValueByAttribute>();
        }
    }
}