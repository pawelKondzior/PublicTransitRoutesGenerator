using Anotar.Log4Net;
using AutoMapper;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Comparers;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.GoogleApi;
using Magisterka.Infrastructure.Shared.Settings;
using Magisterka.Infrastructure.Shared.Structure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Magisterka.Modules.Main.Services
{
    public class CombineBusStopFilesService : BaseBusStopService
    {
        private DataService DataService { get; set; }

        /// private BusLinesList CorrectBusStopList { get; set; }

        


    
    

     
        public CombineBusStopFilesService(BusStopList busStopsCSV, BusStopList busStopsTxt, BusLinesList busLines)
        { 
            DataService = DataService.DataServiceInstance;

            BusStopCSV = busStopsCSV;
            BusStopTXT = busStopsTxt;

            BusLinesList = busLines;
        }



        public void GenerateNewFileWithBusStops()
        {
            CorrectBusStopList = GetCorrectBusStopList();

            UpdateBusStopsWithNamesAndStreets(BusStopCSV, CorrectBusStopList);

            var busStopsInFileAndInBusLines = GetBusStopsExistingInCSVFileAndBusStopList();
            var busStopsToAdd = GetNewBusStopsToAdd(busStopsInFileAndInBusLines);

            busStopsToAdd.ForEach(busStop =>
            {
                var findItem = BusStopTXT.FirstOrDefault(x => x.Name == busStop.Name);

                if (findItem != null)
                {
                    busStop.X = findItem.X;
                    busStop.Y = findItem.Y;
                }
                else
                {
                    LogTo.Warn("brak poleczania");
                }

            });

            var completedList = busStopsToAdd.Where(x => x.HasGeometryPoints()).ToList();


            busStopsInFileAndInBusLines.AddRange(completedList);


            DataService.SaveBusStopList(busStopsInFileAndInBusLines);
        }






    }
}
