// // -----------------------------------------------------------------------
// //  <copyright file="MainWindow.Varibles.cs" company="DevCore .NET">
// //      Copyright DevCore.NET All rights reserved.
// //  </copyright>
// //  <author>Paweł Kondzior</author>
// // -----------------------------------------------------------------------
namespace Magisterka.Application.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using System.Windows.Input;
    using Magisterka.Infrastructure.Shared.Basic;
    using Magisterka.Infrastructure.Shared.ChartExt;
    using Magisterka.Infrastructure.Shared.Collections;
    using Magisterka.Infrastructure.Shared.Consts;
    using Magisterka.Infrastructure.Shared.Enum;
    using Magisterka.Infrastructure.Shared.Extensions;
    using Magisterka.Infrastructure.Shared.Structure;
    using Magisterka.Modules.Main.Algoritms.PopulationGenerators;
    using Magisterka.Modules.Main.Init;
    using Magisterka.Modules.Main.Services;
    using MessageBox = System.Windows.MessageBox;
    using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
    using Magisterka.Modules.Main.Algoritms.EA;
    using log4net;
   // using Magisterka.Modules.Main.Aspects;
    using System.Collections.ObjectModel;

    public partial class MainWindow
    {
        #region Fields

        private static readonly ILog log = LogManager.GetLogger(typeof(MainWindow).Name);

        /// <summary>
        /// The windows forms chart.
        /// </summary>
       /// public readonly Chart WindowsFormsChart = new Chart();

        /// <summary>
        /// The data service.
        /// </summary>
        private readonly DataService DataService =  DataService.DataServiceInstance;

        /// <summary>
        /// The counter.
        /// </summary>
        private int counter = 0;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the algorithm parameters.
        /// </summary>
        public static AlgorithmParameters AlgorithmParameters { get; set; }    

        /// <summary>
        /// Gets or sets the p.
        /// </summary>
        public static Population p { get; set; }

        /// <summary>
        /// Gets or sets the bus graph.
        /// </summary>
        public BusGraph BusGraph { get; set; }

        /// <summary>
        /// Gets or sets the bus lines.
        /// </summary>
        public BusLinesList BusLines { get; set; }

        /// <summary>
        /// Gets or sets the bus stop list.
        /// </summary>
        public BusStopList BusStopList { get; set; }

        /// <summary>
        /// Gets or sets the end point.
        /// </summary>
        public LabeledPoint EndPoint { get; set; }

        /// <summary>
        /// Gets or sets the population.
        /// </summary>
        public Infrastructure.Shared.Structure.Population Population { get; set; }

        /// <summary>
        /// Gets or sets the start point.
        /// </summary>
        public LabeledPoint StartPoint { get; set; }

        /// <summary>
        /// Gets or sets the wykres.
        /// </summary>
        public Wykres Wykres { get; set; }

        /// <summary>
        /// Ustawai generator populacji
        /// </summary>
        private BasePopulationGenerator PopulationGenerator { get; set; }

        private BaseEaAlgorithm EaAlgorithm { get; set; }


        public PartOfTheRoute AlgorithmStartPoint { get; set; }

        public PartOfTheRoute AlgorithmEndPoint { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the selected squares.
        /// </summary>
        private SquareList SelectedSquares { get; set; }

        /// <summary>
        /// Gets or sets the square list.
        /// </summary>
        private SquareList SquareList { get; set; }

        public ObservableCollection<EntireRoute> ObservableCollection { get; set; }

        public StorageService StorageService { get; set; }

        #endregion
    }
}