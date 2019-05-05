// -----------------------------------------------------------------------
//  <copyright file="List.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

namespace Magisterka.Infrastructure.Shared.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ListExt
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        /// <summary>
        /// Jeśli dodało zwraca true, jesli już jest false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="itemToAdd"></param>
        /// <returns></returns>
        public static bool AddIfNotExists<T>(this IList<T> list, T itemToAdd)
        {
            if (list.Contains(itemToAdd))
            {
                return false;
            }
            else
            {
                list.Add(itemToAdd);
                return true;
            }
        }

        public static bool AddCollectionIfNotExists<T>(this  List<List<T>> list, List<T> itemToAdd)
             where T : class 
        {
            if (itemToAdd.Count == 0)
            {
                return false;
            }

            foreach (var item in list)
            {
                if (item.Compare(itemToAdd))
                {
                    return false;
                }

            }

            list.Add(itemToAdd);
            return true;
        }

//        List<List<BusStop>>

        public static bool Compare<T>(this IList<T> list, IList<T> toCompare)
            where T : class 
        {
            if (list.Count != toCompare.Count)
            {
                return false;
            }

            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i] != toCompare[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static List<T> BetweenTwoIndexes<T>(this IList<T> list, int startIndex, int endIndex)
            where T : class
        {
            var resultList = new List<T>();

            foreach (var item in list)
            {
                var index = list.IndexOf(item);

                if (index > startIndex && index < endIndex)
                {
                    resultList.Add(item);
                }
            }

            return resultList;
        }
    }
}