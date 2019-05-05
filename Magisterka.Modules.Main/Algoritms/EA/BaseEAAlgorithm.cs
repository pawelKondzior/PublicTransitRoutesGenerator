// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseEAAlgorithm.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The base ea algorithm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Security.Cryptography.X509Certificates;

namespace Magisterka.Modules.Main.Algoritms.EA
{
    using Anotar.Log4Net;
    using log4net;
    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.Extensions;
    using Magisterka.Infrastructure.Shared.Helpers;
    using Magisterka.Infrastructure.Shared.Structure;
    using Magisterka.Modules.Main.Algoritms.PopulationGenerators;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MoreLinq;
    using Magisterka.Infrastructure.Shared.Dto;

    /// <summary>
    ///     The base ea algorithm.
    /// </summary>
    public abstract class BaseEaAlgorithm : BaseAlgorithm
    {
        #region Static Fields

        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(BaseEaAlgorithm).Name);

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseEaAlgorithm" /> class.
        /// </summary>
        public BaseEaAlgorithm()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEaAlgorithm"/> class.
        /// </summary>
        /// <param name="algorithmParameters">
        /// The algorithm parameters.
        /// </param>
        public BaseEaAlgorithm(AlgorithmParameters algorithmParameters)
            : base(algorithmParameters)

        {
            BasicPopulationGenerators = new List<BasePopulationGenerator>();
            MutationPopulationGenerators = new List<BasePopulationGenerator>();
        }

        public BaseEaAlgorithm(AlgorithmParameters algorithmParameters, Population population)
            : base(algorithmParameters)
        {
            this.Population = population;
        }



        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the population generator.
        /// </summary>
        public List<BasePopulationGenerator> BasicPopulationGenerators { get; set; }

        public List<BasePopulationGenerator> MutationPopulationGenerators { get; set; }

        /// <summary>
        /// Punkt rozpoczecia mutacji
        /// </summary>
        public PartOfTheRoute MutationStartPoint { get; set; }

        /// <summary>
        /// Punkt zakończenia mutacji
        /// </summary>
        public PartOfTheRoute MutationEndPoint { get; set; }

        #endregion

        #region Public Methods and Operators


        public BasePopulationGenerator GetBasicPopulationGenerator()
        {
            return RandomSelectorHelper.GetRandomItem(BasicPopulationGenerators);
        }

        public BasePopulationGenerator GetMutationPopulationGenerator()
        {
            return RandomSelectorHelper.GetRandomItem(MutationPopulationGenerators);
        }


        public virtual EntireRoute BaseMutation(EntireRoute itemToMutate, PartOfTheRoute firstItem,
            PartOfTheRoute secondItem)
        {
            MutationStartPoint = firstItem;
            MutationEndPoint = secondItem;

            return BaseMutation(itemToMutate);
        }

        /// <summary>
        /// The base mutation.
        /// </summary>
        /// <param name="itemToMutate">
        /// The item to mutate.
        /// </param>
        /// <param name="firstItem">
        /// The first item.
        /// </param>
        /// <param name="secondItem">
        /// The second item.
        /// </param>
        /// <returns>
        /// The <see cref="EntireRoute"/>.
        /// </returns>
        public virtual EntireRoute BaseMutation(EntireRoute itemToMutate)
        {
            LogTo.Info("Mutation {0} {1}", MutationStartPoint.BusStop.Id, MutationEndPoint.BusStop.Id);

            //    var newRouteStartPoint = itemToMutate.GetNextPartOfTheRouteFor(MutationStartPoint);
            ///    var newRouteEndPoint = itemToMutate.GetPreviousPartOfTheRouteFor(MutationEndPoint);

            var mutationPopulationGenerator = GetMutationPopulationGenerator();

            mutationPopulationGenerator.AlgorithmParameters.Start = MutationStartPoint.BusStop;
            mutationPopulationGenerator.AlgorithmParameters.End = MutationEndPoint.BusStop;

            // ciekawe czy kiedys null :D? - tak
            var startItemArriveTime = new Time(MutationStartPoint.GetAnyCorrectTime());

            mutationPopulationGenerator.AlgorithmParameters.ArriveTime = startItemArriveTime;
            Population tempPopulation = mutationPopulationGenerator.Generate();

            LogTo.Info("Generator do mutacji {0}", mutationPopulationGenerator.GetType().Name);


            if (tempPopulation == null || !tempPopulation.Any())
            {
                LogTo.Warn("Nie znaleziono drogi łączącej {0}", mutationPopulationGenerator.GetType().Name);
                return null;
            }

            EntireRoute bestSubject = tempPopulation.GetBestSubject(AlgorithmParameters.AdaptationFunctionTypeEnum, startItemArriveTime);




            //   bestSubject.LogRoute("najlepszy osobnik ");

            var itemToMutateClone = (EntireRoute)itemToMutate.Clone();

            //itemToMutate.LogRoute();

            itemToMutateClone.LogInOneLine("Linia przed mutacja");

            ///  itemToMutateClone.RemoveBusStopsBeetwen(MutationStartPoint, MutationEndPoint);

            ///   itemToMutateClone.LogInOneLine("Po kasowaniu ze środka");

            itemToMutateClone.InsertRouteBeetwen(MutationStartPoint, MutationEndPoint, bestSubject);

            itemToMutateClone.LogInOneLine("Po dodaniu przystanków do środka");


            var timesCompleted = itemToMutateClone.CompleteWithTimes(AlgorithmParameters.ArriveTime, AlgorithmParameters.DayTypeEnum);

            if (timesCompleted)
            {
                itemToMutateClone.CalculateRouteTime();
                itemToMutateClone.CalculateTimeFromArrive(AlgorithmParameters.ArriveTime);

                itemToMutateClone.IsFromMutation = true;

                itemToMutateClone.BusMutatedList.Add(this.MutationStartPoint.BusStop);
                itemToMutateClone.BusMutatedList.Add(this.MutationEndPoint.BusStop);

                return itemToMutateClone;
            }

            else
            {
                return null;
            }
        }

