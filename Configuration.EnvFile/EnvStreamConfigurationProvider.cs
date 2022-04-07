using Microsoft.Extensions.Configuration;

namespace Configuration.EnvFile
{
    public class EnvStreamConfigurationProvider : StreamConfigurationProvider
    {
        private const string COMMENT_VALUE = "=";
        private const string SPLIT_VALUE = "=";
        private const string REPLACE_NAVIGATION_FROM = "__";
        private const string REPLACE_NAVIGATION_TO = ":";
        private const string EXISTING_KEY = "There is already a registered key";
        public EnvStreamConfigurationProvider(StreamConfigurationSource source) : base(source) { }

        public override void Load(Stream stream) => Data = Read(stream);

        public static IDictionary<string, string?> Read(Stream stream)
        {
            var result = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

            using (var reader = new StreamReader(stream))
            {
                string sectionPrefix = string.Empty;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine()!.Trim();

                    if (string.IsNullOrWhiteSpace(line) ||
                        line.StartsWith(COMMENT_VALUE, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    var lineValues = line.Split(SPLIT_VALUE, StringSplitOptions.RemoveEmptyEntries);

                    var key = lineValues[0].Replace(REPLACE_NAVIGATION_FROM, REPLACE_NAVIGATION_TO);
                    string value = lineValues[1];

                    if (result.ContainsKey(key))
                    {
                        throw new FormatException(EXISTING_KEY);
                    }

                    result[key] = value;
                }
            }

            return result;
        }
    }
}
