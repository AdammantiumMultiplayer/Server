using AMP.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using static AMP.Data.SafeFile;

namespace AMP.DedicatedServer {
    internal class ServerConfig {

        [JsonIgnore]
        public string filePath = "";

        public ServerSettings serverSettings = new ServerSettings();
        public HostingSettings hostingSettings = new HostingSettings();

        public class ServerSettings {
            public int port = 26950;

            public string map = "Home";
            public string mode = "Default";

            public int max_players = 10;
            public string password = "";

            public string masterServerUrl = "bns.devforce.de";
            public bool showInServerList = false;
            public bool ignoreCertificateErrors = false;
            public string servername = "Unnamed Server";
            public string serverdescription = "No description";
        }

        #region Save and Load
        public void Save() {
            Save(filePath);
        }
        public void Save(string path) {
            Save(this, path);
        }

        public static void Save(ServerConfig serverConfig, string path) {
            string json = JsonConvert.SerializeObject(serverConfig, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public static ServerConfig Load(string path) {
            ServerConfig serverConfig = new ServerConfig();

            bool safe = true;
            if(File.Exists(path)) {
                string json = File.ReadAllText(path);
                try {
                    serverConfig = JsonConvert.DeserializeObject<ServerConfig>(json);

                    serverConfig.filePath = path;
                } catch(Exception e) {
                    Log.Err(e);
                    safe = false;
                }
            }

            if(safe) serverConfig.Save(path);

            return serverConfig;
        }
        #endregion
    }
}
