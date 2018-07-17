using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyQuickNote
{
    /// <summary>
    /// Interaction logic for Statitistics.xaml
    /// </summary>
    public partial class Statitistics : Window
    {
        public Statitistics()
        {
            InitializeComponent();
            SetData();  
            PieChart.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Pie.IsChecked == true)
            {
                PieChart.Visibility = Visibility.Visible;
                BarChart.Visibility = Visibility.Hidden;
                AreaChart.Visibility = Visibility.Hidden;
            }
            else if(Bar.IsChecked == true)
            {
                PieChart.Visibility = Visibility.Hidden;
                BarChart.Visibility = Visibility.Visible;
                AreaChart.Visibility = Visibility.Hidden;
            }
            else
            {
                PieChart.Visibility = Visibility.Hidden;
                BarChart.Visibility = Visibility.Hidden;
                AreaChart.Visibility = Visibility.Visible;
            }
        }

        private void SetData()
        {
            List<Data> listData = Data.DocFileData("QuickNote.txt");
            List<KeyValuePair<string, int>> MyValue = new List<KeyValuePair<string, int>>();
            int soTag = listData.Count;

            for (int i = 0; i < soTag; ++i)
            {
                MyValue.Add(new KeyValuePair<string, int>(listData[i].Tag, listData[i].SoLanGoiTag));
            }
            PieChart.DataContext = MyValue;
            BarChart.DataContext = MyValue;
            AreaChart.DataContext = MyValue;
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            SetData();
        }
        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.isRunningStatitistics = false;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.isRunningStatitistics = false;
        }
    }
}
