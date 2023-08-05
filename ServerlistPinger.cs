using AMP.Data;
using AMP.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading;

namespace AMP.DedicatedServer {
    internal class ServerlistPinger {

        private static string address = "https://" + ServerInit.serverConfig.serverSettings.masterServerUrl + "/ping";

        private static Thread pinger;

        private static int check_update = 500;
        private static int force_update = 55 / (check_update / 1000);

        private static int check_count = 0;
        private static string last_map = "";
        private static string last_mode = "";
        private static int last_playercount = 0;
        internal static bool ShouldUpdateMasterServer()
        {
            check_count++;
            bool ShouldUpdate = ModManager.serverInstance.connectedClients != last_playercount || ModManager.serverInstance.currentLevel != last_map || ModManager.serverInstance.currentMode != last_mode || check_count > force_update;
            if (ShouldUpdate) {
                last_playercount = ModManager.serverInstance.connectedClients;
                last_map = ModManager.serverInstance.currentLevel;
                last_mode = ModManager.serverInstance.currentMode;
                check_count = 0;
            }

            return ShouldUpdate;
        }

        internal static void Start() {
            var httpWebRequest = (HttpWebRequest) WebRequest.Create($"{address}/register.php");
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json; charset=utf-8";

            using(var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                string loginjson = JsonConvert.SerializeObject(new {
                    port        = ServerInit.serverConfig.serverSettings.port,
                    name        = ServerInit.serverConfig.serverSettings.servername,
                    description = ServerInit.serverConfig.serverSettings.serverdescription,
                    icon        = "",
                    max_players = ServerInit.serverConfig.serverSettings.max_players,
                    map         = ModManager.serverInstance.currentLevel,
                    mode        = ModManager.serverInstance.currentMode,
                    version     = Defines.MOD_VERSION,
                    pvp_enabled = ServerInit.serverConfig.hostingSettings.pvpEnable,
                    static_map  = ServerInit.serverConfig.hostingSettings.allowMapChange
                });

                streamWriter.Write(loginjson);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using(var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                    var result = streamReader.ReadToEnd();
                    if(result.Contains("true")) {
                        Log.Info("Server successfully registered in serverlist.");
                    } else {
                        Log.Err("Registration in serverlist failed: " + result);
                        return;
                    }
                }
            }

            ShouldUpdateMasterServer();
            pinger = new Thread(new ThreadStart(() => {
                while(ModManager.serverInstance != null) {
                    Thread.Sleep(check_update);
                    if (ShouldUpdateMasterServer()) {
                        httpWebRequest = (HttpWebRequest)WebRequest.Create($"{address}/ping.php");
                        httpWebRequest.ContentType = "application/json; charset=utf-8";
                        httpWebRequest.Method = "POST";
                        httpWebRequest.Accept = "application/json; charset=utf-8";

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string loginjson = JsonConvert.SerializeObject(new
                            {
                                port = ServerInit.serverConfig.serverSettings.port,
                                players = ModManager.serverInstance.connectedClients,
                                map = ModManager.serverInstance.currentLevel,
                                mode = ModManager.serverInstance.currentMode
                            });

                            streamWriter.Write(loginjson);
                            streamWriter.Flush();
                            streamWriter.Close();

                            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                var result = streamReader.ReadToEnd();
                                if (!result.Contains("true"))
                                {
                                    Log.Err("Serverlist update failed: " + result);
                                }
                            }
                        }
                    }
                }
            }));
            pinger.Start();
        }

        internal static void Stop() {
            if(pinger != null) {
                if(pinger.IsAlive) {
                    pinger.Abort();

                    var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{address}/unregister.php");
                    httpWebRequest.ContentType = "application/json; charset=utf-8";
                    httpWebRequest.Method = "POST";
                    httpWebRequest.Accept = "application/json; charset=utf-8";

                    using(var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                        string loginjson = JsonConvert.SerializeObject(new {
                            port = ServerInit.serverConfig.serverSettings.port
                        });

                        streamWriter.Write(loginjson);
                        streamWriter.Flush();
                        streamWriter.Close();

                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using(var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                            var result = streamReader.ReadToEnd();
                            if(result.Contains("true")) {
                                Log.Info("Server successfully unregistered from serverlist.");
                            } else {
                                Log.Err("Deregistration from serverlist failed: " + result);
                            }
                        }
                    }
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
