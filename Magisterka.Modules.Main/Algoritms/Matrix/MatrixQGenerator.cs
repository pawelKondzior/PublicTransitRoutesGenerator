using Anotar.Log4Net;
using Magisterka.Infrastructure.Shared.Comparers;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Extensions;
using Magisterka.Infrastructure.Shared.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Modules.Main.Algoritms.Matrix
{
    public class MatrixQGenerator : BaseMatrixGenerator
    {
        private Stopwatch timer = new Stopwatch();

        protected bool TimeWarning = false;


        protected List<List<BusStop>> AllResults { get; set; }

        protected GenerateChangeStackModeEnum GenerateChangeStackModeEnum { get; set; }

        public MatrixQGenerator(AlgorithmParameters baseAlgorithmParameters) // :this()
            : base(baseAlgorithmParameters)
        {
            Matrix = AlgorithmParameters.Q;

            MaxGenerationCount = AlgorithmParameters.MaxComputetChangeStackResults;

            GenerateChangeStackModeEnum = GenerateChangeStackModeEnum.Flat;
        }

        public override List<List<BusStop>> Generate()
        {
            var combinationList = new List<BusStop>();
            AllResults = new List<List<BusStop>>();

            var qNumber = AlgorithmParameters.Q[AlgorithmParameters.Start.ArrayNumber, AlgorithmParameters.End.ArrayNumber];

            if (qNumber == 0)
            {
                LogTo.Warn("Brak połczenia między przystankami");
                return AllResults;
            }

            timer.Start();

            var maxChangeNumberPlusOne = AlgorithmParameters.ChangeNumber + 1;

            this.GenerateChangeStack(AlgorithmParameters.Start, AlgorithmParameters.End,
                combinationList, maxChangeNumberPlusOne);

            timer.Stop();

            AllResults.ForEach(X => X.Reverse());

            AllResults = AllResults.Distinct(new ListBusStopComparer()).ToList();
            AllResults.LogChangeStack();

            return AllResults;
        }


        /// <summary>
        /// Najpierw znajduje wszystkie w jednym levelu, później schodzi niżej
        /// </summary>
        protected void GenerateChangeStack(
            BusStop startBusStop,
            BusStop endBusStop,
            List<BusStop> combinationList,
            int numberOfChangesLeft
           )
        {

#if DEBUG
            if (timer.Elapsed.Seconds > 10)//&& !TimeWarning)
            {
                LogTo.Warn("Przekroczono czas");
                TimeWarning = true;
                return;
            }
#endif

            if (numberOfChangesLeft == 0 || AllResults.Count >= AlgorithmParameters.MaxComputetChangeStackResults)
            {
                return;
            }

            if (!combinationList.AddIfNotExists(endBusStop))
            {
                return;
            }

            if (endBusStop == startBusStop)
            {
                AllResults.AddCollectionIfNotExists(combinationList);

                // aggregateResults.AddIfNotExists(combinationList);  //)Add(combinationList);
                // aggregateResults.Add(combinationList);  //)Add(combinationList);
                return;
            }

            if (GenerateChangeStackModeEnum == GenerateChangeStackModeEnum.Flat)
            {
                GenerateChengeStackFlat(
                    startBusStop,
                    endBusStop,
                    combinationList,
                    numberOfChangesLeft);
            }
            else
            {
                GenerateChengeStackDeep(
                    startBusStop,
                    endBusStop,
                    combinationList,
                    numberOfChangesLeft);
            }
        }

        private void GenerateChengeStackFlat(BusStop startBusStop, BusStop endBusStop,
            List<BusStop> combinationList,
            int numberOfChangesLeft)
        {
            var nextBusStopList = new List<BusStop>();

            var qNumber = AlgorithmParameters.Q[startBusStop.ArrayNumber, endBusStop.ArrayNumber];

            if (qNumber == 1)
            {
                nextBusStopList.Add(startBusStop);
            }
            else if (numberOfChangesLeft > 1)
            {
                foreach (BusStop busStop in AlgorithmParameters.BusStopList)
                {
                    if (AlgorithmParameters.Q[busStop.ArrayNumber, endBusStop.ArrayNumber] == 1)
                    {
                        nextBusStopList.Add(busStop);
                    }
                }
            }

            ////  LogTo.Info("Next bus stop count {0}", nextBusStopList.Count);

            var busStopsToDeeperSearch = new List<BusStop>();

            foreach (BusStop nextBusStop in nextBusStopList)
            {
                if (nextBusStop == startBusStop)
                {
                    List<BusStop> completedCombinationList = combinationList;//.ToList();

                    if (completedCombinationList.AddIfNotExists(nextBusStop))
                    {
                        AllResults.AddCollectionIfNotExists(completedCombinationList);
                    }
                }
                else
                {
                    busStopsToDeeperSearch.Add(nextBusStop);
                }
            }


            ///LogTo.Info("numberOfChangesLeft {0}, busStopsToDeeperSearch {1} , aggregateResults {2}", numberOfChangesLeft, busStopsToDeeperSearch.Count, aggregateResults.Count);

            foreach (BusStop nextBusStop in busStopsToDeeperSearch)
            {
                this.GenerateChangeStack(
                    startBusStop,
                    nextBusStop,
                    combinationList.ToList(),
                    numberOfChangesLeft - 1);
            }
        }

        private void GenerateChengeStackDeep(BusStop startBusStop, BusStop endBusStop,
           List<BusStop> combinationList,
           int numberOfChangesLeft)
        {


            throw new NotImplementedException();

            //var tableGenLocalPopulatonCount = 0;

            //var nextBusStopList = new List<BusStop>();

            //if (AlgorithmParameters.Q[startBusStop.ArrayNumber, endBusStop.ArrayNumber] == 1)
            //{
            //    nextBusStopList.Add(startBusStop);
            //}
            //else
            //{
            //    foreach (BusStop busStop in AlgorithmParameters.BusStopList)
            //    {
            //        //if (tableGenLocalPopulatonCount >= AlgorithmParameters.MaxTableGenerationPopulationCount)
            //        //{
            //        /////    LogTo.Warn("TableGeneratedPopulatonCount >= AlgorithmParameters.MaxTableGenerationPopulationCount");
            //        //    break;
            //        //}

            //        if (AlgorithmParameters.Q[busStop.ArrayNumber, endBusStop.ArrayNumber] == 1)
            //        {
            //            nextBusStopList.Add(busStop);
            //        }
            //    }
            //}


            //var busStopsToDeeperSearch = new List<BusStop>();

            //foreach (BusStop nextBusStop in nextBusStopList)
            //{

            //}

        }

    }
}
