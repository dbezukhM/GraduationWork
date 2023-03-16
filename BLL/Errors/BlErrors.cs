using BLL.Results;

namespace BLL.Errors
{
    public static class BlErrors
    {
        public static Error ExistingEntity => new Error("E0001", "Such entity already exists");

        public static Error NotFound(Guid id) => new Error("E0002", "Entity is not found")
            .WithParameter(nameof(id), id);

        public static Error WrongEmailOrPassword =>
            new Error("E0003", "Email address / Password combination is not correct, please try again");

        public static Error FileNotFound => new Error("E0004", "File not found");
    }
}