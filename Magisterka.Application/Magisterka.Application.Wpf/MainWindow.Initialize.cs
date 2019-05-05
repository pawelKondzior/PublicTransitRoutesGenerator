// // -----------------------------------------------------------------------
// //  <copyright company="DevCore .NET">
// //      Copyright (c) DevCore.NET All rights reserved.
// //  </copyright>
// //  <author> Paweł Kondzior</author>
// // -----------------------------------------------------------------------
namespace Magisterka.Application.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using System.Windows.Input;
    using AutoMapper;
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
    
    using log4net;
    //  using Magisterka.Modules.Main.Aspects;

    using Anotar.Log4Net;

    public partial class MainWindow
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            try
            {
                LogTo.Info("Uruchomiono aplikacje");

                new AutoMapperInit().Init();
                this.InitializeComponent();

                this.InitializeWidnowData();

                AlgorithmParameters = new AlgorithmParameters();
                AlgorithmParameters.SetDefaultValues();

                this.BusStopList = this.DataService.GetXmlBusStopList();

                this.BusLines = this.DataService.GetBusLines();

                InitializeMap(this.BusStopList);

                this.RegisterMapEvents();

                


                DDBusLines.ItemsSource = BusLines;

                UpdateSquareList();


                this.StorageService = new StorageService();

                var lastPointList = StorageService.GetAllLastPointsWithNames(this.BusStopList);

                DDLastBusStops.ItemsSource = lastPointList;
                //DDLastBusStops.Items = lastPointList;
            }
            catch (Exception exception)
            {
                LogTo.ErrorException(" MainWindow()", exception);

             //   MessageBox.Show(exception.Message);

               // this.Close();
            }
            finally
            {

            }

        }

        #endregion




        public void UpdateSquareList()
        {
            if (SquareList != null)
            {
                MainMap.ClearSquares();
            }

            this.SquareList = this.BusStopList.GetEquallyDividedSquares(AlgorithmParameters.NumberOfSquares);
            this.SquareList.CompleteInnerBusStopsForAllSquares(this.BusStopList);

           MainMap.DrowSquares(SquareList);
        }

        /// <summary>
        /// The initialize widnow data.
        /// </summary>
        private void InitializeWidnowData()
        {
            this.CBPopulationType.ItemsSource = StaticData.PopulationGeneratorComboBoxContent;
            this.CbEvolutionAlgorithmType.ItemsSource = StaticData.EvolutionAlgorithmTypeComboBoxContent;
        }




    }
}