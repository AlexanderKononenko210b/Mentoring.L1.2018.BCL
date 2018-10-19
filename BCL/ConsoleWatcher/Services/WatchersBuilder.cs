using System.Collections;
using System.Collections.Generic;
using System.IO;
using ConsoleWatcher.Configuration;
using ConsoleWatcher.Interfaces;

namespace ConsoleWatcher.Services
{
    /// <summary>
    /// Represents a model <see cref="WatchersBuilder"/> class.
    /// </summary>
    public sealed class WatchersBuilder : IWatchersBuilder
    {
        private readonly List<FileSystemWatcher> _watchers = new List<FileSystemWatcher>();
        private readonly WatcherConfigurationSection _section;

        /// <summary>
        /// Initialize a new instance <see cref="WatchersBuilder"/> class.
        /// </summary>
        /// <param name="section">The section info.</param>
        public WatchersBuilder(WatcherConfigurationSection section)
        {
            _section = section;
        }

        /// <inheritdoc/>
        public void Build()
        {
            foreach (WatcherDirectoryElement info in _section.WatchingDirectories)
            {
                var fileSystemWatcher = new FileSystemWatcher();
                fileSystemWatcher.Path = info.Path;
                fileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
                fileSystemWatcher.Filter = info.Filter;

                _watchers.Add(fileSystemWatcher);
            }
        }

        /// <summary>
        /// Return enumerator for enumerate throw <see cref="FileSystemWatcher"/> elements.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<FileSystemWatcher> GetEnumerator()
        {
            return _watchers.GetEnumerator();
        }

        /// <summary>
        /// Get enumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
