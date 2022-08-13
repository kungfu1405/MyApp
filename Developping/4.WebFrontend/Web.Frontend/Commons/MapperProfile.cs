using AutoMapper;
using DbData.Entities;
using DbData.Entities.Models;
using DbData.Protos;
using System;
using Google.Protobuf.WellKnownTypes;
using DbData.Entities.Models;
using Mic.Core.Entities;
using Mic.UserDb.Entities;
using UserDb.Protos;

namespace Web.Frontend.Commons
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DbData.Protos.ResponseMessage, AlertMessage>();
            CreateMap<KTPagination, DbData.Protos.PagingType>()
                .ForMember(m => m.Start, opt => opt.MapFrom(src => (src.Page - 1) * src.Perpage))
                .ForMember(m => m.Length, opt => opt.MapFrom(src => src.Perpage));
            CreateMap<KTSort, DbData.Protos.SortType>();

            CreateMap<ERole, RoleStruct>();
            CreateMap<RoleStruct, ERole>();

            CreateMap<EUsergroup, UsergroupStruct>();
            CreateMap<UsergroupStruct, EUsergroup>();

            CreateMap<EUser, UserStruct>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreatedDate, DateTimeKind.Utc).ToTimestamp()))
                .ForMember(dest => dest.LastActivity
                    , opt => opt.MapFrom(src => src.LastActivity.HasValue ? DateTime.SpecifyKind(src.LastActivity.Value, DateTimeKind.Utc).ToTimestamp() : null))
                .ForMember(dest => dest.LastChangePassword
                    , opt => opt.MapFrom(src => src.LastChangePassword.HasValue ? DateTime.SpecifyKind(src.LastChangePassword.Value, DateTimeKind.Utc).ToTimestamp() : null));

            CreateMap<UserStruct, EUser>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToDateTime()))
                .ForMember(dest => dest.LastActivity, opt => opt.MapFrom(src => src.LastActivity.ToDateTime()))
                .ForMember(dest => dest.LastChangePassword, opt => opt.MapFrom(src => src.LastChangePassword.ToDateTime()));

            // Language
            CreateMap<ELanguage, LanguageStruct>();
            CreateMap<LanguageStruct, ELanguage>();

            CreateMap<ELanguageData, LanguageDataStruct>();
            CreateMap<LanguageDataStruct, ELanguageData>();

            CreateMap<ETag, TagStruct>();
            CreateMap<TagStruct, ETag>();

            CreateMap<EExperience, ExperienceStruct>()
                .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => src.FromDate.HasValue ? DateTime.SpecifyKind(src.FromDate.Value, DateTimeKind.Utc).ToTimestamp() : null))
                .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => src.FromDate.HasValue ? DateTime.SpecifyKind(src.ToDate.Value, DateTimeKind.Utc).ToTimestamp() : null))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc).ToTimestamp()))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.PublishDate, DateTimeKind.Utc).ToTimestamp()));
            CreateMap<ExperienceStruct, EExperience>()
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToDateTime()))
                .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => src.FromDate.ToDateTime()))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToDateTime()))
                .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => src.ToDate.ToDateTime()))
                .ForMember(dest => dest.DestinationId, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.DestinationId) ? Guid.Empty : Guid.Parse(src.DestinationId)))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.AuthorId) ? Guid.Empty : Guid.Parse(src.AuthorId)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)));

            CreateMap<EExperienceLanguage, ExperienceLanguageStruct>();
            CreateMap<ExperienceLanguageStruct, EExperienceLanguage>();

            CreateMap<EExperienceSession, ExperienceSessionStruct>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc).ToTimestamp()));
            CreateMap<ExperienceSessionStruct, EExperienceSession>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToDateTime()));

            CreateMap<EExperienceSessionImage, ExperienceSessionImageStruct>();
            CreateMap<ExperienceSessionImageStruct, EExperienceSessionImage>();


            CreateMap<EDestinationLanguage, DestinationLanguageStruct>();
            CreateMap<DestinationLanguageStruct, EDestinationLanguage>();

            CreateMap<EDestinationOverview, DestinationOverviewStruct>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc).ToTimestamp()));
            CreateMap<DestinationOverviewStruct, EDestinationOverview>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToDateTime()));

            CreateMap<EDestination, DestinationStruct>();
            CreateMap<DestinationStruct, EDestination>();

            CreateMap<DestinationFilters, DestinationFilter>();
            CreateMap<DestinationFilter, DestinationFilters>();


            CreateMap<EAttractionLanguage, AttractionLanguageStruct>();
            CreateMap<AttractionLanguageStruct, EAttractionLanguage>();

            CreateMap<EAttraction, AttractionStruct>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc).ToTimestamp()));
            CreateMap<AttractionStruct, EAttraction>()
                .ForMember(dest => dest.DestinationId, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.DestinationId) ? Guid.Empty : Guid.Parse(src.DestinationId)))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToDateTime()));

            CreateMap<AttractionFilters, AttractionFilter>();
            CreateMap<AttractionFilter, AttractionFilters>();
            // user profile
            //CreateMap<EUserProfile, UserProfileStruct>();
            CreateMap<UserProfileStruct, EUserProfile > ();

            //Account
            //CreateMap<SigninModel, SigninRequest>();
            //CreateMap<SigninRequest,SigninModel>();
            CreateMap<SignupRequest, SignUpModel>();
            CreateMap<SignUpModel, SignupRequest>();
        }
    }
}
