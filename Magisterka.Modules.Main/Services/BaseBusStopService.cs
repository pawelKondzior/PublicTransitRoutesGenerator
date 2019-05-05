using Anotar.Log4Net;
using AutoMapper;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Comparers;
using Magisterka.Infrastructure.Shared.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Modules.Main.Services
{
    public abstract class BaseBusStopService
    {
        


        protected BusStopList BusStopCSV { get; set; }

        protected BusStopList BusStopTXT { get; set; }

        protected BusLinesList BusLinesList { get; set; }

        protected List<BusStop> CorrectBusStopList { get; set; }

        protected List<BusStop> GetCorrectBusStopList()
        {
            var correctBusStopList = BusLinesList.GetAllBusStopsDistinct();
            var correctMappedList = Mapper.Map<List<BusStop>>(correctBusStopList);

            LogTo.Debug("bus stops in lines count {0}", correctMappedList.Count);

            return correctMappedList;
        }


        protected void UpdateBusStopsWithNamesAndStreets(List<BusStop> listToUpdate, List<BusStop> correctBusStopList)
        {
            listToUpdate.ForEach(busItem =>
            {
                var itemWithNames = correctBusStopList.FirstOrDefault(x => x.Id == busItem.Id);

                if (itemWithNames != null)
                {
                    busItem.Name = itemWithNames.Name;
                    busItem.Street = itemWithNames.Street;
                }

            });
        }
            


        protected List<BusStop> GetBusStopsExistingInCSVFileAndBusStopList()
        {
            var busStopsInFileButNotInBusLines = new List<BusStop>();
            var busStopsInFileAndInBusLines = new List<BusStop>();


            BusStopCSV.ForEach(busItem =>
            {
                if (CorrectBusStopList.Contains(busItem, new BusStopComparer()))
                {
                    busStopsInFileAndInBusLines.Add(busItem);
                }
                else
                {
                    busStopsInFileButNotInBusLines.Add(busItem);
                }
            });

            return busStopsInFileAndInBusLines;
        }


        protected List<BusStop> GetNewBusStopsToAdd(List<BusStop> busStopsInFileAndInBusLines)
        {
            var busStopsToAdd = new List<BusStop>();
            var busStopsAlreadyInList = new List<BusStop>();

            CorrectBusStopList.ForEach(newBusItem =>
            {
                if (!busStopsInFileAndInBusLines.Contains(newBusItem, new BusStopComparer()))
                {
                    busStopsToAdd.Add(newBusItem);
                }
                else
                {
                    busStopsAlreadyInList.Add(newBusItem);
                }
            });


            return busStopsToAdd;
        }

    }
}
