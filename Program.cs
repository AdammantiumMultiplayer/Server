using AMP;
using AMP.Data;
using AMP.Logging;
using AMP.Network.Server;
using AMP.Threading;
using AMP_Server.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace AMP_Server {
    internal class Program {
        public static string SERVER_DEV_STATE = "Alpha";
        public static string SERVER_VERSION = "";
        public static string SERVER_NAME = "";

        public static void ReadVersion() {
            try {
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString().TrimEnd(new char[] { '.', '0' });
                if(version.Split('.').Length == 1) {
                    version += ".0";
                }
                if(version.Split('.').Length == 2) {
                    version += ".0";
                }

                SERVER_VERSION = SERVER_DEV_STATE + " " + version;
            } catch(Exception) { // With other languages the first one seems to screw up
                SERVER_VERSION = SERVER_DEV_STATE + " [VERSION ERROR] ";
            }
            SERVER_NAME = "AMP " + SERVER_VERSION;
        }

        private static Thread serverThread;

        static void Main(string[] args) {
            ReadVersion();
            ModManager.ReadVersion();

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
                     $"\t\t\t Server Version: {SERVER_VERSION}\r\n" +
                     $"\t\t\t    Mod Version: {ModManager.MOD_VERSION}\r\n" +
                      ".__________________________________________________________|_._._._._._._._._,\r\n" +
                      " ▜█████████████████████████████████████████████████████████|_X_X_X_X_X_X_X_X_|\r\n" +
                      "                                                           '\r\n" +
                      "</color>");

            RegisterCommands();

            Conf.Load("server.ini");
            ServerConfig.Load("config.ini");

            Server.DEFAULT_MAP = Conf.map;
            Server.DEFAULT_MODE = Conf.mode;

            ModManager.HostDedicatedServer((uint) ServerConfig.maxPlayers, Conf.port);

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

                    string[] command_args = input.Split(' ');
                    string command = command_args[0].ToLower();
                    List<string> list = new List<string>(command_args);
                    list.RemoveAt(0);

                    if(CommandHandler.CommandHandlers.ContainsKey(command)) {
                        string response = CommandHandler.CommandHandlers[command].Process(
                                            list.ToArray()
                                          );
                        if(response != null) Log.Info(response);
                    } else {
                        Log.Info($"Command \"{ command }\" could not be found.");
                    }
                    Thread.Sleep(1);
                }catch(Exception e) {
                    Log.Err(e);
                }
            }
        }

        static void RegisterCommands() {
            RegisterCommand("help", new HelpCommand());
            RegisterCommand("stop", new StopCommand());
            RegisterCommand("list", new ListCommand());
            RegisterCommand("status", new StatusCommand());
        }

        static void RegisterCommand(string command, CommandHandler commandHandler) {
            CommandHandler.CommandHandlers.Add(command.ToLower(), commandHandler);
        }
    }
}
