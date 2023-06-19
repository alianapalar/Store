
using AutoMapper;
using Entities.DTOs.User;
using Entities.DTOs.Product;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace StoreApp.Infrastructe.Mapper
{
    public class MappingProfile:Profile
    {
       public MappingProfile()
       {
         CreateMap<ProductDTOForInsertion,Product>();
         CreateMap<ProductDTOForUpdate,Product>().ReverseMap();
         CreateMap<UserInsertionForDTO,IdentityUser>();
         CreateMap<UserUpdateForDTO,IdentityUser>().ReverseMap();
       }   
    }
}