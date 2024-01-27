using System;
using System.IO;
using System.Threading;
using AMP.DedicatedServer;

namespace AMP.Bootstrap {
    class Launcher {
        static void Main(string[] args) {
            Main(new CommandLine(args));
        }

        static void Main(CommandLine cmd) {
            try {
                Rename("ZZZ_AMP.dll", "AMP.dll");
                Rename("ZZZ_AMP.pdb", "AMP.pdb");

                CheckFile("Netamite.dll");
                CheckFile("Netamite.Steam.dll");
                CheckFile("Netamite.Unity.dll");
                CheckFile("Newtonsoft.Json.dll");
                CheckFile("Unity.ResourceManager.dll");
                CheckFile("UnityEngine.CoreModule.dll");
                CheckFile("UnityEngine.PhysicsModule.dll");
                CheckFile("UnityEngine.SharedInternalsModule.dll");
                CheckFile("ThunderRoad.dll");
                CheckFile("ThunderRoad.Manikin.dll");

                ServerInit.Start(cmd);
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("\nError: " + ex.Message);
                Console.WriteLine("An error occurred. See latest error.txt entry");
                using (StreamWriter writer = new StreamWriter("error.txt", true)) {
                    writer.WriteLine("\n--- " + DateTime.Now.ToString() + " ---");
                    writer.Write(ex.ToString());
                }

                Thread.Sleep(3000);
            }
        }

        public static void Rename(string file, string output) {
            bool output_exists = File.Exists(output);
            if (File.Exists(file) && !output_exists) {
                File.Move(file, output);
                Console.WriteLine("Found " + file + " renamed it to " + output);
            } else if (!output_exists) {
                Console.WriteLine("Missing dependency! (" + file + " or " + output + "). Read which files are required in the README");
                throw new Exception("Missing dependency! (" + file + " or " + output + "). Read which files are required in the README");
            }
        }

        public static void CheckFile(string file) {
            if (!File.Exists(file)) {
                throw new Exception("Missing dependency! (" + file + "). Read which files are required in the README");
            }
        }
    }
}