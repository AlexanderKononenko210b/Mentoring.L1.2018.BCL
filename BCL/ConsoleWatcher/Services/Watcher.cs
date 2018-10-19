using System;
using System.IO;
using System.Text.RegularExpressions;
using ConsoleWatcher.Configuration;
using ConsoleWatcher.Enums;
using ConsoleWatcher.Interfaces;
using ConsoleWatcher.Models;
using messages = ConsoleWatcher.Resources.Messages;

namespace ConsoleWatcher.Services
{
    /// <summary>
    /// Represents a model <see cref="Watcher"/> class.
    /// </summary>
    public sealed class Watcher
    {
        private readonly IWatchersBuilder _watchersBuilder;
        private readonly WatcherConfigurationSection _section;

        private int _currentNumber;

        /// <summary>
        /// Initialize a new <see cref="Watcher"/> instance.
        /// </summary>
        /// <param name="watchersBuilder">The watchers builder.</param>
        /// <param name="section">The section info.</param>
        public Watcher(IWatchersBuilder watchersBuilder, WatcherConfigurationSection section)
        {
            _watchersBuilder = watchersBuilder;
            _section = section;
        }

        /// <summary>
        /// Start watch.
        /// </summary>
        public void StartWatch()
        {
            _watchersBuilder.Build();

            foreach (var fileWatcher in _watchersBuilder)
            {
                fileWatcher.Created += OnCreated;
                fileWatcher.Deleted += OnDeleted;
                fileWatcher.EnableRaisingEvents = true;
            }
        }

        #region Events

        /// <summary>
        /// The file filtered event.
        /// </summary>
        public event EventHandler<FileInfoEventArgs> FilteredItem;

        /// <summary>
        /// Invoke filtered event.
        /// </summary>
        private void FilteredFileRaised(FileInfoEventArgs args)
        {
            this.FilteredItem?.Invoke(this, args);
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Handler for file created event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="arg">The <see cref="FileInfoEventArgs"/> arguments.</param>
        private void OnCreated(object sender, FileSystemEventArgs arg)
        {
            Console.WriteLine($"{messages.File}: {arg.Name} {messages.was} {messages.created} {messages._in} {arg.FullPath} {messages.directory}.");

            this.MoveFile(arg.FullPath);
        }

        /// <summary>
        /// Handler for file delete event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="arg">The <see cref="FileInfoEventArgs"/> arguments.</param>
        private void OnDeleted(object sender, FileSystemEventArgs arg)
        {
            Console.WriteLine($"{messages.File}: {arg.Name} {messages.was} {messages.moved} {messages.from} {arg.FullPath} {messages.directory}.");
        }

        #endregion

        /// <summary>
        /// Move file to the output directory.
        /// </summary>
        /// <param name="oldFilePath">The old file path.</param>
        private void MoveFile(string oldFilePath)
        {
            if (File.Exists(oldFilePath))
            {
                while (!this.IsLocked(oldFilePath))
                {
                    var newFilePath = GetNewFilePath(Path.GetFileName(oldFilePath));

                    if (File.Exists(newFilePath))
                    {
                        Console.WriteLine(messages.ExistFileInfo);
                    }
                    else
                    {
                        File.Move(oldFilePath, newFilePath);
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// Make file name with number or/and current date.
        /// </summary>
        /// <param name="oldFileName">The old file name.</param>
        /// <param name="isAddNumber">Necessary add number.</param>
        /// <param name="isAddCurrentDate">Necessary add current date.</param>
        /// <returns>The new file name.</returns>
        private string MakeNewFileName(string oldFileName, bool isAddNumber, bool isAddCurrentDate)
        {
            var newName = this.GetFileName(oldFileName);
            var extension = this.GetExtension(oldFileName);

            if (isAddNumber)
            {
                newName = $"{newName}_{_currentNumber}";
                _currentNumber++;
            }

            if (isAddCurrentDate)
            {
                newName = $"{newName}_{DateTime.Now.ToShortDateString()}";
            }

            return $"{newName}.{extension}";
        }

        /// <summary>
        /// Get file name.
        /// </summary>
        /// <param name="fileName">The files name.</param>
        /// <returns>The files name without extension.</returns>
        private string GetFileName(string fileName) => fileName.Substring(0, fileName.LastIndexOf('.'));

        /// <summary>
        /// Get files extension.
        /// </summary>
        /// <param name="fileName">The files name.</param>
        /// <returns>The files extension.</returns>
        private string GetExtension(string fileName) => fileName.Substring(fileName.LastIndexOf('.') + 1);

        /// <summary>
        /// Get output directory path.
        /// </summary>
        /// <param name="name">The file name.</param>
        /// <returns>The output directory path.</returns>
        private string GetNewFilePath(string name)
        {
            var newPath = string.Empty;

            foreach (RuleElement rule in _section.Rules)
            {
                if (IsFiltered(name, rule.Pattern))
                {
                    this.FilteredFileRaised(new FileInfoEventArgs(name, rule.OutputDirectory, FileActions.Filtered));

                    newPath = Path.Combine(rule.OutputDirectory, MakeNewFileName(name, rule.IsAddNumber, rule.IsAddCurrentDate));

                    return newPath;
                }
            }

            newPath = Path.Combine(_section.DefaultDirectory.Path,
                MakeNewFileName(name, _section.DefaultDirectory.IsAddNumber, _section.DefaultDirectory.IsAddCurrentDate));

            return newPath;
        }

        /// <summary>
        /// Check fileName by pattern.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>True if the file name meets the pattern requirements otherwase false.</returns>
        private bool IsFiltered(string fileName, string pattern) => Regex.IsMatch(fileName, pattern);

        /// <summary>
        /// Check file state.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>If file doesn`t busy any processes than return true otherwase false.</returns>
        public bool IsLocked(string fileName)
        {
            try
            {
                using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return true;
            }
        }
    }
}
