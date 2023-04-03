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
                    cfg => cfg.MapFrom(src => src.Faculty.University.Name))
                .ForMember(x => x.SpecializationName,
                    cfg => cfg.MapFrom(src => src.Specialization.Number + " " + src.Specialization.Name.FirstCharToUpper()))
                .ForMember(x => x.EducationalProgramsTypeName,
                    cfg => cfg.MapFrom(src => src.EducationalProgramsType.Name.FirstCharToUpper()));

            CreateMap<University, IdNameModel<Guid>>();
            CreateMap<Faculty, IdNameModel<Guid>>();
            CreateMap<EducationalProgramsType, IdNameModel<Guid>>();
            CreateMap<Subject, IdNameModel<Guid>>();
            CreateMap<EducationalProgram, IdNameModel<Guid>>()
                .ForMember(x => x.Name, cfg => cfg.MapFrom(src => $"{src.Name} ({src.EducationalProgramsType.Name})"));
            CreateMap<SelectiveBlock, IdNameModel<Guid>>();
            CreateMap<FinalControlType, IdNameModel<Guid>>();
            CreateMap<AreaOfExpertise, IdNameModel<Guid>>()
                .ForMember(x => x.Name, cfg => cfg.MapFrom(src => $"{src.Number} «{src.Name.FirstCharToUpper()}»"));
            CreateMap<Specialization, IdNameModel<Guid>>()
                .ForMember(x => x.Name, cfg => cfg.MapFrom(src => $"{src.Number} «{src.Name.FirstCharToUpper()}»"));
            CreateMap<EducationalProgram, EducationalProgramGetModel>()
                .ForMember(x => x.AreaOfExpertise, cfg => cfg.MapFrom(src => src.Specialization.AreaOfExpertise))
                .ForMember(x => x.University, cfg => cfg.MapFrom(src => src.Faculty.University));

            CreateMap<EducationalProgramCreateModel, EducationalProgram>();
            CreateMap<ProgramResultCreateModel, ProgramResult>();
            CreateMap<ProgramResult, ProgramResultGetModel>()
                .ForMember(x => x.Subjects, cfg => cfg.Ignore());
            CreateMap<Competence, CompetenceGetModel>()
                .ForMember(x => x.Subjects, cfg => cfg.Ignore());
            CreateMap<CompetenceCreateModel, Competence>();

            CreateMap<Subject, SubjectModel>()
                .ForMember(x => x.EducationalProgramName,
                    cfg => cfg.MapFrom(src => $"{src.EducationalProgram.Name} ({src.EducationalProgram.EducationalProgramsType.Name})"))
                .ForMember(x => x.SelectiveBlockName,
                    cfg => cfg.MapFrom(src => src.SelectiveBlock.Name));
            CreateMap<Subject, SubjectGetModel>()
                .ForMember(x => x.Competences, cfg => cfg.Ignore())
                .ForMember(x => x.ProgramResults, cfg => cfg.Ignore());
            CreateMap<SubjectCreateModel, Subject>();
            CreateMap<Person, PersonGetModel>();
            CreateMap<PersonCreateModel, Person>();
            CreateMap<Person, IdNameModel<Guid>>();
            CreateMap<WorkingProgramCreateModel, WorkingProgram>();
            CreateMap<WorkingProgram, WorkingProgramGetModel>();
            CreateMap<WorkingProgram, WorkingProgramDetailsModel>();
            CreateMap<WorkingProgram, IdNameModel<Guid>>();
            CreateMap<Comment, CommentModel>()
                .ForMember(x => x.CreatedByName, cfg => cfg.MapFrom(src => $"{src.CreatedBy.FirstName} {src.CreatedBy.LastName}"));
        }
    }
}