using TrackingCode.Application.Contracts;
using TrackingCode.Domain.Models;

namespace TrackingCode.Api
{
    public class TrackingCodeModelProfile : AutoMapper.Profile
    {
        public TrackingCodeModelProfile()
        {
            CreateMap<TrackingCodeModelCreateDto, TrackingCodeModel>();
            CreateMap<TrackingCodeModel, TrackingCodeModelDto>();
        }
    }
}