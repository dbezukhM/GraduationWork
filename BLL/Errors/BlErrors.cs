using BLL.Results;

namespace BLL.Errors
{
    public static class BlErrors
    {
        public static Error ExistingEntity => new Error("E0001", "Such entity already exists");

        public static Error NotFound(Guid id) => new Error("E0002", "Entity is not found")
            .WithParameter(nameof(id), id);
    }
}