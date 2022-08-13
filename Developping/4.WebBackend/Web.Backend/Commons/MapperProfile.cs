using AutoMapper;
using DynamicData.Protos;
using DynamicData.Entities;
using Web.Backend.Models.DynamicForm;
using Mic.Core.Entities;
using System;
using Google.Protobuf.WellKnownTypes;
using UserDb.Protos;
using DbData.Entities;
using DbData.Entities.Models;
using DbData.Protos;
using Mic.UserDb.Entities;
using Web.Backend.Models;
using Web.Backend.Models.User;

namespace Web.Backend.Commons
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<DynamicData.Protos.ResponseMessage, AlertMessage>();
            CreateMap<KTPagination, DynamicData.Protos.PagingType>()
                .ForMember(m => m.Start, opt => opt.MapFrom(src => (src.Page - 1) * src.Perpage))
                .ForMember(m => m.Length, opt => opt.MapFrom(src => src.Perpage));
            CreateMap<KTSort, DynamicData.Protos.SortType>();

            CreateMap<DbData.Protos.ResponseMessage, AlertMessage>();
            CreateMap<KTPagination, DbData.Protos.PagingType>()
                .ForMember(m => m.Start, opt => opt.MapFrom(src => (src.Page - 1) * src.Perpage))
                .ForMember(m => m.Length, opt => opt.MapFrom(src => src.Perpage));
            CreateMap<KTSort, DbData.Protos.SortType>();


            // ESysTable
            CreateMap<ESysTable, SysTableStruct>()
                .ForMember(dest => dest.LastSyncDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastSyncDate, DateTimeKind.Utc).ToTimestamp()));
            CreateMap<SysTableStruct, ESysTable>()
                .ForMember(dest => dest.LastSyncDate, opt => opt.MapFrom(src => src.LastSyncDate.ToDateTime()));

            CreateMap<ESysColumn, SysColumnStruct>();
            CreateMap<SysColumnStruct, ESysColumn>();

            CreateMap<ESysCustomType, SysCustomTypeStruct>();
            CreateMap<SysCustomTypeStruct, ESysCustomType>();

            CreateMap<TableResponse, DFormListViewModel>()
                .ForMember(m => m.AlertMessages, opt => opt.Ignore())
                .ForMember(m => m.ActionMode, opt => opt.Ignore())
                .ForMember(m => m.ReturnUrl, opt => opt.Ignore());
            CreateMap<TableResponse, DFormViewModel>()
                .ForMember(m => m.AlertMessages, opt => opt.Ignore())
                .ForMember(m => m.ActionMode, opt => opt.Ignore())
                .ForMember(m => m.ReturnUrl, opt => opt.Ignore());

            CreateMap<SysTableListViewModel, ListTableFilter>();

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
            CreateMap<LanguageDataListViewModel, LanguageDataFilter>();

            CreateMap<ContinentStruct, EContinent>();
            CreateMap<EAttractionTag, AttractionTagStruct>();
            CreateMap<AttractionTagStruct, EAttractionTag>();
            CreateMap<CountryStruct, ECountry>();
            CreateMap<ECountry, CountryStruct>();
            CreateMap<CountryListViewModel, CountryFilter>();

            CreateMap<StateStruct, EState>();
            CreateMap<EState, StateStruct>();
            CreateMap<StateListViewModel, StateFilter>();

            CreateMap<CityStruct, ECity>();
            CreateMap<ECity, CityStruct>();
            CreateMap<CityListViewModel, CityFilter>();

            // Experience
            CreateMap<EExperienceLanguage, ExperienceLanguageStruct>();
            CreateMap<ExperienceLanguageStruct, EExperienceLanguage>();

            CreateMap<EExperienceSession, ExperienceSessionStruct>();
            CreateMap<ExperienceSessionStruct, EExperienceSession>();

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

            CreateMap<ExperienceListViewModel, ExperienceFilter>()
                .ForMember(dest => dest.Paging, opt => opt.MapFrom(src => src.Pagination))
                .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => src.FromDate.HasValue ? DateTime.SpecifyKind(src.FromDate.Value, DateTimeKind.Utc).ToTimestamp() : null))
                .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => src.FromDate.HasValue ? DateTime.SpecifyKind(src.ToDate.Value, DateTimeKind.Utc).ToTimestamp() : null));
            

            // Destination
            CreateMap<EDestinationLanguage, DestinationLanguageStruct>();
            CreateMap<DestinationLanguageStruct, EDestinationLanguage>();

            CreateMap<EDestinationOverview, DestinationOverviewStruct>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc).ToTimestamp()));
            CreateMap<DestinationOverviewStruct, EDestinationOverview>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToDateTime()));

            CreateMap<EDestination, DestinationStruct>();
            CreateMap<DestinationStruct, EDestination>();

            CreateMap<DestinationListViewModel, DestinationFilter>();
            CreateMap<DestinationFilter, DestinationListViewModel>();

            // Attraction
            CreateMap<EAttractionLanguage, AttractionLanguageStruct>();
            CreateMap<AttractionLanguageStruct, EAttractionLanguage>();

            CreateMap<EAttraction, AttractionStruct>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc).ToTimestamp()));
            CreateMap<AttractionStruct, EAttraction>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToDateTime()));

            CreateMap<AttractionFilters, AttractionFilter>();
            CreateMap<AttractionFilter, AttractionFilters>();

              CreateMap<AttractionListViewModel, AttractionFilter>();
            CreateMap<AttractionFilter, AttractionListViewModel>();

            // PropertyStruct
            CreateMap<AttPropertyStruct,EAttProperty> ();
            CreateMap<EAttProperty, AttPropertyStruct> ();
            CreateMap<AttPropertyListViewModel, AttPropertyFilter>();

            //vehicle
            CreateMap<VehicleStruct, EVehicle>();
            CreateMap<EVehicle, VehicleStruct>();
            CreateMap<VehicleListViewModel, VehicleFilterStruct>();
        }
    }
}
