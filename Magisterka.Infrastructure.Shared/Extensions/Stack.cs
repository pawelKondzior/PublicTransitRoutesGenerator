// -----------------------------------------------------------------------
//  <copyright file="Stack.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Magisterka.Infrastructure.Shared.Structure;

namespace Magisterka.Infrastructure.Shared.Extensions
{
    public static class StackExt
    {
        public static bool AddIfNotExists<T>(this Stack<T> stack, T itemToAdd)
        {
            if (stack.Contains(itemToAdd))
            {
                return false;
            }
            else
            {
                stack.Push(itemToAdd);
                return true;
            }
        }
    }
}