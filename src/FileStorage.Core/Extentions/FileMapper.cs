﻿using AutoMapper;
using FileStorage.Domain.Models;
using FileStorage.DTOs.Dto;
using File = FileStorage.Domain.Models.File;

namespace FileStorage.Core.Extentions
{
    public class FileMapper : Profile
    {
        public FileMapper()
        {
            CreateMap<File, FileDataDto>()
                .ForMember(dest => dest.FileParts, opt => opt.MapFrom(src => src.FileParts))
                .ReverseMap();

            CreateMap<FileDataCreateDto, File>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.ContentType))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.FileParts, opt => opt.Ignore());
        }
    }
}