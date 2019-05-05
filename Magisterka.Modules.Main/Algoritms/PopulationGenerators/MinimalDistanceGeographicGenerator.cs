
using System.Collections.Generic;
using System.Linq;
using Magisterka.Infrastructure.Shared.Extensions;
using Magisterka.Infrastructure.Shared.Structure;
using Magisterka.Infrastructure.Shared.Collections;
namespace Magisterka.Modules.Main.Algoritms.PopulationGenerators
{
    public class MinimalDistanceGeographicGenerator : MinimalDistanceMatrixGenerator
    {
        #region Prpoerties

       // private Stack<BusStop> BusStopStack { get; set; }

        #endregion

        #region Construcotrs
        //public MinimalDistanceGeographicGenerator()
        //{  

        //}

        public MinimalDistanceGeographicGenerator(AlgorithmParameters baseAlgorithmParameters, SquareList squareList)
            : base(baseAlgorithmParameters)
        {
            var points = squareList.GetBusStopsLocatedIn(AlgorithmParameters.BusStopList);
            AlgorithmParameters.BusStopList = points;

//
            
            //   WindowsFormsChart.DrowBusStopList(points); 
        }

        #endregion 


   

    }
}