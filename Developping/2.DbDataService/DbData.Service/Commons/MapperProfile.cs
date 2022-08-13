using AutoMapper;
using DbData.Protos;
using DbData.Entities;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Linq;
using Mic.UserDb.Entities;
using UserDb.Protos;
using DbData.Entities.Models;
using Mic.Core.Entities;

namespace DbData.Service.Commons
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PagingType, DatatablePaging>();
            CreateMap<DatatablePaging, PagingType>();
            CreateMap<SortType, DatatableSort>();
            CreateMap<DatatableSort, SortType>();

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

            CreateMap<EWebController, WebControllerStruct>();
            CreateMap<WebControllerStruct, EWebController>();

            CreateMap<EWebPage, WebPageStruct>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc).ToTimestamp()))
                .ForMember(dest => dest.ModifyDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.ModifyDate, DateTimeKind.Utc).ToTimestamp()));
            CreateMap<WebPageStruct, EWebPage>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToDateTime()))
                .ForMember(dest => dest.ModifyDate, opt => opt.MapFrom(src => src.ModifyDate.ToDateTime()));

            CreateMap<EWebNavigator, WebNavigatorStruct>();
            CreateMap<WebNavigatorStruct, EWebNavigator>();

            CreateMap<EContinent, ContinentStruct>();
            CreateMap<CountryStruct, ECountry>();
            CreateMap<ECountry, CountryStruct>();
            CreateMap<CountryFilter, CountryFilters>();

            CreateMap<StateStruct, EState>();
            CreateMap<EState, StateStruct>();
            CreateMap<StateFilter, StateFilters>();

            CreateMap<CityStruct, ECity>();
            CreateMap<ECity, CityStruct>();
            CreateMap<CityFilter, CityFilters>();

            CreateMap<ETag, TagStruct>();
            CreateMap<TagStruct, ETag>();
            // Language
            CreateMap<ELanguage, LanguageStruct>();
            CreateMap<LanguageStruct, ELanguage>();

            CreateMap<ELanguageData, LanguageDataStruct>();
            CreateMap<LanguageDataStruct, ELanguageData>();

            CreateMap<ELanguageDataLocal, LanguageDataStruct>()
                .ForMember(dest => dest.IsGroup, opt => opt.Ignore());
            CreateMap<LanguageDataStruct, ELanguageDataLocal>();

            // Experience
            CreateMap<EExperienceLanguage, ExperienceLanguageStruct>();
            CreateMap<ExperienceLanguageStruct, EExperienceLanguage>();

            CreateMap<EExperienceSession, ExperienceSessionStruct>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc).ToTimestamp()));
            CreateMap<ExperienceSessionStruct, EExperienceSession>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToDateTime()));

            CreateMap<EExperienceSessionImage, ExperienceSessionImageStruct>();
            CreateMap<ExperienceSessionImageStruct, EExperienceSessionImage>();

            CreateMap<EExperience, ExperienceStruct>()
                .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => src.FromDate.HasValue ? DateTime.SpecifyKind(src.FromDate.Value, DateTimeKind.Utc).ToTimestamp() : null))
                .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => src.FromDate.HasValue ? DateTime.SpecifyKind(src.ToDate.Value, DateTimeKind.Utc).ToTimestamp() : null))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc).ToTimestamp()))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.PublishDate, DateTimeKind.Utc).ToTimestamp()));
            CreateMap<ExperienceStruct, EExperience>()
                .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => src.FromDate.ToDateTime()))
                .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => src.ToDate.ToDateTime()))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToDateTime()))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToDateTime()));

            CreateMap<ExperienceFilters, ExperienceFilter>()
                .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => src.FromDate.HasValue ? DateTime.SpecifyKind(src.FromDate.Value, DateTimeKind.Utc).ToTimestamp() : null))
                .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => src.FromDate.HasValue ? DateTime.SpecifyKind(src.ToDate.Value, DateTimeKind.Utc).ToTimestamp() : null));
            CreateMap<ExperienceFilter, ExperienceFilters>()
                .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => src.FromDate.ToDateTime()))
                .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => src.ToDate.ToDateTime()));

            // Destination
            CreateMap<EDestinationLanguage, DestinationLanguageStruct>();
            CreateMap<DestinationLanguageStruct, EDestinationLanguage>();
            
            CreateMap<EDestinationLanguage, DestinationLanguageFilter>();
            CreateMap<DestinationLanguageFilter, EDestinationLanguage>();

            CreateMap<EDestinationOverview, DestinationOverviewStruct>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc).ToTimestamp()));
            CreateMap<DestinationOverviewStruct, EDestinationOverview>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToDateTime()));

            CreateMap<EDestination, DestinationStruct>();
            CreateMap<DestinationStruct, EDestination>();

            CreateMap<DestinationFilters, DestinationFilter>();
            CreateMap<DestinationFilter, DestinationFilters>();

            // Attraction
            CreateMap<EAttractionTag, AttractionTagStruct>();
            CreateMap<AttractionTagStruct, EAttractionTag>();

            CreateMap<EAttractionLanguage, AttractionLanguageStruct>();
            CreateMap<AttractionLanguageStruct, EAttractionLanguage>();

            CreateMap<EAttraction, AttractionStruct>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc).ToTimestamp()));
            CreateMap<AttractionStruct, EAttraction>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToDateTime()));

            CreateMap<AttractionFilters, AttractionFilter>();
            CreateMap<AttractionFilter, AttractionFilters>();

            CreateMap<UserProfileStruct, EUserProfile>();
            CreateMap<EUserProfile, UserProfileStruct>();
            CreateMap<UserFollowFilter, UserFollowFilters>();

            CreateMap<UserFollowStruct, EUserFollow>();
            CreateMap<EUserFollow, UserFollowStruct>();
            // att property
            CreateMap<EAttProperty, AttPropertyStruct>();
            CreateMap<AttPropertyStruct, EAttProperty>();
            CreateMap<AttPropertyFilter, AttPropertyFillter>();
            
            //Vehicle
            CreateMap<VehicleStruct, EVehicle>();
            CreateMap<EVehicle, VehicleStruct>();
            CreateMap<VehicleFilterStruct, VehicleFilter>();
            //Account
            //CreateMap<SigninRequest, SigninModel>();
            //CreateMap<SigninModel, SigninRequest>();
            CreateMap<SignupRequest, SignupRequest>();
            CreateMap<SignupRequest, SignupRequest>();


        }
    }
}
