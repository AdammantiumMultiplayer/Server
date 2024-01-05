using AMP.Data;

namespace AMP.DedicatedServer.Commands {
    internal class UnbanCommand : CommandHandler {
        public override string[] ALIASES => new string[] { "unban" };
        public override string HELP => "Bans a player";

        public override string Process(string[] args) {
            if(args.Length >= 1) {
                Banlist.BanEntry entry = ModManager.banlist.Unban(args[0].ToLower());
                if(entry != null) {
                    return $"Player {entry.name} is now unbanned.";
                }
                return $"Player {args[0]} could not be found.";
            }
            return "unban <playername>";
        }
    }
}
