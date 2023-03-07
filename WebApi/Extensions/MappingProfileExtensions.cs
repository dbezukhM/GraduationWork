using AutoMapper;
using BLL.Mapper;
using BLL.Models;
using BLL.Results;
using DAL.Entities;
using WebApi.Models;
using WebApi.Models.RequestResponse;

namespace WebApi.Extensions
{
    public class MappingProfileExtensions : Profile
    {
        public MappingProfileExtensions()
        {
            this.CreateMap<IDbModel, IDomainModel>("", "Model");
            this.CreateMap<IDomainModel, IResponse>("Model", "Response");
            this.CreateMap<IRequest, IDomainModel>("Request", "Model");

            CreateMap<Result, ErrorResponse>();
            CreateMap<Result, SuccessResponse>();
            CreateMap(typeof(Result<>), typeof(SuccessResponse<>))
                .ForMember("Result", opt => opt.MapFrom("Value"));
            CreateMap<Error, ErrorResponse.ErrorModel>();
        }
    }
}