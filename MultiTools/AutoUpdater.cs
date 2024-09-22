using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Exiled.API.Features;
using static MultiTools.Plugin;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;
using Newtonsoft.Json;

namespace MultiTools
{
    public class AutoUpdater
    {
        static bool anyPluginToUpdate = false;

        internal void SendWarn()
        {
            string settingsContent = File.ReadAllText(Path.Combine(Plugin.Instance.Config.FolderPath, "settings.txt"));

            if (settingsContent.Contains("allow=false") && Plugin.warningsent == false)
            {
                Log.Warn($"Hey, thank you for installing MultiTools!\nTo enable automatic and manual plugin updating, type \"allow\" or go to {Plugin.Instance.Config.FolderPath}/settings.txt and where you see \"allow\" enter true instead of false\nDon't worry, even if you send \"allow\", the plugins must be enabled in the plugin settings to be installed automatically, they will NOT be installed unless you enable them.");
                Plugin.warningsent = true;

            }
            if (!anyPluginToUpdate && settingsContent.Contains("allow=true"))
            {
                Log.Warn("No plugins to update. Make sure at least one plugin is enabled for updates!");
            }
        }

        internal static void UpdatePlugins()
        {


            string permissionContent = File.ReadAllText(Path.Combine(Plugin.Instance.Config.FolderPath, "settings.txt"));

            if (permissionContent.Contains("allow=false")) return;

            var customPluginList = LoadCustomPluginList(Path.Combine(Plugin.Instance.Config.FolderPath, "Custom-Updater.yml"));
            var allPluginToUpdate = Instance.pluginToUpdate;

            ;
                if (IsPluginInstalled(allPluginToUpdate.Name))
                {
                    try
                    {
                        if (IsUpdateAvailable(allPluginToUpdate))
                        {
                            if (permissionContent.Contains("allow=true"))
                            {
                                if (ShouldUpdatePlugin(allPluginToUpdate))
                                {
                                    Log.Warn($"Checking the plugin {allPluginToUpdate.Name}....");
                                    UpdatePlugin(allPluginToUpdate);
                                    anyPluginToUpdate = true;

                                    if (customPluginList.Any(p => p.Name.Equals(allPluginToUpdate.Name, StringComparison.OrdinalIgnoreCase)))
                                    {
                                        Log.Warn($"Plugin {allPluginToUpdate.Name} updated!");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Log.Debug($"{allPluginToUpdate.Name} is already updated to the latest version.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"Error updating plugin: {ex.Message}");

                        if (ex is WebException webEx && webEx.Response is HttpWebResponse response)
                        {
                            if (response.StatusCode == HttpStatusCode.NotFound)
                            {
                                Log.Error("Error 404: Unable to find update. Make sure the URL is correct.");
                            }
                            else
                            {
                                Log.Error($"HTTP Error: {response.StatusCode}");
                            }
                        }
                        else if (ex is WebException WebEx && WebEx.Status == WebExceptionStatus.NameResolutionFailure)
                        {
                            Log.Error("Name resolution error: Make sure your server has a working network connection.");
                        }
                        else
                        {
                            Log.Error($"Unknown error during HTTP request: {ex.Message}");
                        }
                    }
                }
                else
                {
                    Log.Debug($"{allPluginToUpdate.Name} isn't installed on the server. The update will be ignored.");
                }
        }


        private static bool ShouldUpdatePlugin(PluginInfo pluginInfo)
        {
            var customPluginList = LoadCustomPluginList(Path.Combine(Plugin.Instance.Config.FolderPath, "Custom-Updater.yml"));

            switch (pluginInfo.Name)
            {
                case "MultiTools" when Plugin.Instance.Config.AutoUpdate:
                    return true;
                default:
                    if (customPluginList.Any(p => p.Name.Equals(pluginInfo.Name, StringComparison.OrdinalIgnoreCase)))
                        return true;
                    else
                        return false;
            }
        }


        internal static bool IsPluginInstalled(string pluginName)
        {
            string pluginPath = Path.Combine(Paths.Plugins, $"{pluginName}.dll");
            return File.Exists(pluginPath);
        }

        internal static bool IsUpdateAvailable(PluginInfo pluginInfo)
        {
            return true;
        }





        internal static void UpdatePlugin(PluginInfo pluginInfo)
        {
            string dllUrl = $"{pluginInfo.GitHubRepoUrl}/releases/latest/download/{pluginInfo.Name}.dll";

            try
            {
                DownloadFile(dllUrl, Path.Combine(Paths.Plugins, $"{pluginInfo.Name}.dll"));
                Log.Info($"{pluginInfo.Name} validated and updated to the latest version!");
            }
            catch (WebException ex)
            {
                Log.Error($"Error downloading file: {ex.Message}");

            }

        }



        internal static void DownloadFile(string fileUrl, string savePath)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(fileUrl, savePath);
            }
        }

        internal static void CreatePLuginUpdaterFiles()
        {
            try
            {

                if (!Directory.Exists(Plugin.Instance.Config.FolderPath))
                {
                    Directory.CreateDirectory(Plugin.Instance.Config.FolderPath);
                }

                if (!File.Exists(Path.Combine(Plugin.Instance.Config.FolderPath, "settings.txt")))
                {

                    File.WriteAllText(Path.Combine(Plugin.Instance.Config.FolderPath, "settings.txt"), "allow=false", Encoding.Default);
                    Log.Warn($"I am creating settings.txt file in the AutoUpdater folder....");
                }
            }
            catch (Exception ex)
            {

                Log.Error($"Error creating YAML file: {ex.Message}");
            }
        }


        internal static PluginInfo[] LoadCustomPluginList(string filePath)
        {
            try
            {

                if (!File.Exists(filePath))
                {
                    Log.Warn($"File {filePath} not found. Creating an empty list.");
                    return Array.Empty<PluginInfo>();
                }


                using (var reader = new StreamReader(filePath))
                {
                    var deserializer = new Deserializer();
                    var customPluginList = deserializer.Deserialize<List<PluginInfo>>(reader);
                    return customPluginList?.ToArray() ?? Array.Empty<PluginInfo>();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error loading YAML file: {ex.Message}");
                return Array.Empty<PluginInfo>();
            }
        }
    }
}
