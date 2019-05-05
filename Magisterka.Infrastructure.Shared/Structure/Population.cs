// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Population.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The population.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Structure
{
    using System;
    using System.Collections.Generic;
    using Magisterka.Infrastructure.Shared.Enum;
    using System.Linq;
    using Magisterka.Infrastructure.Shared.Dto;
    using MoreLinq;
    using Magisterka.Infrastructure.Shared.Helpers;

    /// <summary>
    /// The population.
    /// </summary>
    public class Population : List<EntireRoute>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Population"/> class.
        /// </summary>
        public Population()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Population"/> class.
        /// </summary>
        /// <param name="lineCombinationList">
        /// The line combination list.
        /// </param>
        public Population(LineCombinationList lineCombinationList)
        {
            foreach (LineCombination item in lineCombinationList)
            {
                var entireRoute = new EntireRoute(item);
                this.Add(entireRoute);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Population"/> class.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <param name="busGraph">
        /// The bus graph.
        /// </param>
        /// <param name="busLinesList">
        /// The bus lines list.
        /// </param>
        public Population(List<List<BusStop>> list, BusGraph busGraph, BusLinesList busLinesList)
        {
            // busLinesList.get
            foreach (var item in list)
            {
                var entireRoute = new EntireRoute(item, busGraph);
                this.Add(entireRoute);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Population"/> class.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        public Population(IEnumerable<EntireRoute> list)
        {
            list.ForEach(x => this.Add(x));
        }


        #endregion

        ///// <summary>
        ///// Czas przyjścia na pierwzzy przystanek
        ///// </summary>
        //public Time ArriveTime { get; set; }

        /// <summary>
        /// obliczenie funkcji oceny dla każdego z osobników
        /// </summary>
        public void CalculateEvaluationFunction()
        {
            double evaluationTotal = 0d;

            this.ForEach(x => evaluationTotal += x.AdaptationFunctionResult);

            this.ForEach(x => x.EvaluationFunctionResult = x.AdaptationFunctionResult / evaluationTotal);
        }


       

        /// <summary>
        /// obliczenie funkcji przystosowania dla każdego z osobników
        /// </summary>
        /// <param name="functionTypeEnum">
        /// The function Type Enum.
        /// </param>
        /// <param name="ArriveTime">
        /// The Arrive Time. moze byc nulem, poza liczeniem po czasie trasy
        /// </param>
        public void CalculateAdaptationFunction(AdaptationFunctionTypeEnum functionTypeEnum, Time arriveTime)
        {
            if (functionTypeEnum == AdaptationFunctionTypeEnum.ByTime && arriveTime == null)
            {
                throw new ArgumentException("Brak podanego czasu rozpoczecia trasy");
            }

            if (functionTypeEnum == AdaptationFunctionTypeEnum.ByTime)
            {
                this.ForEach(x =>
                    {
                       // x.CalculateTimeFromArrive(arriveTime);
                        x.CalculateAdaptationFunction(functionTypeEnum);
                    });
            }
            else
            {
                this.ForEach(x => x.CalculateAdaptationFunction(functionTypeEnum));    
            }
        }

        /// <summary>
        /// The get best subject.
        /// </summary>
        /// <param name="functionTypeEnum">
        /// The function type enum.
        /// </param>
        /// <param name="arriveTime">
        /// The arrive time.
        /// </param>
        /// <returns>
        /// The Magisterka.Infrastructure.Shared.Structure.EntireRoute.
        /// </returns>
        public EntireRoute GetBestSubject(AdaptationFunctionTypeEnum functionTypeEnum, Time arriveTime)
        {
            this.CalculateAdaptationFunction(functionTypeEnum, arriveTime);
            this.CalculateEvaluationFunction();

            return this.OrderBy(x => x.EvaluationFunctionResult).FirstOrDefault();
        }


        public void OrderByTime()
        {
            this.OrderBy(x => x.RouteTime);
        }

        public void OrderByBusStopConnection()
        {
            this.OrderByDescending(x => x.BusConnectionsCount);
        }

        

        public List<HyrydizationItemDto> GetItemsToHybrydization()
        {
            var randomItems = RandomSelectorHelper.GetTwoDifrentRandomItems(this);

           
            return FindHybrydyzationPoints(randomItems.Item1, randomItems.Item2);
        }

        public static List<HyrydizationItemDto> FindHybrydyzationPoints( EntireRoute firstRoute, EntireRoute secondRoute)
        {
            List<HyrydizationItemDto> result = new List<HyrydizationItemDto>();


            foreach (var firstPartOfTheRoute in firstRoute.GetEverythingBeetwenStartEndEndStop())
            {
                foreach (var secondPartOfTheRoute in secondRoute.GetEverythingBeetwenStartEndEndStop())
                {
                    if (firstPartOfTheRoute.BusStop.Id != secondPartOfTheRoute.BusStop.Id)
                    {
                        continue;
                    }

                    if (firstPartOfTheRoute.OutputLine == null || secondPartOfTheRoute.OutputLine == null)
                    {
                        continue;
                    }

                    if (firstPartOfTheRoute.OutputLine.Name != secondPartOfTheRoute.OutputLine.Name)
                    {
                        var newResult = new HyrydizationItemDto(firstRoute, secondRoute, firstPartOfTheRoute.BusStop);
                        result.Add(newResult);
                    }
                }

            }

            return result;
        }

        //public List<HyrydizationItemDto> GetItemsToHybrydization()
        //{
        //    List<HyrydizationItemDto> result = new List<HyrydizationItemDto>();

        //    //foreach(var firstRoute in this)
        //    //{
        //    //    foreach (var partOfFistRoute in firstRoute.PartOfTheRoute)
        //    //    {

        //    //        foreach (var secondRoute in this)
        //    //        {
        //    //            if (firstRoute == secondRoute)
        //    //            {
        //    //                continue;
        //    //            }

        //    //            var cartesian = firstRoute.PartOfTheRoute.Cartesian(secondRoute.PartOfTheRoute,
        //    //                (start, end) => HyrydizationItemDto.CreateHyrydizationItemDto(firstRoute, secondRoute, start, end))
        //    //                .Where(x => x != null);


        //    //            result.AddRange(cartesian);

        //    //        }
        //    //    }

        //    //}
        //    return result;

        //}






        public bool AddWithCheckingMax(EntireRoute entireRoute, int maxItemsInPopulation)
        {
            if (this.Count < maxItemsInPopulation)
            {
                this.Add(entireRoute);

                return true;
            }

            return false;
        }
    }
}