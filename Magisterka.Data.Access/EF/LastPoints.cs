namespace Magisterka.Data.Access.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [PetaPoco.TableName("LastPoints")]
    [PetaPoco.PrimaryKey("Id")]
    public partial class LastPoints
    {
        public int Id { get; set; }

        public int StartPointId { get; set; }

        public int EndPointId { get; set; }

        public string DisplayValue
        {
            get
            {
                return string.Format("{0} {1}", StartPointId, EndPointId);
            }
        }
    }
}
