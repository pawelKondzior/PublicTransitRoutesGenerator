// -----------------------------------------------------------------------
//  <copyright file="StringExt.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

using System;

namespace Magisterka.Infrastructure.Shared.Extensions
{

    public static class StringExt
    {
        /// <summary>
        /// Usowa literki i zmienia na stringa
        /// </summary>
        /// <returns></returns>
        public static int ForceParseInt(this string str)
        {
            char[] arr = str.ToCharArray();

            // arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-')));

            arr = Array.FindAll<char>(arr, (c => (char.IsDigit(c))));

            str = new string(arr);

            return int.Parse(str);
        }
    }
}