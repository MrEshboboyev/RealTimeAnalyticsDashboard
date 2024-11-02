using AutoMapper;
using RealTimeAnalyticsDashboard.Application.DTOs;
using RealTimeAnalyticsDashboard.Domain.Entities;

namespace RealTimeAnalyticsDashboard.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Metric
        CreateMap<Metric, MetricDTO>()
            .ReverseMap();
        #endregion

        #region Session
        CreateMap<Session, SessionDTO>()
            .ForMember(dest => dest.UserDTO, opt => opt.MapFrom(src => src.AppUser))
            .ForMember(dest => dest.UserActivityDTOs, opt => opt.MapFrom(src => src.UserActivities))
            .ReverseMap()
                .ForMember(dest => dest.AppUser, opt => opt.Ignore())
                .ForMember(dest => dest.UserActivities, opt => opt.Ignore());
        #endregion

        #region UserActivity
        CreateMap<UserActivity, UserActivityDTO>()
            .ForMember(dest => dest.SessionDTO, opt => opt.MapFrom(src => src.Session))
            .ReverseMap()
                .ForMember(dest => dest.Session, opt => opt.Ignore());
        #endregion

        #region AppUser

        // AppUser -> UserDTO
        CreateMap<AppUser, UserDTO>()
            .ReverseMap();
        #endregion
    }
}
