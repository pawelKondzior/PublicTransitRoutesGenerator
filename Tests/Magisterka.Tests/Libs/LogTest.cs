using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Net;
using Castle.Components.DictionaryAdapter;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Generic;
using Magisterka.Infrastructure.Shared.IoDto;
using Magisterka.Infrastructure.Shared.Structure;
using Moq;
using Xunit;
using Magisterka.Modules.Main.Matrix;
using Magisterka.Infrastructure.Shared.Extensions;
using Magisterka.Infrastructure.Shared.Helpers;
using Magisterka.Infrastructure.Shared.TestData;
using MoreLinq;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;
using Anotar.Log4Net;

namespace Magisterka.Tests.Libs
{
    /// <summary>
    /// 
    /// Dane z
    /// See discussions, stats, and author profiles for this publication at: https://www.researchgate.net/publication/265246529
    /// dr Approximation Method to Route Generation in
    /// Public Transportation Network
    /// </summary>
    public class LogTest
    {

        

        public LogTest()
        {
                    
        }

        [Fact]
        public void TestAnotarLog()
        {
            LogTo.Info("Test logs");
        }



      
    }
}

