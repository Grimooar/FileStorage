using AutoMapper;
using FileStorage.Domain.Models;
using FileStorage.DTOs.Dto;

namespace FileStorage.Core.Extentions;

public class UserMapper : Profile
{
    public UserMapper() {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserCreateDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
    }
}