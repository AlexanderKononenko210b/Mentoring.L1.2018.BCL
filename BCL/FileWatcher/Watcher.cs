//using System;
//using System.IO;
//using System.Text.RegularExpressions;

//namespace FileWatcher
//{
//    /// <summary>
//    /// Represents a model <see cref="Watcher"/> class.
//    /// </summary>
//    public class Watcher
//    {
//        private const string SectionName = "watcherSection";

//        private readonly FileSystemWatcher _fileSystemWatcher;
//        private readonly IConfigureManager _sectionInfo;
//        private readonly WatcherConfigurationSection _watcherSection;

//        private int _currentNumber = 0;

//        /// <summary>
//        /// Initialize a new <see cref="Watcher"/> instance.
//        /// </summary>
//        /// <param name="fileSystemWatcher">The file system watcher.</param>
//        /// <param name="sectionInfo">The configuration manager.</param>
//        public Watcher(FileSystemWatcher fileSystemWatcher, IConfigureManager sectionInfo)
//        {
//            _fileSystemWatcher = fileSystemWatcher;
//            _fileSystemWatcher.Filter = @"D:\Temp";
//            _fileSystemWatcher.Created += OnCreated;
//            _fileSystemWatcher.Changed += OnChanged;
//            _fileSystemWatcher.Deleted += OnDeleted;

//            _sectionInfo = sectionInfo;
//            _watcherSection = (WatcherConfigurationSection) _sectionInfo.GetConfigSection(SectionName);
//        }

//        #region Event handels

//        private void OnCreated(object sender, FileSystemEventArgs arg)
//        {
//            Console.WriteLine($"File: {arg.Name} was {arg.ChangeType} in {arg.FullPath} directory.");

//            this.MoveFile(arg.FullPath);
//        }

//        private void OnChanged(object sender, FileSystemEventArgs arg)
//        {
//            Console.WriteLine($"File: {arg.Name} was {arg.ChangeType} in {arg.FullPath} directory.");
//        }

//        private void OnDeleted(object sender, FileSystemEventArgs arg)
//        {
//            Console.WriteLine($"File: {arg.Name} was {arg.ChangeType} in {arg.FullPath} directory.");
//        }

//        #endregion

//        /// <summary>
//        /// Move file to the output directory.
//        /// </summary>
//        /// <param name="oldFilePath">The old file path.</param>
//        private void MoveFile(string oldFilePath)
//        {
//            if (File.Exists(oldFilePath))
//            {
//                var newFilePath = GetNewFilePath(Path.GetFileName(oldFilePath));

//                File.Move(oldFilePath, newFilePath);
//            }
//        }

//        /// <summary>
//        /// Make file name with number or/and current date.
//        /// </summary>
//        /// <param name="oldFileName">The old file name.</param>
//        /// <param name="isAddNumber">Necessary add number.</param>
//        /// <param name="isAddCurrentDate">Necessary add current date.</param>
//        /// <returns>The new file path.</returns>
//        private string MakeNewFileName(string oldFileName, bool isAddNumber, bool isAddCurrentDate)
//        {
//            var newName = oldFileName;

//            if (isAddNumber)
//            {
//                newName = Path.Combine(newName, _currentNumber.ToString());
//            }

//            if (isAddCurrentDate)
//            {
//                newName = Path.Combine(newName, DateTime.Now.ToShortTimeString());
//            }

//            return newName;
//        }

//        /// <summary>
//        /// Get output directory path.
//        /// </summary>
//        /// <param name="name">The file name.</param>
//        /// <returns>The output directory path.</returns>
//        private string GetNewFilePath(string name)
//        {
//            var newPath = string.Empty;

//            foreach (RuleElement rule in _watcherSection.Rules)
//            {
//                if (IsFiltered(name, rule.Pattern))
//                {
//                    newPath = Path.Combine(rule.OutputDirectory, MakeNewFileName(name, rule.IsAddNumber, rule.IsAddCurrentDate));

//                    return newPath;
//                }
//            }

//            newPath = Path.Combine(_watcherSection.DefaultDirectory.Path,
//                MakeNewFileName(name, _watcherSection.DefaultDirectory.IsAddNumber, _watcherSection.DefaultDirectory.IsAddCurrentDate));

//            return newPath;
//        }

//        /// <summary>
//        /// Check fileName by pattern.
//        /// </summary>
//        /// <param name="fileName">The file name.</param>
//        /// <param name="pattern">The pattern.</param>
//        /// <returns>True if the file name meets the pattern requirements otherwase false.</returns>
//        private bool IsFiltered(string fileName, string pattern) => Regex.IsMatch(fileName, pattern);
//    }
//}
