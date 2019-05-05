// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Trasa.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for Trasa.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Magisterka.Application.Wpf
{
    using System.Windows;

    using Magisterka.Infrastructure.Shared.ChartExt;
    using Magisterka.Infrastructure.Shared.Structure;
    using System.Drawing;

    /// <summary>
    /// Interaction logic for Trasa.xaml
    /// </summary>
    public partial class Trasa : Window
    {
        public MainWindow MainWindow { get; set; }

        public EntireRoute EntireRoute { get; set; }

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Trasa"/> class.
        /// </summary>
        public Trasa(MainWindow mainWindow, EntireRoute entireRoute)
        {
            this.InitializeComponent();

            this.MainWindow = mainWindow;
            this.EntireRoute = entireRoute;

            listView1.ItemsSource = entireRoute.PartOfTheRoute;            
        }

        #endregion

        private void listView1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var item = (PartOfTheRoute)listView1.SelectedItem;

            Drow(item);
        }

        private void listView1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (PartOfTheRoute)listView1.SelectedItem;
            Przystanek przystanek = new Przystanek(MainWindow, item.BusStop);
            przystanek.ShowDialog();
        }

        private void BStartPosition_Click(object sender, RoutedEventArgs e)
        {
            var item = (PartOfTheRoute)listView1.SelectedItem;
            MainWindow.AlgorithmStartPoint = item;
            Drow();
        }

        private void BEndPosition_Click(object sender, RoutedEventArgs e)
        {
           var item = (PartOfTheRoute)listView1.SelectedItem;
            MainWindow.AlgorithmEndPoint = item;
            Drow();
        }

        private void Drow(PartOfTheRoute item = null)
        {
            if (item != null)
            {
                MainWindow.MainMap.MakeBusStopBigger(item.BusStop);
            }
        }

        private void BCheckDuplicates_Click(object sender, RoutedEventArgs e)
        {
            if (EntireRoute.CheckIfHaveDuplicateBusStopsOneAfterAnother())
            {
                BCheckDuplicates.Background = System.Windows.Media.Brushes.Red;
            }
            else
            {
                BCheckDuplicates.Background = System.Windows.Media.Brushes.Green;
            }
        }

        private void BCheckTimes_Click(object sender, RoutedEventArgs e)
        {
           if (EntireRoute.CheckIfTimesAreCorrectlyIncrementing())
            {
                BCheckTimes.Background = System.Windows.Media.Brushes.Green;
            }
            else
            {
                BCheckTimes.Background = System.Windows.Media.Brushes.Red;
            }
        }

        private void BRecalculateTimes_Click(object sender, RoutedEventArgs e)
        {
            EntireRoute.CompleteWithTimes(MainWindow.AlgorithmParameters.ArriveTime, MainWindow.AlgorithmParameters.DayTypeEnum);
        }

        private void BCreationBusStops_Click(object sender, RoutedEventArgs e)
        {
            this.MainWindow.MainMap.NormalAll();
            this.MainWindow.MainMap.HideAll();
            this.MainWindow.MainMap.DrowBusStops(this.EntireRoute.CreationInputBusLines);
            
        }

        private void BShowBusRoute_Click(object sender, RoutedEventArgs e)
        {
            this.MainWindow.MainMap.NormalAll();
            this.MainWindow.MainMap.HideAll();
            this.MainWindow.MainMap.DrowEntireRoute(this.EntireRoute);
        }
    }
}