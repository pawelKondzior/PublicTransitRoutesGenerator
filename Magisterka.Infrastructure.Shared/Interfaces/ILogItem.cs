// // -----------------------------------------------------------------------
// //  <copyright file="ILogItem.cs" company="DevCore .NET">
// //      Copyright DevCore.NET All rights reserved.
// //  </copyright>
// //  <author>Paweł Kondzior</author>
// // -----------------------------------------------------------------------
using log4net;
namespace Magisterka.Infrastructure.Shared.Interfaces
{
    public interface ILogItem
    {
        void LogItem(ILog log);
    }
}