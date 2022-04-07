namespace WebApi.EnvFile.Models
{
    public class RabbitConfig
    {
        public const string Config = "RabbitConfig";

        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}
