using System.Linq;

namespace Esckie.Helpers
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(string line)
        {
            return line == null || line.All(char.IsWhiteSpace);
        }
    }
}
