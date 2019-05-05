// -----------------------------------------------------------------------
//  <copyright file="ArrayGenerator.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Magisterka.Infrastructure.Shared.IoDto;
using Magisterka.Infrastructure.Shared.Structure;
using System.Linq;
using Anotar.Log4Net;
using Magisterka.Modules.Main.Services;
using MoreLinq;

namespace Magisterka.Modules.Main.Algoritms.PopulationGenerators
{

    /// <summary>
    /// Troche nie czaje czym sie różni od BaseArrayGenerator
    /// </summary>
    public class ArrayGenerator : BasePopulationGenerator
    {

        #region Prpoerties

        private Stack<int> BusStopStack { get; set; }

        #endregion

        #region Construcotrs

       

        public ArrayGenerator(AlgorithmParameters baseAlgorithmParameters)
            :base(baseAlgorithmParameters)
        {
             BusStopStack = new Stack<int>();
            this.Population = new Population();
        }

        #endregion 


        #region Methods

        public override Population Generate()
        {


            if (AlgorithmParameters.Q[AlgorithmParameters.Start.ArrayNumber, AlgorithmParameters.End.ArrayNumber] == 0)
            {
                return null;
            }

            GenerateChangeStack(AlgorithmParameters.Start.ArrayNumber, AlgorithmParameters.End.ArrayNumber, AlgorithmParameters.ChangeNumber);

            return Population;


            return new Population();
        }

        public void GenerateChangeStack(int start, int end, int numberOfChangesLeft)
        {
            

            if (numberOfChangesLeft == 0 || Population.Count >= AlgorithmParameters.PopulationCount)
            {
                return;
            }

            BusStopStack.Push(end);

            var busStopStackString = string.Empty;
            BusStopStack.ForEach(x => busStopStackString += " " + x + " ");

            //LogTo.Debug("BusStopStack " +  busStopStackString);
            




            for (int i = 0; i < AlgorithmParameters.BusStopList.Count; i++)
            {
                if (AlgorithmParameters.Q[i, end] == 1 && !BusStopStack.Contains(i))
                {
                    if (i == start)
                    {
                        BusStopStack.Push(i);

                        addBusRoute(BusStopStack);

                        LogTo.Debug("BusStopStack " + busStopStackString);

                        if (Population.Count >= AlgorithmParameters.PopulationCount)
                            return;

                        BusStopStack.Pop();
                    }
                    else
                    {
                        GenerateChangeStack(start, i, numberOfChangesLeft - 1);
                    }
                }
            }

            BusStopStack.Pop();
        }


        static void Print(String s)
        {
            Console.WriteLine(s);
        }

