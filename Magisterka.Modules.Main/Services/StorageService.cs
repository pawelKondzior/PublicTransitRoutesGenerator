// // -----------------------------------------------------------------------
// //  <copyright file="StorageService.cs" company="DevCore .NET">
// //      Copyright DevCore.NET All rights reserved.
// //  </copyright>
// //  <author>Paweł Kondzior</author>
// // -----------------------------------------------------------------------
namespace Magisterka.Modules.Main.Services
{

    using Magisterka.Infrastructure.Shared.Structure;
    using System.Collections.Generic;
    using System.Linq;
    using MoreLinq;
    using Magisterka.Infrastructure.Shared.Generic;
    using EntityFramework.Utilities;
    using Magisterka.Infrastructure.Shared.Parameters;
    using Magisterka.Infrastructure.Shared.Enum;
    using System;
    using Magisterka.Data.Access.PP;
    using PetaPoco;
    using Magisterka.Data.Access.Models;
    using Magisterka.Infrastructure.Shared.Collections;
    using Anotar.Log4Net;

    public class StorageService
    {
        //private MagisterkaEntities DataContext { get; set; }
        DBContextDB DBContextDB { get; set; }

        public StorageService()
        {
            try
            {
                DBContextDB = new DBContextDB();
            }

            catch (Exception ex)
            {
                LogTo.ErrorException("db error", ex);

            }
        }


            public List<LastPoint> GetAllLastPointsWithNames(List<BusStop> busStopList)
        {
            try
            {
                var lastPoints = GetAllLastPoints();

                lastPoints.ForEach(lastPoint =>
                {
                    var startBusStop = busStopList.FirstOrDefault(x => x.Id == lastPoint.StartPointId);
                    var endBusStop = busStopList.FirstOrDefault(x => x.Id == lastPoint.EndPointId);

                    if (startBusStop != null)
                    {
                        lastPoint.FriendlyNameStart = startBusStop.NameAndStreet;
                    }

                    if (endBusStop != null)
                    {
                        lastPoint.FriendlyNameEnd = endBusStop.NameAndStreet;
                    }

                });

                return lastPoints;
            }
            catch (Exception ex)
            {
                LogTo.ErrorException("db error", ex);


                return new List<LastPoint>();
            }

        }

        public List<LastPoint> GetAllLastPoints()
            {
                var result = new List<LastPoint>();

                //using (var dataContext = new Magisterka.Data.Access.EF.DBContext())
                //{

                result = DBContextDB.Fetch<LastPoint>("order by id desc");//.OrderByDescending(x => x.Id).FirstOrDefault()
                                                                          //result = dataContext.LastPoints.OrderByDescending(x => x.Id)
                                                                          //    .OrderByDescending(x => x.Id)
                                                                          //    .ToList();

                return result;
            }

            public LastPoint GetLastSelectedPoints()
            {
                LastPoint lastPoints = null;


                lastPoints = DBContextDB.FirstOrDefault<LastPoint>("order by id desc");//.OrderByDescending(x => x.Id).FirstOrDefault();


                return lastPoints;
            }

            public void SetLastSelectedPoints(int firstPointId, int lastPointId)
            {
                try
                {

                    LastPoint lastPoints = new LastPoint
                    {
                        StartPointId = firstPointId,
                        EndPointId = lastPointId
                    };


                    DBContextDB.Insert(lastPoints);
                }
                catch (Exception ex)
                {
                    LogTo.ErrorException("db error", ex);

                }

            }


            public List<Parameter> GetAllParametersWithoutGuid()
            {
                var list = DBContextDB.Fetch<Parameter>("WHERE [LockedFor] is null");

                return list;
            }


            public bool LockParameter(int parameterId, Guid threadGuid)
            {
                using (var tr = DBContextDB.GetTransaction())
                {

                    var parameter = DBContextDB.FirstOrDefault<Parameter>("WHERE Id = @0", parameterId);

                    if (!parameter.LockedFor.HasValue)
                    {
                        parameter.LockDate = DateTime.Now;
                        parameter.LockedFor = threadGuid;

                        DBContextDB.Update(parameter);
                        tr.Complete();

                        return true;
                    }
                    return false;

                }
            }


            public List<TestToBeDone> GetTestToBeDone(int parameterId, Guid myGuid)
            {
                var result = DBContextDB.Fetch<TestToBeDone>(";EXEC [dbo].[GetTestToBeDone] @0, @1", parameterId, myGuid);

                return result;
            }



