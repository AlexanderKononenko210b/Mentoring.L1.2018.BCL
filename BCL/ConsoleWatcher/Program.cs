using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Threading;
using ConsoleWatcher.Configuration;
using ConsoleWatcher.Models;
using ConsoleWatcher.Services;
using messages = ConsoleWatcher.Resources.Messages;

namespace ConsoleWatcher
{
    public static class Program
    {
        private static string SectionName = "watcherSection";

        static void Main(string[] args)
        {
            var section = (WatcherConfigurationSection)ConfigurationManager.GetSection(Program.SectionName);

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(section.CurrentCulture.Type);

            CreateWorkDirectory(section);

            var watchersBuilder = new WatchersBuilder(section);

            var watcher = new WatcherManager(watchersBuilder, section);
            watcher.FilteredItem += OnFiltered;
            watcher.StartWatch();

            Console.WriteLine(messages.EndAppInfoMessage);
            Console.ReadLine();
        }

        /// <summary>
        /// Handler for file filtered event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="arg">The <see cref="FileInfoEventArgs"/> arguments.</param>
        private static void OnFiltered(object sender, FileInfoEventArgs arg)
        {
            Console.WriteLine(messages.FileFilteredMessage, arg.FileName, arg.NewDirectory);
        }

        /// <summary>
        /// Create work directories.
        /// </summary>
        /// <param name="section">The section info.</param>
        private static void CreateWorkDirectory(WatcherConfigurationSection section)
        {
            foreach (WatcherDirectoryElement directory in section.WatchingDirectories)
            {
                if (!Directory.Exists(directory.Path))
                {
                    Directory.CreateDirectory(directory.Path);
                }
            }

            foreach (RuleElement rule in section.Rules)
            {
                if (!Directory.Exists(rule.OutputDirectory))
                {
                    Directory.CreateDirectory(rule.OutputDirectory);
                }
            }
        }
    }
}
