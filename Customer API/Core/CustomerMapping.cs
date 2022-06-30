using AutoMapper;

namespace Customer_API.Core
{
    public class CustomerMapping : Profile
    {
        public CustomerMapping()
        {
            CreateMap<Model.Customer, DTOs.CustomerDto>().ReverseMap();
        }
    }
}
