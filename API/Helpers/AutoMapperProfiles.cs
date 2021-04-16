using System;
using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<From,To>()
            CreateMap<AppUser,MemberDto>()
                .ForMember(x=>x.PhotoUrl, opt => opt
                    .MapFrom(s=>s.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(scr=>scr.DateOfBirth.CalculateAge()));

            CreateMap<Photo,PhotoDto>();

            CreateMap<MemberUpdateDto,AppUser>();

            CreateMap<RegisterDto,AppUser>();

            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => 
                    opt.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.RecipientPhotoUrl, opt => 
                    opt.MapFrom(s => s.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
        
    }
}