using AMP.Logging;
using System.IO;
using System.Threading;

namespace AMP.DedicatedServer.Commands {
    internal class CommandQueueCommand : CommandHandler {

        internal override string[] aliases => new string[] { "cq" };

        public override string Process(string[] args) {
            if(args.Length == 1 && args[0].Equals("stop")) {
                StopCommandQueue();
                return null;
            } else if(args.Length == 1) {
                StartCommandQueue(args[0]);
                return null;
            }

            return "Usage: cq <script>";
        }

        public void StartCommandQueue(string script) {
            StopCommandQueue();

            string path = script;
            if(!File.Exists(path)) path = script + ".txt";
            if(!File.Exists(path)) path = "scripts/" + script + ".txt";

            if(File.Exists(path)) {
                commandThread = new Thread(() => {
                    string[] lines = File.ReadAllLines(path);

                    if(lines.Length > 0) {
                        foreach(string line in lines) {
                            if(line.Length > 0) {
                                if(line.StartsWith("sleep ")) {
                                    try {
                                        int delay = int.Parse(line.Split(' ')[1]);
                                        Thread.Sleep(delay);
                                    } catch {
                                        Log.Warn("Couldn't process " + line);
                                    }
                                } else {
                                    Log.Info(line);
                                    Program.ProcessCommand(line);
                                }
                            }
                        }
                    }
                });
                commandThread.Start();

                executedCommand = script;
            } else {
                Log.Debug($"Script \"{path}\" not found!");
            }
        }

        Thread commandThread;
        string executedCommand;
        public void StopCommandQueue() {
            if(commandThread != null) {
                commandThread.Abort();
                commandThread = null;

                if(executedCommand != null) {
                    Log.Info($"Stopped executing command \"{executedCommand}\"");
                    executedCommand = null;
                }
            }
        }

        public override string GetHelp() {
            return "Executes a command queue script.";
        }
    }
}
