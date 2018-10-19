using System.Collections.Generic;
using System.IO;

namespace ConsoleWatcher.Interfaces
{
    /// <summary>
    /// Represents an <see cref="IWatchersBuilder"/> class.
    /// </summary>
    public interface IWatchersBuilder : IEnumerable<FileSystemWatcher>
    {
        /// <summary>
        /// Build <see cref="FileSystemWatcher"/> instances.
        /// </summary>
        void Build();
    }
}
