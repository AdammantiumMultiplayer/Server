using AMP.Data;
using AMP.DedicatedServer.Commands;
using AMP.Logging;
using AMP.Network.Server;
using AMP.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AMP.DedicatedServer {
    internal class Program {
        private static Thread serverThread;

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
                     $"\t\t\t    Mod Version: {      Defines.MOD_VERSION }\r\n" +
                      ".__________________________________________________________|_._._._._._._._._,\r\n" +
                      " ▜█████████████████████████████████████████████████████████|_X_X_X_X_X_X_X_X_|\r\n" +
                      "                                                           '\r\n" +
                      "</color>");

            RegisterCommands();

            Conf.Load("server.ini");
            ServerConfig.Load("config.ini");
            GameConfig.showPlayerNames = false;
            GameConfig.showPlayerHealthBars = false;

            Server.DEFAULT_MAP = Conf.map;
            Server.DEFAULT_MODE = Conf.mode;

            ModManager.HostDedicatedServer((uint) ServerConfig.maxPlayers, Conf.port);

            #region Plugins
            PluginLoader.LoadPlugins("plugins");
            PluginEventHandler.RegisterEvents();
            #endregion

            serverThread = new Thread(() => {
                while(ModManager.serverInstance != null) {
                    Thread.Sleep(1);
                    Dispatcher.UpdateTick();
                }
            });
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

            try {
                KeyValuePair<string, CommandHandler> foundCommand =
                    CommandHandler.CommandHandlers.First((item) => item.Key.Equals(command)
                                                                || item.Value.aliases.Contains(command));

                string response = foundCommand.Value.Process(
                                    list.ToArray()
                                    );
                if(response != null) Log.Info(response);
            } catch(InvalidOperationException) {
                Log.Info($"Command \"{command}\" could not be found.");
            }
        }

        static void RegisterCommands() {
            RegisterCommand("help",   new HelpCommand()        );
            RegisterCommand("stop",   new StopCommand()        );
            RegisterCommand("list",   new ListCommand()        );
            RegisterCommand("status", new StatusCommand()      );
            RegisterCommand("kick",   new KickCommand()        );
            RegisterCommand("say",    new SayCommand()         );
            RegisterCommand("si",     new SpawnItemCommand()   );
            RegisterCommand("cq",     new CommandQueueCommand());
            RegisterCommand("pl",     new PluginCommand()      );
        }

        static void RegisterCommand(string command, CommandHandler commandHandler) {
            CommandHandler.CommandHandlers.Add(command.ToLower(), commandHandler);
        }
    }
}