        /// <summary>
        /// The evaluate.
        /// </summary>
        /// <returns>
        /// The <see cref="EntireRoute"/>.
        /// </returns>
        public EntireRoute Evaluate(EntireRoute itemToEA)
        {
            EntireRoute result = null;

            // chwilowo nie potrzbne
            //  this.Population.CalculateAdaptationFunction(AdaptationFunctionTypeEnum, this.ArriveTime);
            //   this.Population.CalculateEvaluationFunction();
            MutationOperatorTypeEnum operationType = RandomSelectorHelper.GetRandomOperator(AlgorithmParameters.MutationProbability);

            log.InfoFormat("typ operacji: {0}", operationType.ToString());

            if (operationType == MutationOperatorTypeEnum.Mutation)
            {
                result = this.Mutation(itemToEA);
            }
            else if (operationType == MutationOperatorTypeEnum.Hybridization)
            {
                result = this.Hybridization(itemToEA);
            }

            return result;
        }




        public virtual EntireRoute Hybridization(EntireRoute itemToEA)
        {
            LogTo.Debug("Hybridization");

            if (Population.Count < 2)
            {
                LogTo.Warn("nie można dokonać krzyzowania - jeden element w populacji");
                return null;
            }

            var randomItem = RandomSelectorHelper.GetRandomItem(Population);

            while (randomItem == itemToEA)
            {
                randomItem = RandomSelectorHelper.GetRandomItem(Population);
            }


            var itemsToHybridization = Population.FindHybrydyzationPoints(itemToEA, randomItem);


            var newRoute = PerformHybridization(itemsToHybridization);

            return newRoute;
        }



        /// <summary>
        ///     The hybridization.
        /// </summary>
        public virtual EntireRoute HybridizationRandom()
        {
            LogTo.Debug("Hybridization random");

            var findHybridizationItemsCount = 0;

            var itemsToHybridization = Population.GetItemsToHybrydization();

            var newRoute = PerformHybridization(itemsToHybridization);

            return newRoute;
        }

        public virtual EntireRoute PerformHybridization(List<HyrydizationItemDto> hybrydyzationPoints)
        {
            if (!hybrydyzationPoints.Any())
            {
                LogTo.Warn("Brak przystanków do krzyzzowania");
                return null;
            }

            var hyrydizationItemDto = hybrydyzationPoints.PickRandom();


            LogTo.Debug("Hybrydycacja dla {0}", hyrydizationItemDto.Item.Id);

            var result = new List<PartOfTheRoute>();


            var firstPart = hyrydizationItemDto.FirstRoute.PartOfTheRoute.TakeUntil(x => x.BusStop == hyrydizationItemDto.Item).ToList();
            var secondPart = hyrydizationItemDto.SecondRoute.PartOfTheRoute.SkipUntil(x => x.BusStop == hyrydizationItemDto.Item).ToList();


            var middleItemSecondPart = secondPart.FirstOrDefault();

            firstPart.ForEach(partOfRoute =>
            {
                var clonedItem = partOfRoute.Clone();

                if (partOfRoute.BusStop.Id == middleItemSecondPart.BusStop.Id)
                {
                    clonedItem.OutputLine = middleItemSecondPart.OutputLine;
                }
                result.Add(clonedItem);
            });

            secondPart.Skip(1).ForEach(partOfRoute =>
            {
                var clonedItem = partOfRoute.Clone();

                result.Add(clonedItem);
            });

            var entireRoute = new EntireRoute(result);
            var timesCompleted = entireRoute.CompleteWithTimes(AlgorithmParameters.ArriveTime, AlgorithmParameters.DayTypeEnum);

            if (timesCompleted)
            {
                entireRoute.CalculateRouteTime();
                entireRoute.CalculateTimeFromArrive(AlgorithmParameters.ArriveTime);
                entireRoute.IsFromHybrydyzaion = true;

                return entireRoute;
            }
            else
            {
                return null;
            }
        }

