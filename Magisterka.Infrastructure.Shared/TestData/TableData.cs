using System.Collections.Generic;
using Magisterka.Infrastructure.Shared.Collections;
using Magisterka.Infrastructure.Shared.Generic;
using Magisterka.Infrastructure.Shared.IoDto;
using Magisterka.Infrastructure.Shared.Structure;
using Moq;
using MoreLinq;
using System.Linq;

namespace Magisterka.Infrastructure.Shared.TestData
{
    public static class TableData
    {

        public static int maxDefaultdefaultDistance = 9999;


        public static int[,] GetMyBookTransferMatixData()
        {
            int[,] tMatrix = new int[12, 12] {
                {0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1},
                {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
                {1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0},
                {1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0},
                {1, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0},
                {1, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0},
                {1, 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0},
                {0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0}

            };


            return tMatrix;
        }


        public static int[,] GetMyBookQMatixData()
        {
            int[,] matrix = new int[12, 12] {
                {0, 1, 1, 1, 1, 2, 1, 0, 1, 0, 1, 1},
                {3, 0, 1, 3, 2, 2, 3, 0, 2, 0, 1, 1},
                {3, 4, 0, 3, 2, 2, 3, 0, 2, 0, 1, 1},
                {1, 2, 2, 0, 1, 2, 1, 0, 1, 0, 2, 2},
                {2, 3, 3, 2, 0, 1, 2, 0, 1, 0, 3, 3},
                {1, 2, 2, 1, 2, 0, 1, 0, 2, 0, 2, 2},
                {1, 2, 2, 1, 1, 2, 0, 0, 1, 0, 2, 2},
                {1, 2, 2, 1, 2, 1, 1, 0, 2, 0, 2, 2},
                {2, 3, 3, 2, 1, 1, 2, 0, 0, 0, 3, 3},
                {1, 2, 2, 1, 2, 1, 1, 1, 2, 0, 2, 2},
                {2, 3, 3, 2, 1, 1, 2, 0, 1, 0, 0, 3},
                {3, 4, 4, 3, 2, 2, 3, 0, 2, 0, 1, 0}

            };


            return matrix;
        }

        public static int[,] GetMyBookDMatixData()
        {
            int[,] matrix = new int[12, 12] {

        {0, 1,  2,  2,  3,  4,  1,  999,    4,  999,    4,  3},
        {9, 0,  1,  7,  5,  6,  8,  999,    4,  999,    3,  2},
        {8, 9,  0,  6,  4,  5,  7,  999,    3,  999,    2,  1},
        {2, 3,  4,  0,  1,  2,  1,  999,    2,  999,    6,  5},
        {4, 5,  6,  2,  0,  1,  3,  999,    1,  999,    8,  7},
        {3, 4,  5,  1,  2,  0,  2,  999,    3,  999,    7,  6},
        {1, 2,  3,  1,  2,  3,  0,  999,    3,  999,    5,  4},
        {4, 5,  6,  2,  3,  1,  3,  0,      4,  999,    8,   7},
        {5, 6,  7,  3,  1,  2,  4,  999,    0,  999,    9,  8},
        {5, 6,  7,  3,  4,  2,  4,  1,      5,   0,     9,  8},
        {6, 7,  8,  4,  2,  3,  5,  999,    1,  999,    0,  9},
        {7, 8,  9,  5,  3,  4,  6,  999,    2,  999,    1,  0},

            };


            return matrix;
        }





        public static BusLinesList GeneerateBookExampleBusLinesList()
        {
            var result = new BusLinesList();


            result.AddRange(new BusLineIoDto[]
            {
                new BusLineIoDto
                {
                    Name = "Czarna",

                },
                 new BusLineIoDto
                {
                    Name = "Czerwona",

                },
                  new BusLineIoDto
                {
                    Name = "Niebieska",

                },
                   new BusLineIoDto
                {
                    Name = "Zielona",

                }
            });

            // "Czarna",
            result[0].AddRange(new BusStopLineIoDto[]
                    {
                      new BusStopLineIoDto
                      {
                            BusStopId = 1,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 2,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 3,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 12,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 11,
                      }
                    });

            // "Czerwona"
            result[1].AddRange(new BusStopLineIoDto[]
                   {
                        new BusStopLineIoDto
                      {
                            BusStopId = 1,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 7,
                      },
                       new BusStopLineIoDto
                      {
                            BusStopId = 4,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 5,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 9,
                      }
                   });

            //  "Niebieska",
            result[2].AddRange(new BusStopLineIoDto[]
                   {
                      new BusStopLineIoDto
                      {
                            BusStopId = 10,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 8,
                      },
                       new BusStopLineIoDto
                      {
                            BusStopId = 6,
                      },
                        new BusStopLineIoDto
                      {
                            BusStopId = 4,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 7,
                      },
                       new BusStopLineIoDto
                      {
                            BusStopId = 1,
                      }
                   });

            // "Zielona",
            result[3].AddRange(new BusStopLineIoDto[]
                   {
                      new BusStopLineIoDto
                      {
                            BusStopId = 11,
                      },
                        new BusStopLineIoDto
                      {
                            BusStopId = 9,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 5,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 6,
                      }
                   });

            return result;
        }


        public static BusStopList GeneerateBookExampleBusStopList()
        {
            var result = new BusStopList();

            var factory = new MockFactory(MockBehavior.Default) { DefaultValue = DefaultValue.Empty };



            var busMock1 = factory.Create<BusStop>();
            busMock1.Object.Name = "1";
            busMock1.Object.Id = 1;
            busMock1.Object.ArrayNumber = 0;

            var busMock2 = factory.Create<BusStop>();
            busMock2.Object.Name = "2";
            busMock2.Object.Id = 2;
            busMock2.Object.ArrayNumber = 1;


            var busMock3 = factory.Create<BusStop>();
            busMock3.Object.Name = "3";
            busMock3.Object.Id = 3;
            busMock3.Object.ArrayNumber = 2;

            var busMock4 = factory.Create<BusStop>();
            busMock4.Object.Name = "4";
            busMock4.Object.Id = 4;
            busMock4.Object.ArrayNumber = 3;


            var busMock5 = factory.Create<BusStop>();
            busMock5.Object.Name = "5";
            busMock5.Object.Id = 5;
            busMock5.Object.ArrayNumber = 4;

            var busMock6 = factory.Create<BusStop>();
            busMock6.Object.Name = "6";
            busMock6.Object.Id = 6;
            busMock6.Object.ArrayNumber = 5;

            var busMock7 = factory.Create<BusStop>();
            busMock7.Object.Name = "7";
            busMock7.Object.Id = 7;
            busMock7.Object.ArrayNumber = 6;

            var busMock8 = factory.Create<BusStop>();
            busMock8.Object.Name = "8";
            busMock8.Object.Id = 8;
            busMock8.Object.ArrayNumber = 7;


            var busMock9 = factory.Create<BusStop>();
            busMock9.Object.Name = "9";
            busMock9.Object.Id = 9;
            busMock9.Object.ArrayNumber = 8;

            var busMock10 = factory.Create<BusStop>();
            busMock10.Object.Name = "10";
            busMock10.Object.Id = 10;
            busMock10.Object.ArrayNumber = 9;

            var busMock11 = factory.Create<BusStop>();
            busMock11.Object.Name = "11";
            busMock11.Object.Id = 11;
            busMock11.Object.ArrayNumber = 10;

            var busMock12 = factory.Create<BusStop>();
            busMock12.Object.Name = "12";
            busMock12.Object.Id = 12;
            busMock12.Object.ArrayNumber = 11;



            List<Mock<BusStop>> mockList = new List<Mock<BusStop>>();
            mockList.Add(busMock1);
            mockList.Add(busMock2);
            mockList.Add(busMock3);
            mockList.Add(busMock4);
            mockList.Add(busMock5);
            mockList.Add(busMock6);
            mockList.Add(busMock7);
            mockList.Add(busMock8);
            mockList.Add(busMock9);
            mockList.Add(busMock10);
            mockList.Add(busMock11);
            mockList.Add(busMock12);

            //mockList.ForEach(mock => mock.SetReturnsDefault(maxDefaultdefaultDistance));

            var cartesianMocks = mockList.Cartesian(mockList, (first, second) => new GenericPair<Mock<BusStop>>(first, second));


            var distanceFive = new List<GenericPair<Mock<BusStop>>>();
            distanceFive.Add(new GenericPair<Mock<BusStop>>(busMock4, busMock5));
            distanceFive.Add(new GenericPair<Mock<BusStop>>(busMock5, busMock4));

            distanceFive.Add(new GenericPair<Mock<BusStop>>(busMock5, busMock6));
            distanceFive.Add(new GenericPair<Mock<BusStop>>(busMock6, busMock5));

            distanceFive.Add(new GenericPair<Mock<BusStop>>(busMock4, busMock6));
            distanceFive.Add(new GenericPair<Mock<BusStop>>(busMock6, busMock4));



            foreach (var pair in cartesianMocks)
            {
                if (distanceFive.Contains(pair))
                {
                  //  pair.First.Setup(x => x.GetDistance(pair.Second.Object)).Returns(5);
                    pair.First.Setup(x => x.GetWalkTime(pair.Second.Object)).Returns(5);
                }
                else
                {
                    pair.First.Setup(x => x.GetWalkTime(pair.Second.Object)).Returns(maxDefaultdefaultDistance);
                }
            }


            mockList.ForEach(x => result.Add(x.Object));

        

            return result;
        }



        public static BusStopList GeneerateBusStopList()
        {
            var result = new BusStopList();

            var factory = new MockFactory(MockBehavior.Default) { DefaultValue = DefaultValue.Empty };



            var busMock1 = factory.Create<BusStop>();
            busMock1.Object.Name = "1";
            busMock1.Object.Id = 1;
            busMock1.Object.ArrayNumber = 0;

            var busMock2 = factory.Create<BusStop>();
            busMock2.Object.Name = "2";
            busMock2.Object.Id = 2;
            busMock2.Object.ArrayNumber = 1;


            var busMock3 = factory.Create<BusStop>();
            busMock3.Object.Name = "3";
            busMock3.Object.Id = 3;
            busMock3.Object.ArrayNumber = 2;

            var busMock4 = factory.Create<BusStop>();
            busMock4.Object.Name = "4";
            busMock4.Object.Id = 4;
            busMock4.Object.ArrayNumber = 3;


            var busMock5 = factory.Create<BusStop>();
            busMock5.Object.Name = "5";
            busMock5.Object.Id = 5;
            busMock5.Object.ArrayNumber = 4;

            var busMock6 = factory.Create<BusStop>();
            busMock6.Object.Name = "6";
            busMock6.Object.Id = 6;
            busMock6.Object.ArrayNumber = 5;

            var busMock7 = factory.Create<BusStop>();
            busMock7.Object.Name = "7";
            busMock7.Object.Id = 7;
            busMock7.Object.ArrayNumber = 6;

            var busMock8 = factory.Create<BusStop>();
            busMock8.Object.Name = "8";
            busMock8.Object.Id = 8;
            busMock8.Object.ArrayNumber = 7;


            var busMock9 = factory.Create<BusStop>();
            busMock9.Object.Name = "9";
            busMock9.Object.Id = 9;
            busMock9.Object.ArrayNumber = 8;


            List<Mock<BusStop>> mockList = new List<Mock<BusStop>>();
            mockList.Add(busMock1);
            mockList.Add(busMock2);
            mockList.Add(busMock3);
            mockList.Add(busMock4);
            mockList.Add(busMock5);
            mockList.Add(busMock6);
            mockList.Add(busMock7);
            mockList.Add(busMock8);
            mockList.Add(busMock9);

            //mockList.ForEach(mock => mock.SetReturnsDefault(maxDefaultdefaultDistance));

            var cartesianMocks = mockList.Cartesian(mockList, (first, second) => new GenericPair<Mock<BusStop>>(first, second));


            var distanceFive = new List<GenericPair<Mock<BusStop>>>();
            distanceFive.Add(new GenericPair<Mock<BusStop>>(busMock7, busMock2));
            distanceFive.Add(new GenericPair<Mock<BusStop>>(busMock2, busMock7));

            distanceFive.Add(new GenericPair<Mock<BusStop>>(busMock5, busMock6));
            distanceFive.Add(new GenericPair<Mock<BusStop>>(busMock6, busMock5));

            var distanceTen = new List<GenericPair<Mock<BusStop>>>();
            distanceTen.Add(new GenericPair<Mock<BusStop>>(busMock1, busMock8));
            distanceTen.Add(new GenericPair<Mock<BusStop>>(busMock8, busMock1));

            foreach (var pair in cartesianMocks)
            {
                if (distanceFive.Contains(pair))
                {
                    pair.First.Setup(x => x.GetWalkTime(pair.Second.Object)).Returns(5);
                }
                else if (distanceTen.Contains(pair))
                {
                    pair.First.Setup(x => x.GetWalkTime(pair.Second.Object)).Returns(10);
                }
                else
                {
                    pair.First.Setup(x => x.GetWalkTime(pair.Second.Object)).Returns(TableData.maxDefaultdefaultDistance);
                }
            }

            //busMock1.Setup(x => x.GetDistance(busMock8.Object)).Returns(10);
            //busMock8.Setup(x => x.GetDistance(busMock1.Object)).Returns(10);

            //busMock2.Setup(x => x.GetDistance(busMock7.Object)).Returns(5);
            //busMock7.Setup(x => x.GetDistance(busMock2.Object)).Returns(5);


            //busMock6.Setup(x => x.GetDistance(busMock5.Object)).Returns(5);
            //busMock5.Setup(x => x.GetDistance(busMock6.Object)).Returns(5);
            mockList.ForEach(x => result.Add(x.Object));

            return result;
        }

        public static BusLinesList GeneerateBusLinesList()
        {
            var result = new BusLinesList();


            result.AddRange(new BusLineIoDto[]
            {
                new BusLineIoDto
                {
                    Name = "Ciągła kreska",

                },
                 new BusLineIoDto
                {
                    Name = "Przerywane kreski",

                },
                  new BusLineIoDto
                {
                    Name = "Kreska kropka",

                },
                   new BusLineIoDto
                {
                    Name = "Kreska i dwie kropki",

                }
            });

            // "Ciągła kreska",
            result[0].AddRange(new BusStopLineIoDto[]
                    {
                      new BusStopLineIoDto
                      {
                            BusStopId = 1,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 2,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 3,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 5,
                      }
                    });

            // "Przerywane kreski"
            result[1].AddRange(new BusStopLineIoDto[]
                   {
                        new BusStopLineIoDto
                      {
                            BusStopId = 4,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 3,
                      },
                       new BusStopLineIoDto
                      {
                            BusStopId = 7,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 9,
                      }
                   });

            //  "Kreska kropka",
            result[2].AddRange(new BusStopLineIoDto[]
                   {
                      new BusStopLineIoDto
                      {
                            BusStopId = 2,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 8,
                      },
                       new BusStopLineIoDto
                      {
                            BusStopId = 9,
                      }
                   });

            // "Kreska i dwie kropki",
            result[3].AddRange(new BusStopLineIoDto[]
                   {
                      new BusStopLineIoDto
                      {
                            BusStopId = 6,
                      },
                        new BusStopLineIoDto
                      {
                            BusStopId = 7,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 8,
                      },
                      new BusStopLineIoDto
                      {
                            BusStopId = 9,
                      }
                   });

            return result;
        }


        public static int[,] GetDMatixData()
        {
            // zmieniłem z oryginałem bo był błedny
            return new int[9, 9] {
               {0, 1, 2, 999, 3, 4, 2, 1, 2},
               {2, 0, 1, 999, 2, 3, 1, 1, 2},
               {3, 2, 0, 999, 1, 2, 1, 2, 2},
               {4, 3, 1, 0,   2, 3, 2, 3, 3},
               {4, 3, 4, 999, 0, 1, 2, 3, 3},
               {3, 2, 3, 999, 1, 0, 1, 2, 2},
               {2, 1, 2, 999, 3, 4, 0, 1, 1},
               {1, 2, 3, 999, 4, 5, 3, 0, 1},
               {999, 999, 999, 999, 999, 999, 999, 999, 0}
            };
        }
    }
}