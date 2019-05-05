// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlgorithmParameters.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The algorithm parameters.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Structure
{
    using System.Linq;

    using Magisterka.Infrastructure.Shared.Collections;
    using Magisterka.Infrastructure.Shared.Enum;
    using AutoMapper;
    

    /// <summary>
    /// The algorithm parameters.
    /// </summary>
    public class AlgorithmParameters
    {
        
        #region Public Properties


        /// <summary>
        /// Gets or sets the adaptation function type enum.
        /// </summary>
        public AdaptationFunctionTypeEnum AdaptationFunctionTypeEnum { get; set; }

        /// <summary>
        /// Gets or sets the algoritm type enum.
        /// </summary>
        public AlgoritmTypeEnum AlgoritmTypeEnum { get; set; }

        /// <summary>
        /// Gets or sets the arrive time.
        /// </summary>
        public Time ArriveTime { get; set; }

        /// <summary>
        /// Gets or sets the bus graph.
        /// </summary>
        public BusGraph BusGraph { get; set; }

        /// <summary>
        /// Gets or sets the bus lines.
        /// </summary>
        public BusLinesList BusLines { get; set; }

        /// <summary>
        /// Gets or sets the bus stop list.
        /// </summary>
        public BusStopList BusStopList { get; set; }

        /// <summary>
        /// Gets or sets the change number.
        /// </summary>
        public int ChangeNumber { get; set; }

        /// <summary>
        /// Gets or sets the d.
        /// </summary>
        public int[,] D { get; set; }

        // public int[] ArriveTime { get; set; }

        // public DayTypeEnum DayType { get; set; }
        /// <summary>
        /// Gets or sets the day type enum.
        /// </summary>
        public DayTypeEnum DayTypeEnum { get; set; }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        public BusStop End { get; set; }

        /// <summary>
        /// Gets or sets the link type.
        /// </summary>
        public int LinkType { get; set; }

        public bool AllowWalkLinks {
            get
            {
                if (LinkType > 0)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets or sets the min change time.
        /// </summary>
        public int MinChangeTime { get; set; }

        /// <summary>
        /// Gets or sets the mutation probability.
        /// </summary>
        public double MutationProbability { get; set; }

        /// <summary>
        /// Gets or sets the number of evaluation.
        /// </summary>
        public int NumberOfEvaluation { get; set; }

        /// <summary>
        /// Gets or sets the pop count.
        /// </summary>
        public int PopulationCount { get; set; }


        /// <summary>
        /// Maksymalna liczba osobników poplulajci generowanej w pierwszym kroku algorytmów tablicowych array
        /// </summary>
        public int MaxTableGenerationPopulationCountArray { get; set; }

        /// <summary>
        /// Maksymalna liczba osobników poplulajci generowanej w pierwszym kroku algorytmów tablicowych Minimal distance
        /// </summary>
        public int MaxTableGenerationPopulationCountMDG { get; set; }

        /// <summary>
        /// Gets or sets the population.
        /// </summary>
        public Population Population { get; set; }

        /// <summary>
        /// Gets or sets the population percent.
        /// </summary>
        public double PopulationPercent { get; set; }

        /// <summary>
        /// Gets or sets the q.
        /// </summary>
        public int[,] Q { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        public BusStop Start { get; set; }

        /// <summary>
        /// Ilość kwadratów na jakie będzie dzielona przestrzeń
        /// </summary>
        public int NumberOfSquares { get; set; }

        /// <summary>
        /// Ilość sąsiednich kwadratów jaka będzie brana pod uwagę
        /// </summary>
        public int NumberOfNeighborSquares { get; set; }

        /// <summary>
        /// Ilość procentowa opisująca, jaka ilość trasy podczas wyszukiwania przystanków,
        ///  będzie użyta do geograficznego ustalenia przystanków mutacji
        /// Inaczej prawie zawsze będzie to pierwszy i ostatni...
        /// </summary>
        public float MaxBusStopsBetweenGeograhMutationProcent { get; set; }


        public int MaxComputetChangeStackResults { get; set; }

        public LoadDataTypeEnum LoadDataTypeEnum { get; set; }



        /// <summary>
        /// Max ilosć poszukiwań tras do krzyzowaia
        /// </summary>
        public int MaxFindHybridizationItemsCount { get; set; }

        public int MaxWalkTime
        {
            get { return LinkType*5; }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The set default values.
        /// </summary>
        public void SetDefaultValues()
        {
            this.DayTypeEnum = DayTypeEnum.WorkingDay;

            this.MinChangeTime = 3;

            this.ArriveTime = new Time(8, 0); // new int[2] {8, 0};

            this.PopulationCount = 20;
            this.MaxTableGenerationPopulationCountMDG = 500;

            this.ChangeNumber = 5;// 2; // 3;// 5; //  2;// 5;
            this.PopulationPercent = 1; // 50.0 / 100.0;
            this.NumberOfEvaluation = 10;
            this.AlgoritmTypeEnum = AlgoritmTypeEnum.First;
            this.LinkType = 3;
            this.MutationProbability = 50d;
            this.NumberOfSquares = 5;
            this.NumberOfNeighborSquares = 3;
            this.MaxBusStopsBetweenGeograhMutationProcent = 40f;

            this.AdaptationFunctionTypeEnum = AdaptationFunctionTypeEnum.ByTime;

            //this.MaxComputetChangeStackResults = 5;
            this.MaxComputetChangeStackResults = PopulationCount; // 5000;/// PopulationCount ;

            this.MaxFindHybridizationItemsCount = 5;

            this.LoadDataTypeEnum = LoadDataTypeEnum.RealExamples;
        }

        #endregion

        /// <summary>
        /// Zwraca kopie, klonują te wartości które powinny zostać skolonowane
        /// </summary>
        /// <returns>Częsciowo sklonowany obiekt</returns>
        public AlgorithmParameters CloneRequired()
        {
            var result = new AlgorithmParameters()
                {
                    AdaptationFunctionTypeEnum = this.AdaptationFunctionTypeEnum,
                    ArriveTime = this.ArriveTime,
                    AlgoritmTypeEnum = this.AlgoritmTypeEnum,
                    BusGraph = this.BusGraph,
                    BusLines = this.BusLines,
                    BusStopList = this.BusStopList,
                    ChangeNumber = this.ChangeNumber,
                    D = this.D,
                    DayTypeEnum = this.DayTypeEnum,
                    End = this.End,
                    LinkType = this.LinkType,
                    MinChangeTime = this.MinChangeTime,
                    MutationProbability = this.MutationProbability,
                    NumberOfEvaluation = this.NumberOfEvaluation,
                    PopulationCount = this.PopulationCount,
                MaxTableGenerationPopulationCountArray = this.MaxTableGenerationPopulationCountArray,
                MaxTableGenerationPopulationCountMDG = this.MaxTableGenerationPopulationCountMDG,
            // Population = this.Population,
                    PopulationPercent = this.PopulationPercent,
                    Q = this.Q,
                    Start = this.Start,
                    NumberOfSquares = this.NumberOfSquares,
                    NumberOfNeighborSquares = this.NumberOfNeighborSquares,
                    MaxBusStopsBetweenGeograhMutationProcent = this.MaxBusStopsBetweenGeograhMutationProcent,
                    MaxComputetChangeStackResults = this.MaxComputetChangeStackResults,
                    MaxFindHybridizationItemsCount =  this.MaxFindHybridizationItemsCount,
                    LoadDataTypeEnum = this.LoadDataTypeEnum
            };

            if (this.Population != null)
            {
                result.Population = new Population(this.Population);//.ToList();
            }
            
            return result;
        }


        public bool ValidateParameters()
        {
            //this.MinChangeTime = 3;

            if (PopulationCount < 1)
            {
                return false;
            }

            if (MaxTableGenerationPopulationCountMDG < 1)
            {
                return false;
            }

            if (ChangeNumber < 1 || ChangeNumber > 10)
            {
                return false;
            }
                
            if (PopulationPercent < 1 || PopulationPercent > 100)
            {
                return false;
            }

            if (NumberOfEvaluation < 1)
            {
                return false;
            }

            if (PopulationPercent < 1 || PopulationPercent > 100)
            {
                return false;
            }

            if (PopulationPercent < 1 || PopulationPercent > 100)
            {
                return false;
            }


            if (LinkType < 0 || LinkType > 3)
            {
                return false;
            }

            if (NumberOfSquares < 0 || NumberOfSquares > 100)
            {
                return false;
            }

            if (NumberOfNeighborSquares < 0 || NumberOfNeighborSquares > 100)
            {
                return false;
            }



            return true;

            //    this.LinkType = 3;
            //this.MutationProbability = 50d;
            //this.NumberOfSquares = 5;
            //this.NumberOfNeighborSquares = 3;
            //this.MaxBusStopsBetweenGeograhMutationProcent = 40f;

            //this.AdaptationFunctionTypeEnum = AdaptationFunctionTypeEnum.ByTime;

            ////this.MaxComputetChangeStackResults = 5;
            //this.MaxComputetChangeStackResults = PopulationCount; // 5000;/// PopulationCount ;

            //this.MaxFindHybridizationItemsCount = 5;

            //this.LoadDataTypeEnum = LoadDataTypeEnum.RealExamples;
        }
    }
}