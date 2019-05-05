using Magisterka.Infrastructure.Shared.Enum;
using System.Collections.Generic;

namespace Magisterka.Tests.Theory
{
    public class TestDataGenerator //: IEnumerable<object[]>
    {
        //public IEnumerator<object[]> GetEnumerator() => GetChangeStackTestSet().GetEnumerator();

        //IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static IEnumerable<object[]> GetMinimalDistanceTestSet()
        {
            yield return new object[]
            {
                new BasePathTestSet
                {
                    LoadDataTypeEnum = LoadDataTypeEnum.MyBook,
                    StartBusId = 10,
                    EndBusId = 9,
                    ChangeNumber = 3,
                    LinkType = 1
                }
            };
        }


        public static IEnumerable<object[]> RealDataSpeedTest()
        {
            yield return new object[]
            {
                new BasePathTestSet
                {
                    LoadDataTypeEnum = LoadDataTypeEnum.RealExamples,
                    StartBusId = 18705,
                    EndBusId = 25131,
                    ChangeNumber = 3,
                    LinkType = 1
                }
            };
        }


        public static IEnumerable<object[]> ProperChangeStackLengthTest()
        {
            yield return new object[]
            {
                new BasePathTestSet
                {
                    LoadDataTypeEnum = LoadDataTypeEnum.RealExamples,
                    StartBusId = 10271,
                    EndBusId = 12107,
                    ChangeNumber = 5,
                    LinkType = 3
                }
            };
        }



        public static IEnumerable<object[]> GetChangeStackTestSet()
        {
            //yield return new object[]
            //{
            //    new ChangeStackTestSet
            //    {
            //        StartBusId = 1,
            //        EndBusId = 6,
            //        ChangeStocks = new List<List<int>>{
            //            new List<int> { 6, 5, 1 },
            //            new List<int> { 6, 9, 1 },
            //            new List<int> { 6, 11, 1 }
            //        }
            //    }

            //};

            yield return new object[]
          {
                new ChangeStackTestSet
                {
                    LoadDataTypeEnum = LoadDataTypeEnum.MyBook,
                    StartBusId = 10,
                    EndBusId = 9,
                    ChangeNumber = 3,
                    LinkType = 1,
                    ChangeStocks = new List<List<int>>{
                        new List<int> {9, 1, 10},
                        new List<int> {9, 4, 10},
                        new List<int> {9, 5, 1, 10},
                        new List<int> {9, 5, 4, 10},
                        new List<int> {9, 5, 6, 10},
                        new List<int> {9, 5, 7, 10},
                        new List<int> {9, 5, 11, 1, 10},
                        new List<int> {9, 7, 10},
                        new List<int> {9, 11, 1, 10},
                        new List<int> {9, 11, 2, 1, 10},
                        new List<int> {9, 11, 3, 1, 10},
                        new List<int> {9, 11, 12, 1, 10 }
                    }
                }
          };

            //yield return new object[]
            //{
            //    new ChangeStackTestSet
            //    {
            //        LoadDataTypeEnum = LoadDataTypeEnum.MyBook,
            //        StartBusId = 10,
            //        EndBusId = 9,
            //        ChangeNumber = 1,
            //        LinkType = 0,
            //        ChangeStocks = new List<List<int>>{
            //            new List<int> { 9, 1, 10},
            //            new List<int> { 9, 4, 10},
            //            new List<int> { 9, 7, 10 }
            //        }
            //    }
            //};
        }
    }
}