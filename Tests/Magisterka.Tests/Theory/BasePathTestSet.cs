using Magisterka.Infrastructure.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Tests.Theory
{
    public class BasePathTestSet
    {
        public int StartBusId { get; set; }

        public int EndBusId { get; set; }

        public LoadDataTypeEnum LoadDataTypeEnum  { get;set;}

        public int ChangeNumber { get; set; }

        public int LinkType { get; set; }
        
    }
}
