using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;


namespace Configuration.EnvFile
{
    public static class EnvConfigurationExtensions
    {
        private const string Error_InvalidFilePath = "Path cannot be empty";

        public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder)
        {            
            return AddEnvFile(builder, path: ".env");
        }

        public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, string path)
        {
            return AddEnvFile(builder, path: path, optional: true);
        }

        public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, string path, bool optional)
        {
            return AddEnvFile(builder, provider: new EnvFileProvider(), path: path, optional: optional, reloadOnChange: false);
        }

        public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            return AddEnvFile(builder, provider: null, path: path, optional: optional, reloadOnChange: reloadOnChange);
        }

        public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, IFileProvider? provider, string path, bool optional, bool reloadOnChange)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException(Error_InvalidFilePath, nameof(path));
            }

            return builder.AddEnvFile(s =>
            {
                s.FileProvider = provider;
                s.Path = path;
                s.Optional = optional;
                s.ReloadOnChange = reloadOnChange;
                s.ResolveFileProvider();
            });
        }

        private static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, Action<EnvConfigurationSource> configureSource)
            => builder.Add(configureSource);
    }
}
