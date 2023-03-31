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

            pinger = new Thread(new ThreadStart(() => {
                while(ModManager.serverInstance != null) {
                    Thread.Sleep(60 * 1000);

                    httpWebRequest = (HttpWebRequest) WebRequest.Create($"{address}/ping.php");
                    httpWebRequest.ContentType = "application/json; charset=utf-8";
                    httpWebRequest.Method = "POST";
                    httpWebRequest.Accept = "application/json; charset=utf-8";

                    using(var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                        string loginjson = JsonConvert.SerializeObject(new {
                            port = ServerInit.serverConfig.serverSettings.port,
                            players = ModManager.serverInstance.connectedClients,
                            map = ModManager.serverInstance.currentLevel,
                            mode = ModManager.serverInstance.currentMode
                        });

                        streamWriter.Write(loginjson);
                        streamWriter.Flush();
                        streamWriter.Close();

                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using(var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                            var result = streamReader.ReadToEnd();
                            if(result.Contains("true")) {
                                
                            } else {
                                Log.Err("Serverlist update failed: " + result);
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
