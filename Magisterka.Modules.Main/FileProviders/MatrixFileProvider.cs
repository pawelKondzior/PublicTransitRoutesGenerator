using log4net;
using Magisterka.Infrastructure.Shared.Enum;
using System;
using System.IO;
using Magisterka.Infrastructure.Shared.Helpers;
using Magisterka.Infrastructure.Shared.Settings;
using Magisterka.Modules.Main.Services;

namespace Magisterka.Modules.Main.FileProviders
{
    public class MatrixFileProvider
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MatrixFileProvider).Name);

        private int Power { get; set; }

        private int? Link { get; set; }

        private MatrixTypeEnum MatrixTypeEnum { get; set; }

        private static readonly DataService DataService = DataService.DataServiceInstance;

        // public string AppDataDir { get; set; }

        public MatrixFileProvider(MatrixTypeEnum matrixTypeEnum, int power)
        {
            MatrixTypeEnum = matrixTypeEnum;
            Power = power;

            /// Init();
        }

        public MatrixFileProvider(MatrixTypeEnum matrixTypeEnum, int power, int? link)
        {
            MatrixTypeEnum = matrixTypeEnum;
            Power = power;
            Link = link;

            ///    Init();
        }

        //private void Init()
        //{
        //  //  AppDataDir  = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Matrix");
        //    /// AppDataDir  = Path.Combine(ConfigurationHelper.DataFolderPath, "Matrix");

        //    AppDataDir = ConfigurationHelper.DataFolderPath;
        //    if (!Directory.Exists(AppDataDir))
        //    {
        //        Directory.CreateDirectory(AppDataDir);
        //    }
        //}

        //private string GetMatrixFileName()
        //{
        //    if (Link.HasValue)
        //    {
        //        return string.Format(@"{0}\Matrix\{1}_{2}_{3}.txt",
        //            AppDataDir, MatrixTypeEnum, Power, Link.Value);
        //    }
        //    return string.Format(@"{0}\Matrix\{1}_{2}.txt",
        //            AppDataDir, MatrixTypeEnum, Power);
        //}

        //private StreamReader GetStreamReader()
        //{
        //    var filePath = FileNameHelper.GetMatrixFilePath(MatrixTypeEnum, this.Link); 


        //    return new StreamReader(filePath);
        //}

        public int[,] ReadMatrix()
        {
            var filePath = FileNameHelper.GetMatrixWithPowerFilePath(MatrixTypeEnum, this.Power, this.Link);

            if (!File.Exists(filePath))
            {
                log.Error(string.Format("Brak pliku {0}", filePath));
                return null;
            }

            return DataService.LoadMatrixFromFile(filePath);
        }
    }
}