        //dorobić sprawdzanie linkow pieszych
        //metoda tworząca trasy, gdy znajdziemy listę przesiadek do przystanku poczatkowego
        private void addBusRoute(Stack<int> stack)
        {
            LogTo.Debug("addBusRoute(Stack<int> stack)");

            int[] array = stack.ToArray();

            //słownik: przystanek i lista linii do następnego przystanku
            // Dictionary<int, List<String>> lineListToNextBusStop = new Dictionary<int, List<String>>();

            int routeCount = 1;

            var connectingBusLines = new List<PartOfBusLine>();
            var walikngLineChangesList = new List<WalkLineChanges>();

            for (int i = 0; i < array.Length - 1; i++)
            {
                connectingBusLines.AddRange(AlgorithmParameters.BusLines.GetConnectedBusLines(array[i], array[i + 1], AlgorithmParameters.BusStopList, AlgorithmParameters.MaxWalkTime));

                var walkLine = AlgorithmParameters.BusGraph.GetWalkLines(array[i], array[i + 1], AlgorithmParameters.BusStopList);

                if (walkLine != null)
                {
                    walikngLineChangesList.Add(walkLine);
                }

                // routeCount *= lines.Count;
            }

            //wygenerować wszystkie kombinacje linii
            //lista wszystkich kombinacji linii

           // var startLines = connectingBusLines.Where(x => x.FirstOrDefault().BusStopId == Start.Id);
          //  var restOfTheLines = connectingBusLines.Where(x => x.FirstOrDefault().BusStopId != Start.Id);

            var subsets = CreateSubsets(connectingBusLines.ToArray());

            var result = new List<List<PartOfBusLine>>();

            foreach (var item in subsets)
            {
                if (item.Count() > 1)
                {
                    var permColl = permutations(item);

                    foreach (var perm in permColl)
                    {
                        result.Add(perm.ToList());
                    }
                }
                else
                {
                    result.Add(item.ToList());
                }
            }

            var lineCombinationsList = new List<LineCombination>();

            foreach (var item in result)
            {
                var newListItem = new LineCombination(item, walikngLineChangesList, AlgorithmParameters.Start, AlgorithmParameters.End);

                if (newListItem.ValidLineCombination)
                {
                    lineCombinationsList.Add(newListItem);    
                }
            }


            int x = 3;

            // var result  = connectingBusLines.GetCombinations(null  , false);
            //var result = GetCombinations(connectingBusLines, 3).ToList();

            //var combinationGenerator = new CombinationGenerator<BusLineIoDto>();

            //var res = combinationGenerator.ProduceWithoutRecursion(connectingBusLines);

            //

            //foreach (var item in startLines)
            //{

            //}

            ////generujemy pełną listę przystanków

            //foreach (List<String> combination in lineCombinations)
            //{
            //    EntireRoute candidate = new EntireRoute();

            //    String actualBusStop = start.Id;
            //    int[] actualArriveTime = new int[2];
            //    arriveTime.CopyTo(actualArriveTime, 0);

            //    for (int i = 0; i < array.Length - 1; i++)
            //    {
            //        if (combination[i].Contains("link"))
            //        {
            //            if (i == 0)
            //                candidate.addRouteElement(generateRouteElement(busStopList[actualBusStop], actualArriveTime, actualArriveTime, null, null, false));
            //            else
            //            {
            //                if (!combination[i - 1].Contains("link"))
            //                    candidate.addRouteElement(generateRouteElement(busStopList[actualBusStop], actualArriveTime, actualArriveTime, busList[combination[i - 1]], null, false));
            //                else
            //                    candidate.addRouteElement(generateRouteElement(busStopList[actualBusStop], actualArriveTime, actualArriveTime, null, null, false));
            //            }

            //            actualArriveTime[1] += Int32.Parse(combination[i].Substring(4));
            //            if (actualArriveTime[1] >= 60)
            //            {
            //                actualArriveTime[0]++;
            //                actualArriveTime[1] = actualArriveTime[1] % 60;
            //            }

            //            if (busStopList.Keys.ElementAt(array[i + 1]).CompareTo(end.Id) == 0)
            //                candidate.addRouteElement(generateRouteElement(busStopList[end.Id], actualArriveTime, null, null, null, false));

            //            actualBusStop = busStopList.Keys.ElementAt(array[i + 1]);
            //            continue;
            //        }

            //        bool change = false;

            //        //dodajemy minimalny czas przesiadki (pomijając przystanek startowy)
            //        if (i != 0)
            //        {
            //            actualArriveTime[1] += minChangeTime;

            //            if (actualArriveTime[1] >= 60)
            //            {
            //                actualArriveTime[0]++;
            //                actualArriveTime[1] = actualArriveTime[1] % 60;
            //            }

            //            change = true;
            //        }
            //        else
            //        {
            //            //jeżeli mamy mutacje i będzie przesiadka na tym przystanku to doliczamy minimalny czas przesiadki

            //            if (i == 0 && mutationLineIn != null)
            //            {
            //                if (combination[i].CompareTo(mutationLineIn.Name) != 0)
            //                {
            //                    actualArriveTime[1] += minChangeTime;

            //                    if (actualArriveTime[1] >= 60)
            //                    {
            //                        actualArriveTime[0]++;
            //                        actualArriveTime[1] = actualArriveTime[1] % 60;
            //                    }

            //                    change = true;
            //                }
            //            }
            //        }

            //        int busIndex = busList[combination[i]].getIndexOfNearestBusDeparture(busStopList[actualBusStop], actualArriveTime, dayType);

            //        if (busIndex == -1)
            //        {
            //            return;
            //        }

            //        int[] departureTime = new int[2];
            //        busList[combination[i]].Route[busStopList[actualBusStop]].ifExists(dayType).TimeTableList[busIndex].CopyTo(departureTime, 0);

            //        //sprawdzamy, czy wywołujemy to dla pierwszego (startowego przystanku)

            //        if (i == 0)
            //            candidate.addRouteElement(generateRouteElement(busStopList[actualBusStop], actualArriveTime, departureTime, null, busList[combination[i]], change));
            //        else
            //        {
            //            if (!(combination[i - 1].Contains("link")))
            //                candidate.addRouteElement(generateRouteElement(busStopList[actualBusStop], actualArriveTime, departureTime, busList[combination[i - 1]], busList[combination[i]], change));
            //            else
            //                candidate.addRouteElement(generateRouteElement(busStopList[actualBusStop], actualArriveTime, departureTime, null, busList[combination[i]], change));
            //        }
            //        actualBusStop = busList[combination[i]].getNextBusStopLine(busStopList[actualBusStop]).BusStop.Id;

            //        while (actualBusStop.CompareTo(busStopList.Keys.ElementAt(array[i + 1])) != 0)
            //        {
            //            if (busList[combination[i]].Route[busStopList[actualBusStop]].ifExists(dayType).TimeTableList.Count - 1 < busIndex || departureTime[0] > busList[combination[i]].Route[busStopList[actualBusStop]].ifExists(dayType).TimeTableList[busIndex][0] || (departureTime[0] == busList[combination[i]].Route[busStopList[actualBusStop]].ifExists(dayType).TimeTableList[busIndex][0] && departureTime[1] >= busList[combination[i]].Route[busStopList[actualBusStop]].ifExists(dayType).TimeTableList[busIndex][1]))
            //            {
            //                busIndex = busList[combination[i]].getIndexOfNearestBusDeparture(busStopList[actualBusStop], departureTime, dayType);
            //            }

            //            if (busIndex == -1)
            //            {
            //                return;
            //            }

            //            busList[combination[i]].Route[busStopList[actualBusStop]].ifExists(dayType).TimeTableList[busIndex].CopyTo(actualArriveTime, 0);
            //            busList[combination[i]].Route[busStopList[actualBusStop]].ifExists(dayType).TimeTableList[busIndex].CopyTo(departureTime, 0);

            //            candidate.addRouteElement(generateRouteElement(busStopList[actualBusStop], actualArriveTime, departureTime, busList[combination[i]], busList[combination[i]], false));

            //            actualBusStop = busList[combination[i]].getNextBusStopLine(busStopList[actualBusStop]).BusStop.Id;
            //        }

            //        //dodajemy przystanek przesiadkowy lub przystanek końcowy

            //        if (busList[combination[i]].Route[busStopList[actualBusStop]].ifExists(dayType).TimeTableList.Count - 1 < busIndex || departureTime[0] > busList[combination[i]].Route[busStopList[actualBusStop]].ifExists(dayType).TimeTableList[busIndex][0] || (departureTime[0] == busList[combination[i]].Route[busStopList[actualBusStop]].ifExists(dayType).TimeTableList[busIndex][0] && departureTime[1] >= busList[combination[i]].Route[busStopList[actualBusStop]].ifExists(dayType).TimeTableList[busIndex][1]))
            //        {
            //            busIndex = busList[combination[i]].getIndexOfNearestBusDeparture(busStopList[actualBusStop], departureTime, dayType);
            //        }

            //        if (busIndex == -1)
            //        {
            //            return;
            //        }

            //        busList[combination[i]].Route[busStopList[actualBusStop]].ifExists(dayType).TimeTableList[busIndex].CopyTo(actualArriveTime, 0);

            //        if (busStopList.Keys.ElementAt(array[i + 1]).CompareTo(end.Id) == 0)
            //        {
            //            if (!(combination[i].Contains("link")))
            //                candidate.addRouteElement(generateRouteElement(busStopList[actualBusStop], actualArriveTime, null, busList[combination[i]], null, false));
            //            else
            //                candidate.addRouteElement(generateRouteElement(busStopList[actualBusStop], actualArriveTime, null, null, null, false));
            //        }
            //    }

            //    if (!candidate.ifLoopExists() && !population.ifRouteExists(candidate))
            //    {
            //        population.Candidates.Add(candidate);
            //        candidate.NumberOfChanges = combination.Count - 1;
            //    }
            //}
        }

