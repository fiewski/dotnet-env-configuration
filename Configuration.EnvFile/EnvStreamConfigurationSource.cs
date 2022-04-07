using Microsoft.Extensions.Configuration;

namespace Configuration.EnvFile
{
    public class EnvStreamConfigurationSource : StreamConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EnvStreamConfigurationProvider(this);
        }
    }
}
