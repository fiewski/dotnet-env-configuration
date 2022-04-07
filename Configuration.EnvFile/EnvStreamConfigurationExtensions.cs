using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Configuration.EnvFile
{
    public static class EnvStreamConfigurationExtensions
    {
        public static IConfigurationBuilder AddEnvStreamFile(this IConfigurationBuilder builder)
        {
            return AddEnvStreamFile(builder, path: ".env");
        }
        public static IConfigurationBuilder AddEnvStreamFile(this IConfigurationBuilder builder, string path)
        {
            return builder.AddEnvFile(s =>
            {
                s.Stream = File.OpenRead(path);                
            });
        }

        private static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, Action<EnvStreamConfigurationSource> configureSource)
            => builder.Add(configureSource);
    }
}
