using Anotar.Log4Net;
using Magisterka.Infrastructure.Shared.Enum;
using Magisterka.Infrastructure.Shared.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Modules.Main.Algoritms.Matrix
{
    public  class NewQMatrixGenerator : BaseMatrixGenerator
    {
        //   private Stopwatch timer = new Stopwatch();

        //  protected bool TimeWarning = false;
      

        public NewQMatrixGenerator(AlgorithmParameters baseAlgorithmParameters) // :this()
            : base(baseAlgorithmParameters)
        {
            Matrix = AlgorithmParameters.Q;

            MaxGenerationCount = AlgorithmParameters.MaxComputetChangeStackResults;
        }

       
    }
}
