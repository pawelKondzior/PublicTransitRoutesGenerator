using Anotar.Log4Net;
using log4net;
using Magisterka.Infrastructure.Shared.Settings;
// // -----------------------------------------------------------------------
// //  <copyright file="ListBusStopExt.cs" company="DevCore .NET">
// //      Copyright DevCore.NET All rights reserved.
// //  </copyright>
// //  <author>Paweł Kondzior</author>
// // -----------------------------------------------------------------------
using Magisterka.Infrastructure.Shared.Structure;
using System.Collections.Generic;
using System.Linq;

namespace Magisterka.Infrastructure.Shared.Extensions
{
    public static class LogExt
    {


        static bool additionalLogging = ConfigurationHelper.AdditionalLogging;

        public static void LogInOneLine(this EntireRoute entireRoute, string text = "")
        {
            if (!additionalLogging)
            {
                return;
            }

            var busStopsId = string.Join(" ", entireRoute.PartOfTheRoute.Select(x => x.BusStop.Id));

            LogTo.Info("{0} {1}", text, busStopsId);

        }
        

        public static void LogRoute(this EntireRoute entireRoute,  string text = "")
        {
            if (!additionalLogging)
            {
                return;
            }

            LogTo.Info("Wypisje trase {0} ----------------->", text);

            entireRoute.PartOfTheRoute.ForEach(route =>
                {
                    var inputLine = string.Empty;
                    var outputLine = string.Empty;

                    if (route.InputLine != null)
                    {
                        inputLine = route.InputLine.NameAndVariant;
                    }

                    if (route.OutputLine != null)
                    {
                        outputLine = route.OutputLine.NameAndVariant;
                    }
                    else if (route.NextBusStopConnection == Enum.ConnectionType.Walk)
                    {
                        outputLine = "walk";
                    }

                LogTo.Info("BusStopId: {0}, linia wchodzaca {1}, linia wychodzaca {2} nastepne polaczenie {3}", 
                    route.BusStop.Id, 
                    inputLine,
                    outputLine,
                    route.NextBusStopConnection.ToString());
            });

            LogTo.Info("Koniec wypisywania trasy ----------------->");

        }

        public static void Log(this List<EntireRoute> list, ILog log, string text = "")
        {
            if (!additionalLogging)
            {
                return;
            }

            list.ForEach(x =>
            {
                log.InfoFormat("<---------- Wypisywanie EntireRoute {0} ---------->", text);
                x.PartOfTheRoute.Log(log);
                log.Info("<---------- Koniec EntireRoute ---------->");
            });
        }

        public static void Log(this List<PartOfTheRoute> list, ILog log, string text = "")
        {
            if (!additionalLogging)
            {
                return;
            }
            // log.Info("<-- Wypisywanie PartOfTheRoute -->");

            foreach (var partOfTheRoute in list)
            {
                partOfTheRoute.Log(log, text);
            }

         //   log.Info("<-- Koniec PartOfTheRoute -->");
        }

        public static void Log(this PartOfTheRoute partOTheRoute, ILog log, string text = "")
        {
            if (!additionalLogging)
            {
                return;
            }

            var inputLineText = string.Empty;
            var outputLineText = string.Empty;

            if (partOTheRoute.InputLine != null)
            {
                inputLineText = partOTheRoute.InputLine.Name;
            }

            if (partOTheRoute.OutputLine != null)
            {
                outputLineText = partOTheRoute.OutputLine.Name;
            }


            var logText = string.Format("{0} busStopId: {1} Nazwa: {2} {3} typ nastepnego polaczenia: {4} ",
                text, partOTheRoute.BusStop.Id, partOTheRoute.BusStop.Name,
                partOTheRoute.BusStop.Street, partOTheRoute.NextBusStopConnection);


            if (!string.IsNullOrEmpty(inputLineText))
            {
                logText += string.Format("linia wejsciowa: {0} ", inputLineText);
            }

            if (!string.IsNullOrEmpty(outputLineText))
            {
                logText += string.Format("linia wyjsciowa: {0} ", outputLineText);
            }

            log.InfoFormat(logText);
        }



        public static void Log(this IEnumerable<BusStop> busStops, string text = "")
        {
            if (!additionalLogging)
            {
                return;
            }

            if (busStops == null)
            {
                LogTo.Warn("Brak listy przystanków");
                return;
            }

            LogTo.Info("Wypisywanei listy przystanków {0} ---------->", text);

            var prevId = 0;
            foreach (var x in busStops)
            {
                if (prevId != x.Id)
                {
                    LogTo.Info("BusStopId: {0} Nazwa {1} {2} ", x.Id, x.Name, x.Street);
                }
                else
                {
                    LogTo.Error("Zduplikowany BusStopId: {0} ", x.Id);
                }
                prevId = x.Id;
            }

            LogTo.Info("Koniec listy przystanków {0} ---------->", text);
        }


        public static void LogChangeStack(this List<List<BusStop>> collections)
        {
            if (!additionalLogging)
            {
                return;
            }

            LogTo.Debug("<------------ Wypisywanie change stack ---------->");

            collections.ForEach(list => LogTo.Info(string.Join(" ", list.Select(x => x.Id).ToArray())));

            LogTo.Debug("<------------ Koniec Wypisywania change stack  ---------->");
        }

        public static void Log(this List<List<BusStop>> collections)
        {
            if (!additionalLogging)
            {
                return;
            }

            LogTo.Info("<------------ Wypisywanie listy listy przystanków ---------->");

            collections.ForEach(x => x.Log());

            LogTo.Info("<------------ Koniec Wypisywania listy listy przystanków ---------->");
        }


        public static void LogJustBusStopNumbers(this List<List<BusStop>> collections)
        {
            if (!additionalLogging)
            {
                return;
            }

            LogTo.Info("<------------ Wypisywanie samych numerów przystanków ---------->");


            collections.ForEach(busStopList =>
            {
                var strCollecton = busStopList.Select(x => x.Id.ToString());
                var valueToLog = string.Join(" ", strCollecton);

                LogTo.Info(valueToLog);
            });


            LogTo.Info("<------------ Wypisywanie samych numerów przystanków ---------->");
        }



        public static void Log(this List<PartOfBusLine> list, ILog log)
        {
            if (!additionalLogging)
            {
                return;
            }

            log.Info("Wypisywanei listy PartOfBusLine ---------->");

            // var prevId = 0;
            list.ForEach(x =>
            {
                string startId = "brak";
                string endId = "brak";

                if (x.Start != null)
                {
                    startId = x.Start.Id.ToString();
                }

                if (x.End != null)
                {
                    endId = x.End.Id.ToString();
                }

                var busLineName = string.Empty;

                if (x.BusLineIoDto != null)
                {
                    busLineName = x.BusLineIoDto.NameAndVariant;
                }

                log.InfoFormat("Poczatkowy BusStopId: {0} Koncowy BusStopId: {1} Polaczenie: {2}  Trasa: {3}" ,
                    startId, endId, x.ConnectionType.ToString(), busLineName);
            });

            log.Info("Koniec listy PartOfBusLine ---------->");
        }
    }

}