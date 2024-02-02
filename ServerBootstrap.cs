using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using AMP.DedicatedServer;
using AMP.Logging;

namespace AMP.Bootstrap {
    class Launcher {
        static void Main(string[] args) {
            Main(new CommandLine(args));
        }

        private static List<string> missingFiles = new List<string>();

        static void Main(CommandLine cmd) {
            if(cmd.HasArg("?") || cmd.HasArg("help")) {
                Console.WriteLine("Possible Arguments:");
                Console.WriteLine("-port <port>\t\tSpecify a port, this will override the config file");
                Console.WriteLine("-max_players <num>\tSpecify a max amount of player, this will override the config file");
            } else {
                try {
                    Rename("ZZZ_AMP.dll", "AMP.dll");
                    Rename("ZZZ_AMP.pdb", "AMP.pdb");

                    CheckFile("Netamite.dll");
                    CheckFile("Netamite.Steam.dll");
                    CheckFile("Netamite.Unity.dll");
                    CheckFile("Netamite.Voice.dll");
                    CheckFile("Newtonsoft.Json.dll");
                    CheckFile("Unity.ResourceManager.dll");
                    CheckFile("UnityEngine.CoreModule.dll");
                    CheckFile("UnityEngine.PhysicsModule.dll");
                    CheckFile("UnityEngine.SharedInternalsModule.dll");
                    CheckFile("ThunderRoad.dll");
                    CheckFile("ThunderRoad.Manikin.dll");

                    if(missingFiles.Count == 0) {
                        ServerInit.Start(cmd);
                    } else {
                        throw new Exception("Missing dependencies:\n" + string.Join("\n", missingFiles) + "\n\nRead which files are required in the README");
                    }
                } catch(Exception ex) {
                    //Console.WriteLine(ex.ToString());
                    Log.Err("Error: " + ex.Message);
                    Log.Err("\nAn error occurred. See latest error.txt entry");

                    using(StreamWriter writer = new StreamWriter("error.txt", true)) {
                        writer.WriteLine("\n--- " + DateTime.Now.ToString() + " ---");
                        writer.Write(ex.ToString());
                    }

                    Thread.Sleep(10000);
                }
            }
        }

        public static void Rename(string file, string output) {
            bool output_exists = File.Exists(output);
            if (File.Exists(file) && !output_exists) {
                File.Move(file, output);
                Log.Warn("Found " + file + " renamed it to " + output);
            } else if (!output_exists) {
                missingFiles.Add(output);
            }
        }

        public static void CheckFile(string file) {
            if (!File.Exists(file)) {
                missingFiles.Add(file);
            }
        }
    }
}