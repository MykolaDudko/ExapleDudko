using AutoMapper;
using WebApiCore.Data.DTO;
using WebApiCore.Data.Models;

namespace WebApiCore.Api.ProfileMappings
{
    public class CustomersProfile : Profile
    {
        public CustomersProfile()
        {
            CreateMap<Customer, CustomerDTO>();
        }
    }
}