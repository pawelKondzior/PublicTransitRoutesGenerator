using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Magisterka.Infrastructure.Shared.Consts;
using Magisterka.Infrastructure.Shared.Enum;

namespace Magisterka.Application.Wpf
{
    /// <summary>
    /// Interaction logic for Parametry.xaml
    /// </summary>
    public partial class Parametry : Window
    {
        MainWindow MainWindow { get; set; }

        public Parametry(MainWindow main )
        {
            MainWindow = main;

            InitializeComponent();
            InitializeData();


            IEnumerable<TextBox> collection = this.grid1.Children.OfType<TextBox>();

            foreach (TextBox tb in collection)
            {
                tb.TextChanged += Tb_TextChanged;
                // do something with tb here
            }

        }

        private void Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetData();
            if (!MainWindow.AlgorithmParameters.ValidateParameters())
            {
                ((TextBox)sender).Background = Brushes.Red;
            }
            else
            {
                ((TextBox)sender).Background = Brushes.White;
            }
        }

      

        private void InitializeData()
        {
            CbDayType.ItemsSource = StaticData.DayTypeComboBoxContent;
            TbMinChangeTime.Text = MainWindow.AlgorithmParameters.MinChangeTime.ToString();

            TbLinkType.Text = MainWindow.AlgorithmParameters.LinkType.ToString();
            TbPopulationCount.Text = MainWindow.AlgorithmParameters.PopulationCount.ToString();
            TbChangeNumber.Text = MainWindow.AlgorithmParameters.ChangeNumber.ToString();
            TbPopulationPercent.Text = MainWindow.AlgorithmParameters.PopulationPercent.ToString();
            TbNumberOfEvaluation.Text = MainWindow.AlgorithmParameters.NumberOfEvaluation.ToString();
            TbSquareNumber.Text = MainWindow.AlgorithmParameters.NumberOfSquares.ToString();
            TbNumberOfNeighborSquares.Text = MainWindow.AlgorithmParameters.NumberOfNeighborSquares.ToString();
            TBChangeStack.Text = MainWindow.AlgorithmParameters.MaxComputetChangeStackResults.ToString();


        }


        private void GetData()
        {
            MainWindow.AlgorithmParameters.MinChangeTime = int.Parse(TbMinChangeTime.Text);

            if (CbDayType.SelectedItem == null)
            {
                MainWindow.AlgorithmParameters.DayTypeEnum = DayTypeEnum.WorkingDay; ;
            }
            else
            {
                KeyValuePair<DayTypeEnum, string> dayType = (KeyValuePair<DayTypeEnum, string>)CbDayType.SelectedItem;
                MainWindow.AlgorithmParameters.DayTypeEnum = dayType.Key;
            }

            try
            {
                MainWindow.AlgorithmParameters.LinkType = int.Parse(TbLinkType.Text);
                MainWindow.AlgorithmParameters.PopulationCount = int.Parse(TbPopulationCount.Text);
                MainWindow.AlgorithmParameters.ChangeNumber = int.Parse(TbChangeNumber.Text);
                MainWindow.AlgorithmParameters.PopulationPercent = int.Parse(TbPopulationPercent.Text);
                MainWindow.AlgorithmParameters.NumberOfEvaluation = int.Parse(TbNumberOfEvaluation.Text);
                MainWindow.AlgorithmParameters.NumberOfSquares = int.Parse(TbSquareNumber.Text);
                MainWindow.AlgorithmParameters.NumberOfNeighborSquares = int.Parse(TbNumberOfNeighborSquares.Text);
                MainWindow.AlgorithmParameters.MaxComputetChangeStackResults = int.Parse(TBChangeStack.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bok_Click(object sender, RoutedEventArgs e)
        {

            GetData();
            if (MainWindow.AlgorithmParameters.ValidateParameters())
            {
                MainWindow.UpdateSquareList();
                this.Close(); 

            }
            else
            {
                MessageBox.Show("Bledne parametry");
            }


            
        }

        private void HourSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }

}
