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
    public class BusStopMetadataService : BaseBusStopService
    {



        

        private DataService DataService { get; set; }

        


        private readonly string GooglePlacesTextBasicQuery = @"https://maps.googleapis.com/maps/api/place/textsearch/json?" +
          "query={0}" +
          "&types=bus_station" +
          "&radius=5" +
          "&key={1}";

        private readonly string GooglePlacesTextNameQuery = @"https://maps.googleapis.com/maps/api/place/textsearch/json?" +
            "query={0}+in+wrocław" +
            "&types=bus_station" +
            "&radius=5" +
            "&key={1}";

        private readonly string GooglePlacesTextNameAndStreetQuery = @"https://maps.googleapis.com/maps/api/place/textsearch/json?" +
            "query={0}+{1}+wrocław" +
            "&types=bus_station" +
            "&radius=5" +
            "&key={2}";

        private readonly string GooglePlacesTextStreetQuery = @"https://maps.googleapis.com/maps/api/place/textsearch/json?" +
       "query={0}+in+wrocław" +
       "&radius=5" +
       "&key={1}";


        private readonly string GooglePlacesTextAnyType = @"https://maps.googleapis.com/maps/api/place/textsearch/json?" +
           "query={0}+wrocław" +
           "&radius=5" +
           "&key={1}";


        public BusStopMetadataService(BusStopList busStops, BusLinesList busLines)
        { 
            DataService = DataService.DataServiceInstance;
            BusStopCSV = busStops;
            BusLinesList = busLines;
        }

        public BusStopMetadataService()
        {
            DataService = DataService.DataServiceInstance;
            BusLinesList = this.DataService.GetBusLines(BusLineLoadTypEnum.WithNightLines);
            BusStopCSV = this.DataService.GetBusStopListCSV();
        }


        public void GenerateNewFileWithBusStops()
        {
            CorrectBusStopList = GetCorrectBusStopList();
            var busStopsInFileAndInBusLines = GetBusStopsExistingInCSVFileAndBusStopList();
            var busStopsToAdd = GetNewBusStopsToAdd(busStopsInFileAndInBusLines);

            busStopsToAdd.ForEach(busStop => CompleteBusStopMetadata(busStop));

            var completedList = busStopsToAdd.Where(x => x.HasGeometryPoints()).ToList();


            busStopsInFileAndInBusLines.AddRange(completedList);


            DataService.SaveBusStopList(busStopsInFileAndInBusLines);
        }


      


        private string GetQueryForBusStop(BusStop busStop)
        {
            var query = string.Format(GooglePlacesTextNameQuery, busStop.Name, ConfigurationHelper.GoogleApiKey);

            LogTo.Debug("GOOGLE QUERY: {0}", query);

            return query;
        }

        private string GetQueryForBusStopAndStree(BusStop busStop)
        {
            var query = string.Format(GooglePlacesTextNameAndStreetQuery, busStop.Name, busStop.Street, ConfigurationHelper.GoogleApiKey);

            LogTo.Debug("GOOGLE QUERY: {0}", query);

            return query;   
        }


         private string GetQueryBasic(BusStop busStop)
        {
            var query = string.Format(GooglePlacesTextBasicQuery, busStop.Name, ConfigurationHelper.GoogleApiKey);

            LogTo.Debug("GOOGLE QUERY: {0}", query);

            return query;
        }

        private string GetQueryForStreet(BusStop busStop)
        {
            var query = string.Format(GooglePlacesTextStreetQuery, busStop.Street, ConfigurationHelper.GoogleApiKey);

            LogTo.Debug("GOOGLE QUERY: {0}", query);

            return query;
        }

        private string GetQueryAnyType(BusStop busStop)
        {
            var query = string.Format(GooglePlacesTextAnyType, busStop.Name, ConfigurationHelper.GoogleApiKey);

            LogTo.Debug("GOOGLE QUERY: {0}", query);

            return query;
        }

        




        private double[] wrongLatArray = new double[] { 51.0961696, 51.1078852, 51.1078852 };
        private double[] wrongLngArray = new double[] { 17.0381909, 17.0385376, 17.0385376 };

        private bool IfResultCorrect(Result queryResult)
        {
            return queryResult != null
                && !wrongLatArray.Contains(queryResult.geometry.location.lat)
                && !wrongLngArray.Contains(queryResult.geometry.location.lng);
        }

        private void CompleteBusStopMetadata(BusStop busStop)
        {
            string query = null; 
            Result queryResult = null;


            LogTo.Info("Bus stop {0} street {1}", busStop.Name, busStop.Street);

            if (!string.IsNullOrEmpty(busStop.Street))
            {
                query = GetQueryForBusStopAndStree(busStop);
                queryResult = QueryGooglePlacesApi(busStop, query).Result;
            }


            if (IfResultCorrect(queryResult))
            {
                busStop.Y = queryResult.geometry.location.lat;
                busStop.X = queryResult.geometry.location.lng;

                LogTo.Info("Correct result with GetQueryForBusStopAndStree"); 
                return;
            }

            query = GetQueryForBusStop(busStop);
            queryResult = QueryGooglePlacesApi(busStop, query).Result;

            if (IfResultCorrect(queryResult))
            {
                busStop.Y = queryResult.geometry.location.lat;
                busStop.X = queryResult.geometry.location.lng;

                LogTo.Info("Correct result with GetQueryForBusStop");
                return;
            }



            if (!string.IsNullOrEmpty(busStop.Street))
            {
                query = GetQueryForStreet(busStop);
                queryResult = QueryGooglePlacesApi(busStop, query).Result;

                if (IfResultCorrect(queryResult))
                {
                    busStop.Y = queryResult.geometry.location.lat;
                    busStop.X = queryResult.geometry.location.lng;

                    LogTo.Info("Correct result with GetQueryForStreet");
                    return;
                }
            }


            query = GetQueryBasic(busStop);
            queryResult = QueryGooglePlacesApi(busStop, query).Result;

            if (IfResultCorrect(queryResult))
            {
                busStop.Y = queryResult.geometry.location.lat;
                busStop.X = queryResult.geometry.location.lng;

                LogTo.Info("Correct result with GetQueryBasic");
                return;
            }

            else
            {
                LogTo.Warn("Query for street");

                query = GetQueryAnyType(busStop);
                queryResult = QueryGooglePlacesApi(busStop, query).Result;

                if (queryResult != null)
                {
                    busStop.Y = queryResult.geometry.location.lat;
                    busStop.X = queryResult.geometry.location.lng;

                    LogTo.Info("Correct result with GetQueryAnyType");
                }
                else
                {
                    LogTo.Fatal("NIE WYGENEROWANO");
                }
            }


        }

        private async Task<Result> QueryGooglePlacesApi(BusStop busStop, string query)
        {
            
            PlacesApiQueryResponse result = null;

            using (var client = new HttpClient())
            {
                var response = await  client.GetStringAsync(query);

                result = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(response);
            }


            if (!result.results.Any())
            {
                LogTo.Warn("!result.results.Any()  id {0}", busStop.Id);
                return null;
            }

            Thread.Sleep(1000);

            return result.results.FirstOrDefault();
        }





    }
}
