namespace Magisterka.Data.Access.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Result")]
    public partial class Result
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Result()
        {
            SingleResult = new HashSet<SingleResult>();
        }

        public int Id { get; set; }

        public int ParametersId { get; set; }

        public int StartStopPointsId { get; set; }

        public double GenerationTime { get; set; }

        public bool RouteExists { get; set; }

        public virtual Parameters Parameters { get; set; }

        public virtual StartStopPoints StartStopPoints { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SingleResult> SingleResult { get; set; }
    }
}
