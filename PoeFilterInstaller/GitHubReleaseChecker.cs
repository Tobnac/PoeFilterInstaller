using QuickType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoeFilterInstaller
{
    public class GitHubReleaseChecker
    {
        public int CheckIntervalSeconds { get; set; } = 10;
        public event EventHandler OnNewFilterVersions;

        private GitHubReleases latestRelease;
        private readonly string apiUrl = "https://api.github.com/repos/NeverSinkDev/NeverSink-Filter/releases";
        private readonly WebClient client = new WebClient();

        public void Run()
        {
            while (true)
            {
                this.ProcessRelease(this.GetLastestRelease());
                Thread.Sleep(this.CheckIntervalSeconds * 1000);
            }
        }

        private GitHubReleases GetLastestRelease()
        {
            this.client.Headers.Add("User-Agent: Other");
            var jsonString = this.client.DownloadString(this.apiUrl);
            var jsonObj = GitHubReleases.FromJson(jsonString);
            return jsonObj.First();
        }

        private void ProcessRelease(GitHubReleases release)
        {
            var now = DateTimeOffset.Now;
            var releaseTime = release.PublishedAt;
            var releaseAge = now.Subtract(releaseTime);
            
            if (this.latestRelease == null || !release.PublishedAt.Equals(this.latestRelease.PublishedAt))
            {
                this.PrintReleaseInfo(release, releaseAge);
                this.latestRelease = release;
            }
            else
            {
                Console.WriteLine("no new release :(");
            }
        }

        private void PrintReleaseInfo(GitHubReleases release, TimeSpan releaseAge)
        {
            if (this.latestRelease == null)
            {
                var print = "Latest filter release was ";
                if (releaseAge.TotalDays >= 1) print += Math.Floor(releaseAge.TotalDays) + " days, ";
                if (releaseAge.Hours > 0) print += releaseAge.Hours + " hours, ";
                print += releaseAge.Minutes + " minutes ago";
                Console.WriteLine(print);
            }

            else
            {
                Console.WriteLine("NEW FILTER RELEASE!!!! NEVERSINK TOOK OUR ENERGY!");
                this.OnNewFilterVersions?.Invoke(release, EventArgs.Empty);
            }

            Console.WriteLine(release.Name);
        }


    }
}
