using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Settings;

namespace Magisterka.Infrastructure.Shared.Helpers
{
    public static class FileNameHelper
    {

        public static readonly string BusStopFileCSV = ConfigurationHelper.DataFolderPath + @"\SlupkiWspolrzedne.csv";// @"\busStop.xml";

        public static readonly string BusStopNewFileTXT = ConfigurationHelper.DataFolderPath + @"\stops.txt";// @"\busStop.xml";

        public static readonly string BusStopFileXML = ConfigurationHelper.DataFolderPath + @"\NewBusStops.xml";// @"\busStop.xml";

        public static readonly string BusLinesPath = ConfigurationHelper.DataFolderPath + @"\bus";

        public  static readonly string MatixFilesPath = ConfigurationHelper.DataFolderPath + @"\Matix";


        public static string GetMatrixFilePath(MatrixTypeEnum matrixTypeEnum, int? maxWalkTime)
        {
            return System.IO.Path.Combine(MatixFilesPath, GetMatrixFileName(matrixTypeEnum, maxWalkTime));
        }


        public static string GetMatrixWithPowerFilePath(MatrixTypeEnum matrixTypeEnum, int power, int? maxWalkTime)
        {
            return System.IO.Path.Combine(MatixFilesPath, GetMatrixWithPowerFileName(matrixTypeEnum, power, maxWalkTime));
        }


        //var folderPath = ConfigurationHelper.DataFolderPath;
        //var path = string.Empty;
        //int power
        public static string GetMatrixFileName(MatrixTypeEnum matrixTypeEnum,  int? maxWalkTime)
        {
            if (maxWalkTime.HasValue)
            {
                return $"{matrixTypeEnum.ToString()}_{maxWalkTime.Value}.txt";
            }

            return $"{matrixTypeEnum.ToString()}.txt";
        }

        public static string GetMatrixWithPowerFileName(MatrixTypeEnum matrixTypeEnum, int power, int? maxWalkTime)
        {
            if (maxWalkTime.HasValue)
            {
                return $"{matrixTypeEnum.ToString()}_{power}_{maxWalkTime.Value}.txt";
            }

            return $"{matrixTypeEnum.ToString()}_{power}.txt";
        }
    }
}