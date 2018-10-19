using System;
using System.Configuration;
using System.Globalization;
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
        private static string RussianCulture = "ru-RU";

        static void Main(string[] args)
        {
            var section = (WatcherConfigurationSection)ConfigurationManager.GetSection(Program.SectionName);

            Thread.CurrentThread.CurrentCulture = new CultureInfo(Program.RussianCulture);

            var watchersBuilder = new WatchersBuilder(section);

            var watcher = new Watcher(watchersBuilder, section);
            watcher.FilteredItem += OnFiltered;
            watcher.StartWatch();

            Console.WriteLine($"{messages.EndAppInfo}");
            Console.ReadLine();
        }

        /// <summary>
        /// Handler for file filtered event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="arg">The <see cref="FileInfoEventArgs"/> arguments.</param>
        private static void OnFiltered(object sender, FileInfoEventArgs arg)
        {
            Console.WriteLine($"{messages.Filtered} {arg.FileName} {messages._in} {arg.NewDirectory} ");
        }
    }
}
