using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMP_Server {
    public class CommandHandler {

        public static Dictionary<string, CommandHandler> CommandHandlers = new Dictionary<string, CommandHandler>();

        public virtual string Process(string[] args) {
            return GetHelp();
        }

        public virtual string GetHelp() {
            return "No help provided.";
        }

    }
}