        private List<List<BusLineIoDto>> GetAllCombinations(List<BusLineIoDto> connectingBusLines)
        {
            var subsets = CreateSubsets(connectingBusLines.ToArray());

            var result = new List<List<BusLineIoDto>>();

            foreach (var item in subsets)
            {
                if (item.Count() > 1)
                {
                    var permColl = permutations(item);

                    foreach (var perm in permColl)
                    {
                        result.Add(perm.ToList());
                    }
                }
                else
                {
                    result.Add(item.ToList());
                }
            }
            return result;
        }

        private PartOfTheRoute generateRouteElement(BusStop busStop, int[] timeIn, int[] timeOut, BusLineIoDto lineIn, BusLineIoDto lineOut, bool change)
        {
            return new PartOfTheRoute();

            /*
            RouteElement routeElement = new RouteElement();
            routeElement.BusStop = busStop;
            if (change)
            {
                timeIn[1] -= minChangeTime;

                if (timeIn[1] < 0)
                {
                    timeIn[1] += 60;
                    timeIn[0] -= 1;
                }
            }

            if (timeIn != null)
                timeIn.CopyTo(routeElement.TimeIn, 0);
            else
                routeElement.TimeIn = null;

            if (timeOut != null)
                timeOut.CopyTo(routeElement.TimeOut, 0);
            else
                routeElement.TimeOut = null;

            routeElement.LineIn = lineIn;
            routeElement.LineOut = lineOut;

            return routeElement;*/
        }

