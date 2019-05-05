namespace Magisterka.Data.Access.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Parameters
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Parameters()
        {
            Result = new HashSet<Result>();
        }

        public int Id { get; set; }

        public int TestAlgorithmTypeEnum { get; set; }

        public int AdaptationFunctionTypeEnum { get; set; }

        public int ChangeNumber { get; set; }

     //   public int DayTypeEnum { get; set; }

        public int LinkType { get; set; }

        public int PopulationCount { get; set; }

        public double? MutationProbability { get; set; }

        public int? NumberOfEvaluation { get; set; }

       

        public int? NumberOfSquares { get; set; }

        public int? NumberOfNeighborSquares { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Result { get; set; }
    }
}
