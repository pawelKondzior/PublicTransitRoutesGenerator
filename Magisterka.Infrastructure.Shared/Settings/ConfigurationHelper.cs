using System.IO;
using System.Reflection;

namespace Magisterka.Infrastructure.Shared.Settings
{
    public static class ConfigurationHelper
    {
        public static string _folderPath = null;

        public static   bool? _additionalLogging = null;
        public static   bool? _drowLines = null;


        public static string DataFolderPath
        {
            get
            {
                if (_folderPath == null)
                {
                    string folder = System.Configuration.ConfigurationSettings.AppSettings["DataLocation"];

                    string fullPath = System.Reflection.Assembly.GetAssembly(typeof(ConfigurationHelper)).Location;
                    string path = Path.Combine(Directory.GetParent(fullPath).Parent.Parent.FullName, folder);

                    _folderPath = path;
                }
                return _folderPath;
            }
        }

        public static bool AdditionalLogging
        {
            get
            {
                if (!_additionalLogging.HasValue)
                {
                    var value = System.Configuration.ConfigurationSettings.AppSettings["AdditionalLogging"];
                    _additionalLogging = bool.Parse(value);
                }


                return _additionalLogging.Value;

            }
        }

        public static bool DrowLines
        {
            get
            {
                if (!_drowLines.HasValue)
                {
                    var value = System.Configuration.ConfigurationSettings.AppSettings["DrowLines"];
                    _drowLines = bool.Parse(value);
                }


                return _drowLines.Value;

            }
        }




        public static string GoogleApiKey
        {
            get
            {
                
                string key = System.Configuration.ConfigurationSettings.AppSettings["GoogleApiKey"];
                    
                
                return key;
            }
        }
    }
}