        /*
        static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list)
        {
            List<List<T>> result = new List<List<T>>();

            for (int i = 1; i < list.Count() - 1; i++)
            {
                var temp = GetCombinations(list, i);
               // result.AddRange();
            }

            return result;
        }

        static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetCombinations(list, length - 1)
                .SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }));
        }
         * */

        List<T[]> CreateSubsets<T>(T[] originalArray)
        {
            List<T[]> subsets = new List<T[]>();

            for (int i = 0; i < originalArray.Length; i++)
            {
                int subsetCount = subsets.Count;
                subsets.Add(new T[] { originalArray[i] });

                for (int j = 0; j < subsetCount; j++)
                {
                    T[] newSubset = new T[subsets[j].Length + 1];
                    subsets[j].CopyTo(newSubset, 0);
                    newSubset[newSubset.Length - 1] = originalArray[i];
                    subsets.Add(newSubset);
                }
            }

            return subsets;
        }

        ///// <summary>
        ///// Returns all permutations of the input <see cref="IEnumerable&lt;T&gt;"/>.
        ///// </summary>
        ///// <param name="source">The list of items to permute.</param>
        ///// <returns>A collection containing all permutations of the input <see cref="IEnumerable&lt;T&gt;"/>.</returns>
        //public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> source)
        //{
        //    if (source == null)
        //        throw new ArgumentNullException("source");
        //    // Ensure that the source IEnumerable is evaluated only once
        //    return permutations(source.ToArray());
        //}

        public static IEnumerable<IEnumerable<T>> permutations<T>(IEnumerable<T> source)
        {
            var c = source.Count();
            if (c == 1)
                yield return source;
            else
                for (int i = 0; i < c; i++)
                    foreach (var p in permutations(source.Take(i).Concat(source.Skip(i + 1))))
                        yield return source.Skip(i).Take(1).Concat(p);
        }


        /*
        // Returns an enumeration of enumerators, one for each permutation
        // of the input.
        public static IEnumerable<IEnumerable<T>> Permute<T>(IEnumerable<T> list, int count)
        {
            if (count == 0)
            {
                yield return new T[0];
            }
            else
            {
                int startingElementIndex = 0;
                foreach (T startingElement in list)
                {
                    IEnumerable<T> remainingItems = AllExcept(list, startingElementIndex);

                    foreach (IEnumerable<T> permutationOfRemainder in Permute(remainingItems, count - 1))
                    {
                        yield return Concat<T>(
                            new T[] { startingElement },
                            permutationOfRemainder);
                    }
                    startingElementIndex += 1;
                }
            }
        }
         * */

        // Enumerates over contents of both lists.
        public static IEnumerable<T> Concat<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            foreach (T item in a) { yield return item; }
            foreach (T item in b) { yield return item; }
        }

        // Enumerates over all items in the input, skipping over the item
        // with the specified offset.
        public static IEnumerable<T> AllExcept<T>(IEnumerable<T> input, int indexToSkip)
        {
            int index = 0;
            foreach (T item in input)
            {
                if (index != indexToSkip) yield return item;
                index += 1;
            }
        }

        #endregion
    }
}




/***** Moje starsze *
         //dorobić sprawdzanie linkow pieszych
        //metoda tworząca trasy, gdy znajdziemy listę przesiadek do przystanku poczatkowego
        private void addBusRoute(Stack<int> stack)
        {
            int[] array = stack.ToArray();

            //słownik: przystanek i lista linii do następnego przystanku
            // Dictionary<int, List<String>> lineListToNextBusStop = new Dictionary<int, List<String>>();

            int routeCount = 1;

            List<BusLineIoDto> connectingBusLines = new List<BusLineIoDto>();
            List<WalkLineChanges> possibleTransfers = new List<WalkLineChanges>();

    

            for (int i = 0; i < array.Length - 1; i++)
            {
                connectingBusLines.AddRange(BusLines.GetConnectedBusLines(array[i], array[i + 1], BusStopList));

                var walkLine = BusGraph.GetWalkLines(array[i], array[i + 1], BusStopList);

                if (walkLine != null)
                {
                    possibleTransfers.Add(walkLine);
                }

                // routeCount *= lines.Count;
            }

            //wygenerować wszystkie kombinacje linii
            //lista wszystkich kombinacji linii

            List<LineCombination> lineCombinations = new List<LineCombination>();

            var startLines = connectingBusLines.Where(x => x.FirstOrDefault().BusStopId == Start.Id);
            var restOfTheLines = connectingBusLines.Where(x => x.FirstOrDefault().BusStopId != Start.Id);

            foreach (var item in startLines)
            {
                
            }

 * ***/

