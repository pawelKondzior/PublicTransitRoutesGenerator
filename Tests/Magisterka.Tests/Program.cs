using Magisterka.Tests.Paths;
using Magisterka.Tests.Theory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Tests
{
    /// <summary>
    /// Console program for performence analysys by VS 2017
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            var obj = TestDataGenerator.RealDataSpeedTest().FirstOrDefault().FirstOrDefault();// as BasePathTestSet;


            var data = (BasePathTestSet)obj;

            new MinimalDistanceGeneratorTest().RealSpeedTest(data);

            
        }
    }
}
