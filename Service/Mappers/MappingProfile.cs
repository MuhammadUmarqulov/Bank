using AutoMapper;
using BankNTProject.Domain.Entities;
using BankNTProject.Service.DTOs.CreditDTOs;
using BankNTProject.Service.DTOs.TransientDTOs;
using BankNTProject.Service.DTOs.UserDTOs;

namespace BankNTProject.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserForCreation>().ReverseMap();
            CreateMap<Credit, CreditForCreation>().ReverseMap();
            CreateMap<Card, UserForUpdate>().ReverseMap();
            CreateMap<Transient, TransientForCreation>().ReverseMap();
        }
    }
}
