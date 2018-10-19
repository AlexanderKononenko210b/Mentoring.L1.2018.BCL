using System.Configuration;

namespace ConsoleWatcher.Configuration
{ 
    /// <summary>
    /// Represents a <see cref="WatcherDirectoryCollection"/> class.
    /// </summary>
    public class WatcherDirectoryCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Create new configuration element.
        /// </summary>
        /// <returns>The <see cref="ConfigurationElement"/></returns>
        protected override ConfigurationElement CreateNewElement() => new WatcherDirectoryElement();

        /// <summary>
        /// Get element key.
        /// </summary>
        /// <param name="element">The configuration element.</param>
        /// <returns>The element key.</returns>
        protected override object GetElementKey(ConfigurationElement element) => ((WatcherDirectoryElement) element).Name;
    }
}
