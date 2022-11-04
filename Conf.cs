using AMP.Data;

namespace AMPS {
    internal class Conf {
        public static INIFile settings;

        public static int port = 26950;
        public static string map = "Arena";
        public static string mode = "Sandbox";

        public static void Load(string path) {
            settings = new INIFile(path);

            if(!settings.FileExists()) {
                Save();
            }

            port = settings.GetOption("port", port);
            map = settings.GetOption("map", map);
            mode = settings.GetOption("mode", mode);
        }

        public static void Save() {
            settings.SetOption("port", port);
            settings.SetOption("map", map);
            settings.SetOption("mode", mode);

        }
    }
}
