using BLL.Results;

namespace BLL.Errors
{
    public static class BlErrors
    {
        public static Error ExistingEntity => new Error("E0001", "Such entity already exists");

        public static Error NotFound(Guid id) => new Error("E0002", "Entity is not found")
            .WithParameter(nameof(id), id);

        public static Error EntityNotFound => new Error("E0002", "Entity is not found");

        public static Error WrongEmailOrPassword =>
            new Error("E0003", "Email address / Password combination is not correct, please try again");

        public static Error FileNotFound => new Error("E0004", "File not found");

        public static Error EducationalProgramNameNotUnique => new Error("E0005", "Educational program name is not unique");

        public static Error ProgramResultNameNotUnique => new Error("E0006", "Program result name is not unique");

        public static Error CompetenceNameNotUnique => new Error("E0007", "Competence name is not unique");

        public static Error SubjectNameNotUnique => new Error("E0008", "Subject name is not unique");

        public static Error PasswordNotCorrect => new Error("E0009", "Old password is not correct");

        public static Error PasswordIsSimple => new Error("E0010", "The password is simple");
    }
}