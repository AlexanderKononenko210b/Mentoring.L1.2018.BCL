using System.Configuration;

namespace ConsoleWatcher.Configuration
{
    /// <summary>
    /// Represents a model <see cref="DefaultOutputDirectoryElement"/> class.
    /// </summary>
    public class DefaultOutputDirectoryElement : ConfigurationElement
    {
        /// <summary>
        /// Gets a default output directory.
        /// </summary>
        [ConfigurationProperty("path")]
        public string Path => (string) base["path"];

        /// <summary>
        /// Gets a bool value for add number to the files name.
        /// </summary>
        [ConfigurationProperty("isAddNumber")]
        public bool IsAddNumber => (bool)this["isAddNumber"];

        /// <summary>
        /// Gets a bool value for add current date to the files name.
        /// </summary>
        [ConfigurationProperty("isAddCurrentDate")]
        public bool IsAddCurrentDate => (bool)this["isAddCurrentDate"];
    }
}
