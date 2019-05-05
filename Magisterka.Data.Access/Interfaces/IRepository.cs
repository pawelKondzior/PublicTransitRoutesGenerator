// -----------------------------------------------------------------------
//  <copyright file="IRepository.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Magisterka.Data.Access.Interfaces
{
    public interface IRepository<T> // : IDisposable
    {
        List<T> All();
    }
}