using common_mapping.Settings;
using Microsoft.Extensions.Options;

namespace common_mapping
{
    public class Options : IOptions<Setup>
    {
        public Options(Setup value)
        {
            Value = value;
        }

        public Setup Value { get; }
    }
}
