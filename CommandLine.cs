using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AMP.DedicatedServer {
    public class CommandLine {
        private Dictionary<string, string> args = new Dictionary<string, string>();
        public CommandLine(string[] args) {
            if (UsingOldCommandLine(args)) {
                if (args.Length > 0) {
                    this.args["port"] = args[0];
                }

                if (args.Length > 1) {
                    this.args["max_players"] = args[1];
                }
            } else {
                for (int i=0; i<args.Length; i++) {
                    if (args[i].StartsWith("-")) {
                        if ((args.Length - 1) > i) {
                            this.args[args[i].Substring(1)] = args[i + 1];
                        } else {
                            this.args[args[i].Substring(1)] = "";
                        }
                    }
                }
            }
        }

        public bool HasArg(string name) {
            return args.ContainsKey(name);
        }

        public string GetArg(string name)
        {
            return args[name];
        }

        private bool UsingOldCommandLine(string[] args) {
            if (args.Length > 0) {
                for (int i=0; i<args.Length; i++) {
                    if (args[i].StartsWith("-")) {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}