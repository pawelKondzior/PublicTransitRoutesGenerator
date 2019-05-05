namespace Magisterka.Data.Access.PP
{
    using PetaPoco;
    using System;
    


    public partial class LastPoint : DBContextDB.Record<LastPoint>
    {
        [Ignore]
        public string FriendlyNameStart { get; set; }

        [Ignore]
        public string FriendlyNameEnd { get; set; }


        [Ignore]
        public string DisplayValue
        {
            get
            {
                return string.Format("{0} {1} - {2} {3} ", StartPointId, FriendlyNameStart, EndPointId, FriendlyNameEnd);
            }
        }
    }
}
