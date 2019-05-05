using Magisterka.Infrastructure.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Magisterka.Infrastructure.Shared.Parameters
{
    public static class ParametersRanges
    {




        public static TestAlgorithmTypeEnum[] PopulationGenerators = new TestAlgorithmTypeEnum[]
            {
                TestAlgorithmTypeEnum.ArrayGenerator,
                TestAlgorithmTypeEnum.MinimalDistanceMatrixGenerator
            };

        public static TestAlgorithmTypeEnum[] PopulationGeoGenerators = new TestAlgorithmTypeEnum[]
           {
                TestAlgorithmTypeEnum.ArrayGeographicGenerator,
                TestAlgorithmTypeEnum.MinimalDistanceGeographicGenerator,
           };



        public static TestAlgorithmTypeEnum[] EAGenerators = new TestAlgorithmTypeEnum[]
                   {
                      TestAlgorithmTypeEnum.PopulationWithFewestBusChange,
                      TestAlgorithmTypeEnum.PopulationWithMinimalDistanceMatrixGenerator,
                      TestAlgorithmTypeEnum.Mixed
                   };


        public static TestAlgorithmTypeEnum[] EAGeoGenerators = new TestAlgorithmTypeEnum[]
           {
                      TestAlgorithmTypeEnum.PopulationWithFewestBusChangeGeo,
                      TestAlgorithmTypeEnum.PopulationWithMinimalDistanceMatrixGeneratorGeo,
                      TestAlgorithmTypeEnum.MixedGeo
           };

        ///public static TestAlgorithmTypeEnum[] TestAlgorithmTypeEnum = Extensions.EnumExt.GetEnumValuesList<TestAlgorithmTypeEnum>();


        public static List<AdaptationFunctionTypeEnum> AdaptationFunctionTypeEnum = Extensions.EnumExt.GetEnumValuesList<AdaptationFunctionTypeEnum>();

        /// public AlgoritmTypeEnum AlgoritmTypeEnum { get; set; }


        public static Magisterka.Infrastructure.Shared.Structure.Time ArriveTime = new Structure.Time(8, 00);



        //public static int[] ChangeNumber = new int[] { 1, 2, 3, 4, 5 };
        public static int[] ChangeNumber = new int[] { 1, 3, 5 };


        public static DayTypeEnum DayTypeEnum = DayTypeEnum.WorkingDay;


        public static int[] LinkType = new int[] { 0, 1, 3 };

        public static double[] MutationProbability = new double[] { 0, 0.5, 1.0 };

        public static int[] NumberOfEvaluation = new int[] { 5, 10 };

        public static int[] PopulationCount = new int[] { 10, 20 };

        /// <summary>
        /// Ilość kwadratów na jakie będzie dzielona przestrzeń
        /// </summary>
        public static int[] NumberOfSquares = new int[] {  5, 10 };

        /// <summary>
        /// Ilość sąsiednich kwadratów jaka będzie brana pod uwagę
        /// </summary>
        public static int[] NumberOfNeighborSquares = new int[] { 0, 3 };


        ///// <summary>
        ///// Maksymalna liczba osobników poplulajci generowanej w pierwszym kroku algorytmów tablicowych array
        ///// </summary>
        //public int MaxTableGenerationPopulationCountArray { get; set; }

        ///// <summary>
        ///// Maksymalna liczba osobników poplulajci generowanej w pierwszym kroku algorytmów tablicowych Minimal distance
        ///// </summary>
        //public int MaxTableGenerationPopulationCountMDG { get; set; }



        /// <summary>
        /// Gets or sets the population percent.
        /// </summary>
        /// public double PopulationPercent { get; set; }








        ///// <summary>
        ///// Ilość procentowa opisująca, jaka ilość trasy podczas wyszukiwania przystanków,
        /////  będzie użyta do geograficznego ustalenia przystanków mutacji
        ///// Inaczej prawie zawsze będzie to pierwszy i ostatni...
        ///// </summary>
        //public float MaxBusStopsBetweenGeograhMutationProcent { get; set; }


        /// public int MaxComputetChangeStackResults { get; set; }

        ///  public LoadDataTypeEnum LoadDataTypeEnum { get; set; }



        /// <summary>
        /// Max ilosć poszukiwań tras do krzyzowaia
        /// </summary>
        //  public int MaxFindHybridizationItemsCount { get; set; }

        //public int MaxWalkTime
        //{
        //    get { return LinkType * 5; }
        //}
    }
}
