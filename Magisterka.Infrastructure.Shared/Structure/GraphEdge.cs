// -----------------------------------------------------------------------
//  <copyright file="GraphEdge.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------

namespace Magisterka.Infrastructure.Shared.Structure
{
    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.IoDto;
    using QuickGraph;

    /// <summary>
    /// Krawędz grafy
    /// </summary>
    public class GraphEdge : IEdge<GraphNode>
    {
        #region Properties

        public ConnectionType ConnectionType { get; set; }

       // public GraphNode GraphNode{ get; set; }

        public int TimeInMinutes { get; set; }

        /// <summary>
        /// May be null
        /// </summary>
        public BusLineIoDto BusLine { get; set; }

        #endregion


        #region Constructors

        public GraphEdge()
        {
        }

        public GraphEdge(GraphNode target, GraphNode source, ConnectionType connectionType)
        {
            this.Target = target;
            this.Source = source;
            this.ConnectionType = connectionType;
        }

        public GraphEdge(GraphNode target, GraphNode source, ConnectionType connectionType, int timeInMinutes)
        {
            this.Target = target;
            this.Source = source;
            this.ConnectionType = connectionType;
            this.TimeInMinutes = timeInMinutes;
        }

        public GraphEdge(GraphNode target, GraphNode source, ConnectionType connectionType, BusLineIoDto line)
        {
            this.Target = target;
            this.Source = source;
            this.ConnectionType = connectionType;
            BusLine = line;
        }

        #endregion


        public GraphNode Source { get; set; }

        public GraphNode Target { get; set; }
       
    }
}