using Microsoft.Extensions.Configuration;

namespace Configuration.EnvFile
{
    public class EnvConfigurationProvider : FileConfigurationProvider
    {
        public EnvConfigurationProvider(FileConfigurationSource source) : base(source) { }
        public override void Load(Stream stream) => Data = EnvStreamConfigurationProvider.Read(stream);

    }
}