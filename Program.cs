using AMP;
using AMP.Data;
using AMP.Logging;
using System;
using System.Reflection;

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


        static void Main(string[] args) {
            ReadVersion();
            ModManager.ReadVersion();

            Log.loggerType = Log.LoggerType.CONSOLE;

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Log.Info("");
            Log.Info("");
            Log.Info("<color=#FF8C00>" +
                      ",_._._._._._._._._|__________________________________________________________.\r\n" +
                      "|_X_X_X_X_X_X_X_X_|█████████████████████████████████████████████████████████▛\r\n" +
                      "                  !\r\n" +
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
                      "                                                           !\r\n" +
                      "</color>");

            ServerConfig.Load("server.ini");

            ModManager.HostDedicatedServer((uint) ServerConfig.maxPlayers, 26950);
        }
    }
}
