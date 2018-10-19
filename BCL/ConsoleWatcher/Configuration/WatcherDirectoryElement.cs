using System.Configuration;

namespace ConsoleWatcher.Configuration
{
    /// <summary>
    /// Represents a model <see cref="WatcherDirectoryElement"/> class.
    /// </summary>
    public class WatcherDirectoryElement : ConfigurationElement
    {
        /// <summary>
        /// Gets a watcher directory.
        /// </summary>
        [ConfigurationProperty("name", IsKey = true)]
        public string Name => (string) base["name"];

        /// <summary>
        /// Gets a directory path.
        /// </summary>
        [ConfigurationProperty("path")]
        public string Path => (string) base["path"];

        /// <summary>
        /// Gets a filter for directories files.
        /// </summary>
        [ConfigurationProperty("filter")]
        public string Filter => (string)base["filter"];
    }
}
