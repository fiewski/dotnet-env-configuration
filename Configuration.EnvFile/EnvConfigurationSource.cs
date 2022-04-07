using Microsoft.Extensions.Configuration;

namespace Configuration.EnvFile
{
    internal class EnvConfigurationSource : FileConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new EnvConfigurationProvider(this);
        }
    }
}
