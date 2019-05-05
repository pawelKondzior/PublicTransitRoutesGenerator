namespace Magisterka.Data.Access.EF.test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LastPoints
    {
        public int Id { get; set; }

        public int StartPointId { get; set; }

        public int EndPointId { get; set; }
    }
}
