// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomSelectorHelper.cs" company="">
//   
// </copyright>
//  <author>Paweł Kondzior</author>
// <summary>
//   The random selector helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Magisterka.Infrastructure.Shared.Helpers
{
    using System;
    using System.Collections.Generic;

    using Magisterka.Infrastructure.Shared.Enum;
    
    using RandomGen;
    using System.Threading;

    /// <summary>
    /// The random selector helper.
    /// </summary>
    public static class RandomSelectorHelper
    {
        #region Public Methods and Operators
        private static readonly ThreadLocal<Random> appRandom  = new ThreadLocal<Random>(() => new Random());
        /// <summary>
        /// The get random index.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The System.Int32.
        /// </returns>
        public static int GetRandomIndex<T>(List<T> collection)
        {
            

            //Func<int> ratioFunc = Gen.Random.Numbers.Integers(0, collection.Count);
            //int rand = ratioFunc();

            
            int rand =  appRandom.Value.Next(collection.Count);
            return rand;
        }

        /// <summary>
        /// The get random item.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The T.
        /// </returns>
      
        public static T GetRandomItem<T>(List<T> collection)
        {
          ///  Gen.Random.Numbers.Integers(0, collection.Count);

            // Func<int> ratioFunc = Gen.Random.Numbers.Integers(0, collection.Count);
            //int rand = ratioFunc();

            int rand = appRandom.Value.Next(collection.Count);
            ///
            
            

            return collection[rand];
        }

        /// <summary>
        /// The get random operator.
        /// </summary>
        /// <param name="mutationProbability">
        /// The mutation probability.
        /// </param>
        /// <returns>
        /// The Magisterka.Infrastructure.Shared.Enum.MutationOperatorTypeEnum.
        /// </returns>
        /// 




        public static MutationOperatorTypeEnum GetRandomOperator(double mutationProbability)
        {

            //var trandom = new TRandom();

            //trandom.Normal()

            //Func<double> ratioDoubleFunc = Gen.Random.Numbers.Doubles().BetweenZeroAndOne();
            //var ratio = ratioDoubleFunc();

            double ratio = appRandom.Value.NextDouble();
           
            if (ratio < (mutationProbability/100))
            {
                return MutationOperatorTypeEnum.Mutation;
            }

            return MutationOperatorTypeEnum.Hybridization;
        }

        /// <summary>
        /// The get two difrent random index.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The System.Tuple`2[T1 -&gt; T, T2 -&gt; T].
        /// </returns>
        public static Tuple<T, T> GetTwoDifrentRandomItems<T>(List<T> collection)
        {
            if (collection.Count < 2)
            {
                throw new Exception("Cannot generete two out of one item collection");
            }

            int startIndex = GetRandomIndex(collection);
            int endIndex = GetRandomIndex(collection);

            while (startIndex == endIndex)
            {
                endIndex = GetRandomIndex(collection);
            }

            if (startIndex > endIndex)
            {
                int temp = startIndex;
                startIndex = endIndex;
                endIndex = temp;
            }

            T startItem = collection[startIndex];
            T endItem = collection[endIndex];

            return new Tuple<T, T>(startItem, endItem);
        }

        #endregion
    }
}