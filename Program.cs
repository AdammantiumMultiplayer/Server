using AMP.Data;
using AMP.DedicatedServer.Commands;
using AMP.Logging;
using AMP.Network.Server;
using AMP.Threading;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace AMP.DedicatedServer {
    public class Program {
        public static Thread serverThread;

        internal static ServerConfig serverConfig;

        static void Main(string[] args) {
            Log.loggerType = Log.LoggerType.CONSOLE;

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
                     $"\t\t\t Server Version: { Data.Defines.SERVER_VERSION }\r\n" +
                     $"\t\t\t    Mod Version: {      Defines.MOD_VERSION    }\r\n" +
                      ".__________________________________________________________|_._._._._._._._._,\r\n" +
                      " ▜█████████████████████████████████████████████████████████|_X_X_X_X_X_X_X_X_|\r\n" +
                      "                                                           '\r\n" +
                      "</color>");

            serverConfig = ServerConfig.Load("server.json");
            
            ModManager.safeFile = new SafeFile();
            ModManager.safeFile.hostingSettings = serverConfig.hostingSettings;

            Server.DEFAULT_MAP  = serverConfig.serverSettings.map;
            Server.DEFAULT_MODE = serverConfig.serverSettings.mode;

            int  port        = serverConfig.serverSettings.port;
            uint max_players = (uint) serverConfig.serverSettings.max_players;
            string password  = serverConfig.serverSettings.password;

            if(args.Length > 0) {
                port = ushort.Parse(args[0]);

                if(args.Length > 1) {
                    max_players = uint.Parse(args[1]);

                    if(args.Length > 2) {
                        password = args[2];
                    }
                }
            }

            ModManager.HostDedicatedServer(max_players, port, password);

            RegisterCommands();
            int default_command_count = CommandHandler.CommandHandlers.Count;

            #region Plugins
            PluginLoader.LoadPlugins("plugins");
            PluginEventHandler.RegisterEvents();
            int plugin_command_count = CommandHandler.CommandHandlers.Count - default_command_count;
            #endregion
            Log.Info(Defines.SERVER, $"Found {default_command_count + plugin_command_count} (Default: {default_command_count} / Plugins: {plugin_command_count}) commands.");

            serverThread = new Thread(() => {
                while(ModManager.serverInstance != null) {
                    Thread.Sleep(1);
                    Dispatcher.UpdateTick();
                }
            });
            serverThread.Name = "ServerThread";
            serverThread.Start();

            Console.CancelKeyPress += delegate {
                new StopCommand().Process(new string[0]);
                serverThread.Abort();
                Environment.Exit(0);
            };

            while(ModManager.serverInstance != null) {
                try {
                    var input = Console.ReadLine();

                    if(input == null || input.Length == 0) continue;

                    ProcessCommand(input);

                    Thread.Sleep(1);
                }catch(Exception e) {
                    Log.Err(e);
                }
            }

            PluginLoader.UnloadPlugins();
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
    }
}
