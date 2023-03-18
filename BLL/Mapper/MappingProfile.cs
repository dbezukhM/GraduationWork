using AutoMapper;
using BLL.Extensions;
using BLL.Models;
using DAL.Entities;

namespace BLL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EducationalProgram, EducationalProgramModel>()
                .ForMember(x => x.UniversityName,
                    cfg => cfg.MapFrom(scr => scr.Faculty.University.Name.FirstCharToUpper()))
                .ForMember(x => x.SpecializationName,
                    cfg => cfg.MapFrom(scr => scr.Specialization.Name.FirstCharToUpper()))
                .ForMember(x => x.EducationalProgramsTypeName,
                    cfg => cfg.MapFrom(scr => scr.EducationalProgramsType.Name.FirstCharToUpper()));

            CreateMap<University, IdNameModel<Guid>>();
            CreateMap<Faculty, IdNameModel<Guid>>();
            CreateMap<EducationalProgramsType, IdNameModel<Guid>>();
            CreateMap<Subject, IdNameModel<Guid>>();
            CreateMap<AreaOfExpertise, IdNameModel<Guid>>()
                .ForMember(x => x.Name, cfg => cfg.MapFrom(scr => $"{scr.Number} «{scr.Name.FirstCharToUpper()}»"));
            CreateMap<Specialization, IdNameModel<Guid>>()
                .ForMember(x => x.Name, cfg => cfg.MapFrom(scr => $"{scr.Number} «{scr.Name.FirstCharToUpper()}»"));
            CreateMap<EducationalProgram, EducationalProgramGetModel>()
                .ForMember(x => x.AreaOfExpertise, cfg => cfg.MapFrom(src => src.Specialization.AreaOfExpertise))
                .ForMember(x => x.University, cfg => cfg.MapFrom(src => src.Faculty.University));

            CreateMap<EducationalProgramCreateModel, EducationalProgram>();
        }
    }
}