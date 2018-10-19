using System;
using ConsoleWatcher.Enums;

namespace ConsoleWatcher.Models
{
    /// <summary>
    /// Represents a model of the <see cref="FileInfoEventArgs"/> class.
    /// </summary>
    public class FileInfoEventArgs : EventArgs
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="FileInfoEventArgs"/> class.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="newDirectory">The new directory.</param>
        /// <param name="action">The action.</param>
        public FileInfoEventArgs(string fileName, string newDirectory, FileActions action)
        {
            FileName = fileName;
            Action = action;
            NewDirectory = newDirectory;
        }

        /// <summary>
        /// Gets or sets a file path.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets new files directory.
        /// </summary>
        public string NewDirectory { get; set; }

        /// <summary>
        /// Gets or sets a files action.
        /// </summary>
        public FileActions Action { get; set; }
    }
}
