using AutoMapper;
using PspApi.DTO.ResponseDTOs;
using PspApi.Models;

namespace PspApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Merchant, MerchantDTO>();
            CreateMap<Order, OrderDTO>();
        }
    }
}
