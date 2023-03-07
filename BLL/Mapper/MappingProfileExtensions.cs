using AutoMapper;

namespace BLL.Mapper
{
    public static class MappingProfileExtensions
    {
        public static void CreateMap<TSource, TDestination>(this Profile profile, string sourceSuffix,
            string destinationSuffix)
        {
            var sourceModels = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(TSource)));

            var destinationModels = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(TDestination)))
                .ToLookup(t => t.Name, t => t);

            foreach (var sourceModel in sourceModels)
            {
                if (!sourceModel.Name.Contains(sourceSuffix))
                {
                    continue;
                }

                var destinationModelName =
                    $"{sourceModel.Name.Substring(0, sourceModel.Name.Length - sourceSuffix.Length)}{destinationSuffix}";
                var severalDestinationModels = destinationModels[destinationModelName];

                foreach (var destinationModel in severalDestinationModels)
                {
                    profile.CreateMap(sourceModel, destinationModel).ReverseMap();
                }
            }
        }
    }
}