        public virtual void Hybridization(EntireRoute firstRoute, EntireRoute secondRoute,
            List<int> hybridizationBusStops)
        {
            var hybridizationStop = RandomSelectorHelper.GetRandomItem(hybridizationBusStops);



        }


        /// <summary>
        /// The population summary.
        /// </summary>
        /// <returns>
        /// The <see cref="Population"/>.
        /// </returns>
        public Population PopulationSummary()
        {
            this.Population.ForEach(x => x.CalculateAdaptationFunction(AdaptationFunctionTypeEnum.ByTime));

            this.Population.CalculateEvaluationFunction();

            return this.Population;
        }

        public virtual EntireRoute Mutation(EntireRoute itemToEA)
        {
            LogTo.Debug("Mutation");

            return MutationBase(itemToEA);
        }


        /// <summary>
        /// The mutation.
        /// </summary>
        /// <returns>
        /// The <see cref="EntireRoute"/>.
        /// </returns>
        public virtual EntireRoute MutationRandom()
        {
            LogTo.Debug("RandomMutation");

            EntireRoute itemToMutate = RandomSelectorHelper.GetRandomItem(this.Population);
            itemToMutate.NumberOfSelectingForMutation++;

            return MutationBase(itemToMutate);
        }

        private EntireRoute MutationBase(EntireRoute itemToMutate)
        {
            if (itemToMutate.PartOfTheRoute.Count <= 2)
            {
                log.Info("Trasa ma mniej niż 2 elementy - nie nadaje się do mutacji");

                return null;
            }

            log.Info("Losowanie elementow");
            Tuple<PartOfTheRoute, PartOfTheRoute> items =
                RandomSelectorHelper.GetTwoDifrentRandomItems(itemToMutate.PartOfTheRoute);

            items.Item1.Log(log, "pierwszy element");
            items.Item1.NumberOfSelectingForMutation++;

            items.Item2.Log(log, "drugi element");
            items.Item2.NumberOfSelectingForMutation++;

            EntireRoute newItem = this.BaseMutation(itemToMutate, items.Item1, items.Item2);

            return newItem;
        }

        /// <summary>
        /// The single evaluate.
        /// </summary>
        /// <returns>
        /// The <see cref="Population"/>.
        /// </returns>
        public virtual Population SingleEvaluate()
        {
            //this.Evaluate();

            return this.PopulationSummary();
        }

        /// <summary>
        /// The start algoritm.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public virtual Population StartAlgoritm(Population population = null)
        {
            if (population == null)
            {
                LogTo.Info("Rozpoczyna generacje populacji");


                this.Population = new Population();/// GetBasicPopulationGenerator().Generate();

                BasicPopulationGenerators.ForEach(generator => Population.AddRange(generator.Generate()));

                Population = Finess(Population);

                LogTo.Info("Zakończył generacej populacji");
            }
            else
            {
                this.Population = population;
            }

            var newPopulation = new Population();
            newPopulation.AddRange(Population);


            for (int i = 0; i < AlgorithmParameters.NumberOfEvaluation; i++)
            {
                foreach (var item in newPopulation.ToList())
                {
                    try
                    {
                        EntireRoute result = this.Evaluate(item);

                        if (result != null)
                        {
                            result.Generation = i;
                            newPopulation.Add(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogTo.Error("foreach evaluate", ex);
                    }
                }

                newPopulation = Finess(newPopulation);
            }

            return newPopulation;
        }

        public Population Finess(Population oldPopulation)
        {
            oldPopulation.ForEach(x => x.CalculateBusStopConnections());

            oldPopulation.CalculateAdaptationFunction(AlgorithmParameters.AdaptationFunctionTypeEnum, AlgorithmParameters.ArriveTime);


            var newItems = oldPopulation.OrderBy(x => x.AdaptationFunctionResult).Take(AlgorithmParameters.PopulationCount);

            return new Population(newItems);
        }

        public virtual void FindItemsToMutate(EntireRoute itemToMutate)
        {
            return;
        }


        #endregion
    }
}