using System.Configuration;

namespace ConsoleWatcher.Configuration
{
    /// <summary>
    /// Represents a <see cref="RulesCollection"/> class.
    /// </summary>
    public class RulesCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Create new element.
        /// </summary>
        /// <returns>The <see cref="ConfigurationElement"/> instance.</returns>
        protected override ConfigurationElement CreateNewElement() => new RuleElement();

        /// <summary>
        /// Get element key.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The element key.</returns>
        protected override object GetElementKey(ConfigurationElement element) => ((RuleElement) element).Name;
    }
}
