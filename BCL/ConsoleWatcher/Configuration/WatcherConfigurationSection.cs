using System.Configuration;

namespace ConsoleWatcher.Configuration
{
    /// <summary>
    /// Represents a <see cref="WatcherConfigurationSection"/> class.
    /// </summary>
    public class WatcherConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Gets a current culture.
        /// </summary>
        [ConfigurationProperty("culture")]
        public  CultureElement CurrentCulture => (CultureElement) this["culture"];

        /// <summary>
        /// Gets a directories for watching.
        /// </summary>
        [ConfigurationCollection(typeof(WatcherDirectoryElement), AddItemName = "watcherDirectory")]
        [ConfigurationProperty("watcherDirectories")]
        public WatcherDirectoryCollection WatchingDirectories => (WatcherDirectoryCollection)this["watcherDirectories"];

        /// <summary>
        /// Gets a rules for moving files.
        /// </summary>
        [ConfigurationCollection(typeof(RuleElement), AddItemName = "rule")]
        [ConfigurationProperty("rules")]
        public RulesCollection Rules => (RulesCollection) this["rules"];

        /// <summary>
        /// Gets a default output directory.
        /// </summary>
        [ConfigurationProperty("defaultOutputDirectory")]
        public DefaultOutputDirectoryElement DefaultDirectory => (DefaultOutputDirectoryElement) this["defaultOutputDirectory"];
    }
}
