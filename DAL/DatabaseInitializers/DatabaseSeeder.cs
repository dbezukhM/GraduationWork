using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.DatabaseInitializers
{
    public class DatabaseSeeder
    {
        public static Guid FinalControlTypeExam = Guid.Parse("41A17216-A137-403E-8912-000C2DB43368");
        public static Guid FinalControlTypeCredit = Guid.Parse("6D42A2EC-C9F0-4265-9C4D-6C4FD60EC467");

        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedUniversities(modelBuilder);
            SeedFaculties(modelBuilder);
            SeedEducationalProgramsTypes(modelBuilder);
            SeedAreaOfExpertise(modelBuilder);
            SeedSpecializations(modelBuilder);
            SeedEducationalPrograms(modelBuilder);
            SeedCompetences(modelBuilder);
            SeedProgramResults(modelBuilder);
            SeedFinalControlTypes(modelBuilder);
            SeedSelectiveBlocks(modelBuilder);
            SeedSubjects(modelBuilder);
            SeedSubjectCompetences(modelBuilder);
            SeedSubjectProgramResults(modelBuilder);
        }

        private static void SeedUniversities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>().HasData(
                new University
                {
                    Id = Guid.Parse("D7878FAF-66CC-4808-B3F1-69E57D1BF014"),
                    Name = "Київський національний університет імені Тараса Шевченка",
                });
        }

        private static void SeedFaculties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faculty>().HasData(
                new Faculty
                {
                    Id = Guid.Parse("DC560CE1-CCF7-4C65-90CF-4DB0D9C538F7"),
                    Name = "Факультет комп’ютерних наук та кібернетики",
                    UniversityId = Guid.Parse("D7878FAF-66CC-4808-B3F1-69E57D1BF014"),
                });
        }

        private static void SeedAreaOfExpertise(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaOfExpertise>().HasData(
                new AreaOfExpertise
                {
                    Id = Guid.Parse("579CA45F-59E8-4C59-BE83-14D0032A84C4"),
                    Number = 12,
                    Name = "Інформаційні технології",
                },
                new AreaOfExpertise
                {
                    Id = Guid.Parse("0C9FBE25-8829-4A04-BCC7-78EFA3CF5D6F"),
                    Number = 11,
                    Name = "Математика та статистика",
                });
        }

        private static void SeedSpecializations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization
                {
                    Id = Guid.Parse("4E5DAFD6-F581-406E-A1D6-5FB82E86C4C9"),
                    Number = 121,
                    Name = "Інженерія програмного забезпечення",
                    AreaOfExpertiseId = Guid.Parse("579CA45F-59E8-4C59-BE83-14D0032A84C4"),
                },
                new Specialization
                {
                    Id = Guid.Parse("66E1809B-AB20-4122-A1C4-8643CECE445D"),
                    Number = 122,
                    Name = "Комп’ютерні науки",
                    AreaOfExpertiseId = Guid.Parse("579CA45F-59E8-4C59-BE83-14D0032A84C4"),
                },
                new Specialization
                {
                    Id = Guid.Parse("02E05698-AC3A-40DA-B648-8487F4DBB48F"),
                    Number = 124,
                    Name = "Системний аналіз",
                    AreaOfExpertiseId = Guid.Parse("579CA45F-59E8-4C59-BE83-14D0032A84C4"),
                },
                new Specialization
                {
                    Id = Guid.Parse("6FF2CE28-22C6-4248-9095-DBF4346D048C"),
                    Number = 113,
                    Name = "Прикладна математика",
                    AreaOfExpertiseId = Guid.Parse("0C9FBE25-8829-4A04-BCC7-78EFA3CF5D6F"),
                });
        }

        private static void SeedEducationalProgramsTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EducationalProgramsType>().HasData(
                new EducationalProgramsType
                {
                    Id = Guid.Parse("2C0156C4-1DC6-4977-B70B-1D6F512096D1"),
                    Name = "молодший бакалавр",
                }, new EducationalProgramsType
                {
                    Id = Guid.Parse("F0963A92-50EF-4A3E-AB48-5871AE63D10E"),
                    Name = "бакалавр",
                }, new EducationalProgramsType
                {
                    Id = Guid.Parse("088E34E7-B9F9-4389-BCDC-E9CC06F51F04"),
                    Name = "магістр",
                });
        }

        private static void SeedEducationalPrograms(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EducationalProgram>().HasData(
                new EducationalProgram
                {
                    Id = Guid.Parse("E119AF71-D0C5-436A-95F5-3EEB626F82F2"),
                    Name = "Програмна інженерія",
                    EducationalProgramsTypeId = Guid.Parse("F0963A92-50EF-4A3E-AB48-5871AE63D10E"),
                    FacultyId = Guid.Parse("DC560CE1-CCF7-4C65-90CF-4DB0D9C538F7"),
                    SpecializationId = Guid.Parse("4E5DAFD6-F581-406E-A1D6-5FB82E86C4C9"),
                },
                new EducationalProgram
                {
                    Id = Guid.Parse("D02C1C73-C6D0-433E-8D25-C03FB60C520B"),
                    Name = "Інформатика",
                    EducationalProgramsTypeId = Guid.Parse("F0963A92-50EF-4A3E-AB48-5871AE63D10E"),
                    FacultyId = Guid.Parse("DC560CE1-CCF7-4C65-90CF-4DB0D9C538F7"),
                    SpecializationId = Guid.Parse("66E1809B-AB20-4122-A1C4-8643CECE445D"),
                },
                new EducationalProgram
                {
                    Id = Guid.Parse("B64B25B8-CC04-45D4-9E9A-446A10E886D6"),
                    Name = "Системний аналіз",
                    EducationalProgramsTypeId = Guid.Parse("F0963A92-50EF-4A3E-AB48-5871AE63D10E"),
                    FacultyId = Guid.Parse("DC560CE1-CCF7-4C65-90CF-4DB0D9C538F7"),
                    SpecializationId = Guid.Parse("02E05698-AC3A-40DA-B648-8487F4DBB48F"),
                },
                new EducationalProgram
                {
                    Id = Guid.Parse("68FD7C38-7313-425B-9D47-5F1784E722C4"),
                    Name = "Прикладна математика",
                    EducationalProgramsTypeId = Guid.Parse("F0963A92-50EF-4A3E-AB48-5871AE63D10E"),
                    FacultyId = Guid.Parse("DC560CE1-CCF7-4C65-90CF-4DB0D9C538F7"),
                    SpecializationId = Guid.Parse("6FF2CE28-22C6-4248-9095-DBF4346D048C"),
                });
        }

        private static void SeedCompetences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Competence>().HasData(
                new Competence
                {
                    Id = Guid.Parse("89E42FA0-9453-42F7-8E74-C2A2C8DC2908"),
                    Name = "ЗК2",
                    Description = "Здатність застосовувати знання у практичних ситуаціях",
                    EducationalProgramId = Guid.Parse("E119AF71-D0C5-436A-95F5-3EEB626F82F2"),
                }, new Competence
                {
                    Id = Guid.Parse("CA7185F4-CB3B-47BE-A54D-83F0BAFBA01E"),
                    Name = "ЗК12",
                    Description = "Здатність оцінювати та забезпечувати якість виконуваних робіт",
                    EducationalProgramId = Guid.Parse("E119AF71-D0C5-436A-95F5-3EEB626F82F2"),
                }, new Competence
                {
                    Id = Guid.Parse("0CDFFE9C-E6F9-47FA-9989-EF92287B8227"),
                    Name = "СК3",
                    Description =
                        "Здатність до логічного мислення, побудови логічних висновків, використання формальних мов і моделей алгоритмічних обчислень, проектування, розроблення й аналізу алгоритмів, оцінювання їх ефективності та складності, розв’язності та нерозв’язності алгоритмічних проблем для адекватного моделювання предметних областей і створення програмних та інформаційних систем",
                    EducationalProgramId = Guid.Parse("E119AF71-D0C5-436A-95F5-3EEB626F82F2"),
                }, new Competence
                {
                    Id = Guid.Parse("D47B1851-6D15-478B-B048-953B039E1575"),
                    Name = "СК8",
                    Description =
                        "Здатність проектувати та розробляти програмне забезпечення із застосуванням різних парадигм програмування: узагальненого, об’єктно-орієнтованого, функціонального, логічного, з відповідними моделями, методами й алгоритмами обчислень, структурами даних і механізмами управління",
                    EducationalProgramId = Guid.Parse("E119AF71-D0C5-436A-95F5-3EEB626F82F2"),
                });
        }

        private static void SeedProgramResults(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgramResult>().HasData(
                new ProgramResult
                {
                    Id = Guid.Parse("C796B93C-20EA-4D8C-B573-6BF0C34C9862"),
                    Name = "ПРН9",
                    Description =
                        "Розробляти програмні моделі предметних середовищ, вибирати парадигму програмування з позицій зручності та якості застосування для реалізації методів та алгоритмів розв’язання задач в галузі комп’ютерних наук.",
                    EducationalProgramId = Guid.Parse("E119AF71-D0C5-436A-95F5-3EEB626F82F2"),
                }, new ProgramResult
                {
                    Id = Guid.Parse("61EC2730-9EB1-46B6-B43C-BDC0011594CB"),
                    Name = "ПРН15",
                    Description =
                        "Застосовувати знання методології та CASE-засобів проектування складних систем, методів структурного аналізу систем, об'єктно-орієнтованої методології проектування при розробці і дослідженні функціональних моделей організаційно-економічних і виробничо-технічних систем.",
                    EducationalProgramId = Guid.Parse("E119AF71-D0C5-436A-95F5-3EEB626F82F2"),
                });
        }

        private static void SeedFinalControlTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinalControlType>().HasData(
                new FinalControlType
                {
                    Id = FinalControlTypeExam,
                    Name = "Іспит",
                }, new FinalControlType
                {
                    Id = FinalControlTypeCredit,
                    Name = "Залік",
                });
        }

        private static void SeedSelectiveBlocks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SelectiveBlock>().HasData(
                new SelectiveBlock
                {
                    Id = Guid.Parse("986241D5-B78A-4D9B-A96F-1DA84D9C48AA"),
                    Name = "Обов'язкова",
                }, new SelectiveBlock
                {
                    Id = Guid.Parse("CFA51FE0-BF95-42FC-97D4-434F848DF0BE"),
                    Name = "Вибіркова",
                });
        }

        private static void SeedSubjects(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().HasData(
                new Subject
                {
                    Id = Guid.Parse("55DB3BCA-4BB7-4D39-951E-7DAECF070A4A"),
                    Name = "Об’єктно-орієнтоване програмування",
                    Credits = 4,
                    Semester = 3,
                    LecturesHours = 28,
                    SeminarsHours = 28,
                    PracticalClassesHours = 2,
                    LaboratoryClassesHours = 1,
                    TrainingsHours = 0,
                    ConsultationsHours = 0,
                    SelfWorkHours = 62,
                    SelectiveBlockId = Guid.Parse("986241D5-B78A-4D9B-A96F-1DA84D9C48AA"),
                    FinalControlTypeId = FinalControlTypeExam,
                    EducationalProgramId = Guid.Parse("E119AF71-D0C5-436A-95F5-3EEB626F82F2"),
                });
        }

        private static void SeedSubjectCompetences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubjectCompetence>().HasData(
                new SubjectCompetence
                {
                    Id = Guid.Parse("203E0BAC-FB45-4B97-A647-29D793F1D456"),
                    SubjectId = Guid.Parse("55DB3BCA-4BB7-4D39-951E-7DAECF070A4A"),
                    CompetenceId = Guid.Parse("89E42FA0-9453-42F7-8E74-C2A2C8DC2908"),
                }, new SubjectCompetence
                {
                    Id = Guid.Parse("AF99DEBE-93A6-4FC6-B7A9-0F642436B8CD"),
                    SubjectId = Guid.Parse("55DB3BCA-4BB7-4D39-951E-7DAECF070A4A"),
                    CompetenceId = Guid.Parse("CA7185F4-CB3B-47BE-A54D-83F0BAFBA01E"),
                }, new SubjectCompetence
                {
                    Id = Guid.Parse("859E5601-EF8F-48AC-94E0-312F77049AB0"),
                    SubjectId = Guid.Parse("55DB3BCA-4BB7-4D39-951E-7DAECF070A4A"),
                    CompetenceId = Guid.Parse("0CDFFE9C-E6F9-47FA-9989-EF92287B8227"),
                }, new SubjectCompetence
                {
                    Id = Guid.Parse("A0D961E4-3BD4-4A9C-8176-A4FDFFC076C1"),
                    SubjectId = Guid.Parse("55DB3BCA-4BB7-4D39-951E-7DAECF070A4A"),
                    CompetenceId = Guid.Parse("D47B1851-6D15-478B-B048-953B039E1575"),
                });
        }

        private static void SeedSubjectProgramResults(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubjectProgramResult>().HasData(
                new SubjectProgramResult
                {
                    Id = Guid.Parse("DC38164F-E5D6-4A62-B24D-21668D05B663"),
                    SubjectId = Guid.Parse("55DB3BCA-4BB7-4D39-951E-7DAECF070A4A"),
                    ProgramResultId = Guid.Parse("C796B93C-20EA-4D8C-B573-6BF0C34C9862"),
                }, new SubjectProgramResult
                {
                    Id = Guid.Parse("07CBCE22-4F54-4C84-8446-F2ECBDBADE00"),
                    SubjectId = Guid.Parse("55DB3BCA-4BB7-4D39-951E-7DAECF070A4A"),
                    ProgramResultId = Guid.Parse("61EC2730-9EB1-46B6-B43C-BDC0011594CB"),
                });
        }
    }
}