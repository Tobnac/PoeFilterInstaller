using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoeFilterInstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            // start watching download folders and move new filters
            new FileObserver().Run();

            // check for github releases (new filters)
            var gitHubber = new GitHubReleaseChecker();
            var thread = new Thread(new ThreadStart(gitHubber.Run));
            thread.Start();

            // endless loop
            while (Console.ReadLine() != "exit") ;
        }
    }
}
