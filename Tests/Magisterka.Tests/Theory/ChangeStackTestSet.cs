using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Tests.Theory
{
    public class ChangeStackTestSet : BasePathTestSet
    {
        public ChangeStackTestSet()
        {
            // ChangeStocks = new List<List<int>>();
        }

        public IEnumerable<List<int>> ChangeStocks { get; set; }
    }
}
