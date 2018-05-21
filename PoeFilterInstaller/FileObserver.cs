using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoeFilterInstaller
{
    public class FileObserver
    {
        public bool ReplaceNewest { get; set; } = true;
        public string FilterFolder { get; set; } = "%userprofile%\\Documents\\My Games\\Path of Exile";
        public List<string> SourceFolders { get; set; } = new List<string>()
        {
            "%userprofile%\\Desktop", "%userprofile%\\Downloads"
        };        

        private List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();

        public void Run()
        {
            Init();
        }

        private void Init()
        {
            // "build" folder paths
            FilterFolder = Environment.ExpandEnvironmentVariables(FilterFolder);
            SourceFolders = SourceFolders.Select(x => Environment.ExpandEnvironmentVariables(x)).ToList();

            // verify that the folders are existing
            if (!Directory.Exists(FilterFolder) || !SourceFolders.All(x => Directory.Exists(x)))
            {
                Console.WriteLine("Error: Folders not found");
                Console.Read();
                throw new Exception();
            }

            // create watchers
            SourceFolders.ForEach(x => watchers.Add(new FileSystemWatcher(x)));

            // init watchers
            watchers.ForEach(x =>
            {
                x.Created += OnChange;
                x.EnableRaisingEvents = true;
            });

            // move existing files
            OnChange(null, null);
        }

        private void OnChange(object sender, FileSystemEventArgs arg)
        {
            // wait for download to finish
            Thread.Sleep(300);

            // all folders
            foreach (var path in SourceFolders)
            {
                // direct files
                foreach (var file in Directory.EnumerateFiles(path))
                {
                    CheckOutNewFile(file);
                }

                // files in sub folders (for archives, etc.)
                foreach (var folder in Directory.EnumerateDirectories(path))
                {
                    foreach (var file in Directory.EnumerateFiles(folder))
                    {
                        CheckOutNewFile(file);
                    }
                }
            }
        }

        private void CheckOutNewFile(string file)
        {
            if (IsFilterFile(file))
            {
                MoveFilterToGame(file);
            }
        }

        private void MoveFilterToGame(string originPath)
        {
            Console.WriteLine("Moving to filterFolder: " + originPath);
            var fileName = originPath.Split('\\').Last();

            if (this.ReplaceNewest)
            {
                var currentFilter = GetNewestFilter();
                File.Replace(originPath, currentFilter, currentFilter + "_new");
            }

            else
            {
                var id = 1;
                string newPath = FilterFolder + "\\" + fileName;
                while (File.Exists(newPath))
                {
                    newPath = FilterFolder + "\\(" + id++ + ") " + fileName;
                }

                File.Move(originPath, newPath);
            }
        }

        private string GetNewestFilter()
        {
            return Directory
                .EnumerateFiles(this.FilterFolder)
                .Where(x => IsFilterFile(x))
                .OrderByDescending(x => File.GetCreationTimeUtc(x))
                .FirstOrDefault();
        }

        private bool IsFilterFile(string file)
        {
            return file.Split('.').Last() == "filter";
        }

    }
}
