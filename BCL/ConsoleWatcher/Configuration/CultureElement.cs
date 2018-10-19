using System.Configuration;

namespace ConsoleWatcher.Configuration
{
    /// <summary>
    /// Represents a model <see cref="CultureElement"/> class.
    /// </summary>
    public class CultureElement : ConfigurationElement
    {
        /// <summary>
        /// Gets a current culture.
        /// </summary>
        [ConfigurationProperty("name")]
        public string Type => (string) base["name"];
    }
}
