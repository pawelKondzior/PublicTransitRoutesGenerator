

namespace Magisterka.Infrastructure.Shared.Dto
{
    using System.Collections.Generic;
    using Magisterka.Infrastructure.Shared.Basic;

    public class SplitLinesResult
    {
        public List<SinglePoint> XAxisSplit { get; set; }

        public List<SinglePoint> YAxisSplit { get; set; }
    }
}