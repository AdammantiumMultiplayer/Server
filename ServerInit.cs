using AMP.Data;
using AMP.DedicatedServer.Commands;
using AMP.Logging;
using AMP.Network.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;

namespace AMP.DedicatedServer {
    public class ServerInit {
        internal static ServerConfig serverConfig;

        public static int port = 13698;

        internal static string serverIcon = "";
        internal static string gamemode_override = "";

        public static void Start(CommandLine cmd) {
            Log.loggerType = Log.LoggerType.CONSOLE;
            Netamite.Logging.Log.loggerType = Netamite.Logging.Log.LoggerType.EVENT_ONLY;

            Netamite.Logging.Log.onLogMessage += (type, message) => {
                Log.Msg((Log.Type) type, message);
            };

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Log.Info("");
            Log.Info( "<color=#FF8C00>" +
                      ",_._._._._._._._._|__________________________________________________________.\r\n" +
                      "|_X_X_X_X_X_X_X_X_|█████████████████████████████████████████████████████████▛\r\n" +
                      "                  '\r\n" +
                      "\t\t\t   █████╗ ███╗   ███╗██████╗  \r\n" +
                      "\t\t\t  ██╔══██╗████╗ ████║██╔══██╗ \r\n" +
                      "\t\t\t  ███████║██╔████╔██║██████╔╝ \r\n" +
                      "\t\t\t  ██╔══██║██║╚██╔╝██║██╔═══╝  \r\n" +
                      "\t\t\t  ██║  ██║██║ ╚═╝ ██║██║      \r\n" +
                      "\t\t\t  ╚═╝  ╚═╝╚═╝     ╚═╝╚═╝      \r\n" +
                     $"\t\t\t Server Version: { Data.Defines.SERVER_VERSION   }\r\n" +
                     $"\t\t\t    Mod Version: {      Defines.FULL_MOD_VERSION }\r\n" +
                      ".__________________________________________________________|_._._._._._._._._,\r\n" +
                      " ▜█████████████████████████████████████████████████████████|_X_X_X_X_X_X_X_X_|\r\n" +
                      "                                                           '\r\n" +
                      "</color>");

            serverConfig = ServerConfig.Load("server.json");
            
            ModManager.safeFile = new SafeFile();
            ModManager.safeFile.hostingSettings = serverConfig.hostingSettings;

            ModManager.banlist = Banlist.Load("banlist.json");

            Server.DEFAULT_MAP  = serverConfig.serverSettings.map;
            Server.DEFAULT_MODE = serverConfig.serverSettings.mode;

            port             = serverConfig.serverSettings.port;
            uint max_players = (uint) serverConfig.serverSettings.max_players;
            string password  = serverConfig.serverSettings.password;

            if (cmd.HasArg("port")) {
                port = ushort.Parse(cmd.GetArg("port"));
            }

            if (cmd.HasArg("max_players")) {
                max_players = uint.Parse(cmd.GetArg("max_players"));
            }

            if(File.Exists("icon.jpg")) {
                byte[] imageArray = File.ReadAllBytes("icon.jpg");
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                serverIcon = base64ImageRepresentation;
                //Log.Debug("Found custom server icon. Make sure its a jpg and 64x64.");
            }

            ModManager.SetupNetamite();
            ModManager.HostDedicatedServer(max_players, port, password, OnStart);


            Console.CancelKeyPress += delegate {
                new StopCommand().Process(new string[0]);
                ServerlistPinger.Stop();
                Environment.Exit(0);
            };

            while(ModManager.serverInstance != null) {
                try {
                    var input = Console.ReadLine();

                    if(input == null || input.Length == 0) continue;

                    ProcessCommand(input);

                    Thread.Sleep(1);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }

            ServerlistPinger.Stop();
            PluginLoader.UnloadPlugins();
        }

        public static void OnStart(string message) {
            if(message != null) {
                Console.WriteLine($"Serverstart failed: {message}");
                return;
            }

            RegisterCommands();
            int default_command_count = CommandHandler.CommandHandlers.Count;

            #region Plugins
            PluginLoader.LoadPlugins("plugins");
            if(serverConfig.serverSettings.plugin_autorefresh) {
                PluginWatcher.InitWatcher();
            }

            int plugin_command_count = CommandHandler.CommandHandlers.Count - default_command_count;
            #endregion
            Log.Info(Defines.SERVER, $"Registered {default_command_count + plugin_command_count} commands (Default: {default_command_count} / Plugins: {plugin_command_count}).");

            if(serverConfig.serverSettings.showInServerList) {
                ServerlistPinger.Start();
            }
        }

        public static void ProcessCommand(string input) {
            string[] command_args = input.Split(' ');
            string command = command_args[0].ToLower();
            List<string> list = new List<string>(command_args);
            list.RemoveAt(0);

            CommandHandler foundCommand = CommandHandler.GetCommandHandler(command);

            if(foundCommand != null) {
                string response = foundCommand.Process(
                                    list.ToArray()
                                    );
                if(response != null) Log.Info(response);
            } else {
                Log.Info($"Command \"{command}\" could not be found.");
            }
        }

        static void RegisterCommands() {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            foreach(Type type in types) {
                if(type.BaseType == typeof(CommandHandler)) {
                    CommandHandler handler = (CommandHandler)Activator.CreateInstance(type);
                    CommandHandler.RegisterCommandHandler(handler);
                }
            }
        }

        public static void SetGameModeOverride(string gamemode) {
            Log.Info(Defines.SERVER, $"Gamemode changed to \"{gamemode}\".");
            gamemode_override = gamemode;
        }
        public static void ResetGameModeOverride() {
            gamemode_override = string.Empty;
            Log.Info(Defines.SERVER, $"Gamemode set back to map default.");
        }
    }
}