            public void SaveResultError(TestToBeDone testToBeDone)
            {
                var parameter = DBContextDB.FirstOrDefault<Parameter>("WHERE Id = @0", testToBeDone.Id);
                var points = DBContextDB.FirstOrDefault<StartStopPoint>("WHERE Id = @0", testToBeDone.StartStopPointsId);

                var result = new Result();
                result.ParametersId = parameter.Id;
                result.StartStopPointsId = points.Id;
                result.GenerationTime = 0;
                result.RouteExists = false;
                result.Error = true;

                var resultId = (long)DBContextDB.Insert(result);

            }

            public void SaveResults(TestToBeDone testToBeDone, Population population, TimeSpan generationTime)
            {
                var parameter = DBContextDB.FirstOrDefault<Parameter>("WHERE Id = @0", testToBeDone.Id);
                var points = DBContextDB.FirstOrDefault<StartStopPoint>("WHERE Id = @0", testToBeDone.StartStopPointsId);


                var result = new Result();
                result.ParametersId = parameter.Id;
                result.StartStopPointsId = points.Id;
                result.GenerationTime = generationTime.TotalMilliseconds;

                if (population != null && population.Any())
                {
                    result.RouteExists = true;
                }
                else
                {
                    result.RouteExists = false;
                }

                var resultId = (long)DBContextDB.Insert(result);

                population.ForEach(popItem =>
                {
                    var singleResult = new SingleResult();
                    singleResult.ResultId = resultId;
                    singleResult.Fitness = popItem.AdaptationFunctionResult;
                    singleResult.Parts = popItem.PartOfTheRoute.Count;
                    singleResult.Time = popItem.RouteTime;
                    singleResult.TimeFromStart = popItem.TimeFromArrive;
                    singleResult.LinesCount = popItem.LinesCount;
                    singleResult.WalkConnectionsCount = popItem.WalkConnectionsCount;
                    singleResult.BusConnectionsCount = popItem.BusConnectionsCount;

                    DBContextDB.Insert(singleResult);
                });
            }





            //public List<Parameters> GetParmsForTestAlgorithmTypeEnum(TestAlgorithmTypeEnum testAlgorithmTypeEnum)
            //{

            //    //using (var dataContext = new Magisterka.Data.Access.EF.DBContext())
            //    //{
            //    //    //dataContext.StartStopPoints
            //    //    //    .Where(x => x.Result)
            //    //}
            //}

            public void GenerateAllParameters()
            {
                GenerateParametersSet(ParametersRanges.PopulationGenerators, false, false);
                GenerateParametersSet(ParametersRanges.PopulationGeoGenerators, false, true);
                GenerateParametersSet(ParametersRanges.EAGenerators, true, false);
                GenerateParametersSet(ParametersRanges.EAGeoGenerators, true, true);

            }

