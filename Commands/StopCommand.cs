using AMP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMP_Server.Commands {
    internal class StopCommand : CommandHandler {

        public override string Process(string[] args) {
            ModManager.StopHost();
            return null;
        }

        public override string GetHelp() {
            return "Stops the server.";
        }
    }
}
