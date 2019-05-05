// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicEaAlgorithm.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   Podstawowy algorytm genetyczny
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Magisterka.Infrastructure.Shared.Enum;

namespace Magisterka.Modules.Main.Algoritms.EA
{
    using Magisterka.Infrastructure.Shared.Collections;
    using Magisterka.Infrastructure.Shared.Helpers;
    using Magisterka.Infrastructure.Shared.Structure;
    using Magisterka.Modules.Main.Algoritms.PopulationGenerators;
    using Magisterka.Modules.Main.Builders;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Algorytm genetyczny wyszukujący przystaniki do mutacji za pomocą odległości
    /// </summary>
    public class GeoMutationEaAlgorithm : BaseEaAlgorithm
    {
        private SquareList SquareList { get; set; }

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicEaAlgorithm"/> class.
        /// </summary>
        /// <param name="baseAlgorithmParameters">
        /// The base algorithm parameters.
        /// </param>
        //public GeoMutationEaAlgorithm(AlgorithmParameters baseAlgorithmParameters, BasePopulationGenerator populationGenerator)
        //    : base(baseAlgorithmParameters)
        //{
        //    this.PopulationGenerator = populationGenerator;
        //}

        public GeoMutationEaAlgorithm(AlgorithmParameters baseAlgorithmParameters, SquareList squareList)
            : base(baseAlgorithmParameters)
        {
            SquareList = squareList;

            // defoult population generator
            PopulationGeneratorBuilder basicPopulationGeneratorBuilder = new PopulationGeneratorBuilder(AlgorithmParameters,
              PopulationGeneratorTypeEnum.ArrayGeographicGenerator, squareList);

            var mutationParameters = AlgorithmParameters.CloneRequired();
            mutationParameters.MaxComputetChangeStackResults = 3;
            var mutationPopulationGenerator = new PopulationGeneratorBuilder(mutationParameters, PopulationGeneratorTypeEnum.ArrayGeographicGenerator, squareList);

            BasicPopulationGenerators.Add(basicPopulationGeneratorBuilder.GetPopulationGenerator());
            MutationPopulationGenerators.Add(mutationPopulationGenerator.GetPopulationGenerator());
        }


        public override EntireRoute BaseMutation(EntireRoute itemToMutate)
        {
            this.FindItemsToMutate(itemToMutate);

            var squareHelper = new SquareListHelper(SquareList);
            var selectedSquares = squareHelper.GetSelectedSquareList(this.MutationStartPoint.BusStop,
                this.MutationEndPoint.BusStop, AlgorithmParameters.NumberOfNeighborSquares);

           

            return base.BaseMutation(itemToMutate);
        }

        public override EntireRoute BaseMutation(EntireRoute itemToMutate, PartOfTheRoute firstItem, PartOfTheRoute secondItem)
        {
            return this.BaseMutation(itemToMutate);
        }

        /// <summary>
        /// Z tego co pamiętam
        /// Znajduje najlepsze punkty do przeprowadzenia na nich mutacji 
        /// </summary>
        /// <param name="itemToMutate"></param>
        public override void FindItemsToMutate(EntireRoute itemToMutate)
        {
            var list = new List<PartoOfTheRouteDistances>();

            var tempRouteParts = itemToMutate.PartOfTheRoute.ToList();
            var allStopsCount = itemToMutate.PartOfTheRoute.Count;
            int stopsMaxDistance = (int)((allStopsCount * AlgorithmParameters.MaxBusStopsBetweenGeograhMutationProcent) / 100f);

            foreach (var item in itemToMutate.PartOfTheRoute)
            {
                tempRouteParts.Remove(item);

                list.Add(new PartoOfTheRouteDistances(item, tempRouteParts, stopsMaxDistance));
            }

            var bestItems = list.Where(x => x.ItemWithBestRatio != null)
                .Select(x => x.ItemWithBestRatio)
                .ToList();

            var bestRatioItem = bestItems
                    .OrderByDescending(x => x.DisanceToStopsCountRatio)
                    .FirstOrDefault();

            this.MutationStartPoint = bestRatioItem.StartPartOfTheRoute;
            this.MutationEndPoint = bestRatioItem.EndPartOfTheRoute;
        }



        #endregion
    }
}