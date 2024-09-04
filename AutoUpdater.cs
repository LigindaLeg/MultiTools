using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Octokit;

namespace MultiTools
{
    public class AutoUpdater
    {
        private const string RepositoryOwner = "LigindaLeg";
        private const string RepositoryName = "MultiTools";
        private const string AssetName = "MultiTools.dll";

        private static readonly string LocalDllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AssetName);

        public static async Task CheckForUpdatesAsync()
        {
            try
            {
                var client = new GitHubClient(new ProductHeaderValue("AutoUpdater"));
                var releases = await client.Repository.Release.GetAll(RepositoryOwner, RepositoryName);

                if (releases.Count == 0)
                {
                    Console.WriteLine("Нет доступных релизов.");
                    return;
                }

                var latestRelease = releases[0];
                var asset = latestRelease.Assets.FirstOrDefault(a => a.Name.Equals(AssetName, StringComparison.OrdinalIgnoreCase));

                if (asset == null)
                {
                    Console.WriteLine("Не найден нужный файл в последнем релизе.");
                    return;
                }

                var localVersion = GetLocalFileVersion();
                var remoteVersion = latestRelease.TagName;

                if (localVersion != remoteVersion)
                {
                    Console.WriteLine($"Обновление доступно! ({localVersion} -> {remoteVersion})");
                    await DownloadAndReplaceAsync(asset.BrowserDownloadUrl);
                }
                else
                {
                    Console.WriteLine("Вы уже используете последнюю версию.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при проверке обновлений: {ex.Message}");
            }
        }

        private static string GetLocalFileVersion()
        {
            if (File.Exists(LocalDllPath))
            {
                var versionInfo = FileVersionInfo.GetVersionInfo(LocalDllPath);
                return versionInfo.FileVersion;
            }

            return "0.0.0.0";
        }

        private static async Task DownloadAndReplaceAsync(string url)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    Console.WriteLine("Скачивание обновления...");
                    await webClient.DownloadFileTaskAsync(new Uri(url), LocalDllPath);
                    Console.WriteLine("Обновление завершено.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при скачивании обновления: {ex.Message}");
            }
        }
    }
}