            public void GenerateParametersSet(IEnumerable<TestAlgorithmTypeEnum> listOfAlgorithms, bool generateForEa, bool ganerateForGeo)
            {
                var resultList = new List<Parameter>();
                var tempList = new List<Parameter>();

                listOfAlgorithms.ForEach(value =>
                {
                    var newItem = new Parameter();
                    newItem.TestAlgorithmTypeEnum = (int)value;
                    resultList.Add(newItem);
                });


                tempList = resultList.ToList();
                resultList = new List<Parameter>();

                tempList.ForEach(item =>
                {
                    ParametersRanges.ChangeNumber.ForEach(value =>
                    {
                        var newItem = new Parameter();
                        newItem.TestAlgorithmTypeEnum = item.TestAlgorithmTypeEnum;
                        newItem.ChangeNumber = value;

                        resultList.Add(newItem);

                    });
                });

                tempList = resultList.ToList();
                resultList = new List<Parameter>();


                tempList.ForEach(item =>
                {
                    ParametersRanges.LinkType.ForEach(value =>
                    {
                        var newItem = item.CloneParameterValues();
                        newItem.LinkType = value;

                        resultList.Add(newItem);

                    });
                });

                tempList = resultList.ToList();
                resultList = new List<Parameter>();



                tempList.ForEach(item =>
                {
                    ParametersRanges.PopulationCount.ForEach(value =>
                    {
                        var newItem = item.CloneParameterValues();
                        newItem.PopulationCount = value;

                        resultList.Add(newItem);

                    });
                });



                if (generateForEa)
                {

                    ///tempList = resultList.ToList();
                    ///resultList = new List<Parameter>();

                    //tempList.ForEach(item =>
                    //{
                    //    ParametersRanges.AdaptationFunctionTypeEnum.ForEach(value =>
                    //    {
                    //        var newItem = item.CloneParameterValues();
                    //        newItem.AdaptationFunctionTypeEnum = (int)value;

                    //        resultList.Add(newItem);

                    //    });
                    //});


                    resultList.ForEach(item =>
                    {
                        item.AdaptationFunctionTypeEnum = (int)AdaptationFunctionTypeEnum.ByTime;
                    //    ParametersRanges.AdaptationFunctionTypeEnum.ForEach(value =>
                    //    {
                    //        var newItem = item.CloneParameterValues();
                    //        newItem.AdaptationFunctionTypeEnum = (int)value;

                    //        resultList.Add(newItem);

                    //    });
                });

                    tempList = resultList.ToList();
                    resultList = new List<Parameter>();


                    tempList.ForEach(item =>
                    {
                        ParametersRanges.MutationProbability.ForEach(value =>
                        {
                            var newItem = item.CloneParameterValues();
                            newItem.MutationProbability = value;

                            resultList.Add(newItem);

                        });
                    });


                    tempList = resultList.ToList();
                    resultList = new List<Parameter>();


                    tempList.ForEach(item =>
                    {
                        ParametersRanges.NumberOfEvaluation.ForEach(value =>
                        {
                            var newItem = item.CloneParameterValues();
                            newItem.NumberOfEvaluation = value;

                            resultList.Add(newItem);

                        });
                    });
                }



                if (ganerateForGeo)
                {
                    tempList = resultList.ToList();
                    resultList = new List<Parameter>();

                    tempList.ForEach(item =>
                    {
                        ParametersRanges.NumberOfSquares.ForEach(value =>
                        {
                            var newItem = item.CloneParameterValues();
                            newItem.NumberOfSquares = value; ;

                            resultList.Add(newItem);
                        });
                    });


                    tempList = resultList.ToList();
                    resultList = new List<Parameter>();

                    tempList.ForEach(item =>
                    {
                        ParametersRanges.NumberOfNeighborSquares.ForEach(value =>
                        {
                            var newItem = item.CloneParameterValues();
                            newItem.NumberOfNeighborSquares = value;

                            resultList.Add(newItem);

                        });
                    });
                }


                tempList = resultList.ToList();
                ///  resultList = new List<Parameters>();
                ///  

                //using (var tr = DBContextDB.GetTransaction())
                //{
                int i = 0;
                tempList.ForEach(item =>
                {
                    Console.WriteLine("Dodaje {0} z {1}", i, tempList.Count);

                    DBContextDB.Insert(item);

                    i++;
                });
                //    tr.Complete();
                //}

                //using (var tr = DBContextDB.GetTransaction())
                //{
                //int i = 0;
                //tempList.ForEach(item =>
                //{
                //    Console.WriteLine("Dodaje {0} z {1}", i, tempList.Count);

                //    DBContextDB.Insert(item);

                //    i++;
                //});
                //    tr.Complete();
                //}

            }



            public void GenerateBusStopCombinationFromSquares(BusStopList busStopList, int squereInOneLine)
            {
                var squereList = busStopList.GetEquallyDividedSquares(squereInOneLine);
                squereList.CompleteInnerBusStopsForAllSquares(busStopList);


                List<BusStop> list = new List<BusStop>();

                squereList.ForEach(squere =>
                {
                    var busStop = squere.InnerBusStops.FirstOrDefault();

                    if (busStop != null)
                    {
                        list.Add(busStop);
                    }

                });

                var combinations = GenerateAllBusStopCombination(list);


                combinations.ForEach(item => DBContextDB.Insert(item));

            }


            public List<StartStopPoint> GetTestBusStartStopPoints()
            {
                return DBContextDB.Fetch<StartStopPoint>("").ToList();
            }


            public List<StartStopPoint> GenerateAllBusStopCombination(List<BusStop> list)
            {
                var cartesian = list.Cartesian(list, (first, second) => new GenericPair<BusStop>(first, second));

                var result = new List<StartStopPoint>();

                cartesian.ForEach(x =>
                {
                    if (x.First.Id != x.Second.Id)
                    {

                        var newItem = new StartStopPoint
                        {
                            StartId = x.First.Id,
                            StopId = x.Second.Id
                        };

                        result.Add(newItem);
                    }
                });


                return result;
            }


            public List<GetTestIds> GetAllTestIds()
            {
                var result = DBContextDB.Fetch<GetTestIds>(";EXEC [dbo].[GetTestIds]");

                return result;
            }
        }
    }