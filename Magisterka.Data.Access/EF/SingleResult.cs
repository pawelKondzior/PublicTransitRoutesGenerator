namespace Magisterka.Data.Access.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SingleResult")]
    public partial class SingleResult
    {
       // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int ResultId { get; set; }

        public int Fitness { get; set; }

        public int TimeFromStart { get; set; }

        public int Time { get; set; }

        public int Parts { get; set; }

        public virtual Result Result { get; set; }
    }
}
