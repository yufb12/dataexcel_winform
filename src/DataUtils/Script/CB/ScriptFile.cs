
namespace Feng.Script.CB
{

    public class ScriptFile
    {
        public string Path { get; set; }
        public string Contents { get; set; }
    }

    public class ArgsNull
    {
        private static ArgsNull dbnull = new ArgsNull();
        public static ArgsNull DBNULL { get { return dbnull; } }
    }

}
