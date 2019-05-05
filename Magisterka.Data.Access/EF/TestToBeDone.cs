namespace Magisterka.Data.Access.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public partial class TestToBeDone
    {
      
        public int Id { get; set; }

 
        public int TestAlgorithmTypeEnum { get; set; }

      
        public int AdaptationFunctionTypeEnum { get; set; }

        public int ChangeNumber { get; set; }

        public int LinkType { get; set; }

        public int PopulationCount { get; set; }

        public double? MutationProbability { get; set; }

        public int? NumberOfEvaluation { get; set; }

        public int? NumberOfSquares { get; set; }

        public int? NumberOfNeighborSquares { get; set; }

      
        public int StartStopPointsId { get; set; }

       
        public int StartId { get; set; }

      
        public int StopId { get; set; }

        public int? ResultId { get; set; }

        public int? ParametersId { get; set; }

        public int? StartStopPointsId { get; set; }

        public int? GenerationTime { get; set; }
    }
}
