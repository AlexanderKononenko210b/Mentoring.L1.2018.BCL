using System.Configuration;

namespace ConsoleWatcher.Configuration
{
    /// <summary>
    /// Represents a <see cref="RuleElement"/> class.
    /// </summary>
    public class RuleElement : ConfigurationElement
    {
        /// <summary>
        /// Gets a name of rule.
        /// </summary>
        [ConfigurationProperty("name")]
        public string Name => (string) this["name"];

        /// <summary>
        /// Gets a file name pattern.
        /// </summary>
        [ConfigurationProperty("pattern")]
        public string Pattern => (string) this["pattern"];

        /// <summary>
        /// Gets a path to output directory.
        /// </summary>
        [ConfigurationProperty("outputDirectory")]
        public string OutputDirectory => (string) this["outputDirectory"];

        /// <summary>
        /// Gets a bool value for add number to the files name.
        /// </summary>
        [ConfigurationProperty("isAddNumber")]
        public bool IsAddNumber => (bool) this["isAddNumber"];

        /// <summary>
        /// Gets a bool value for add current date to the files name.
        /// </summary>
        [ConfigurationProperty("isAddCurrentDate")]
        public bool IsAddCurrentDate => (bool) this["isAddCurrentDate"];
    }
}
