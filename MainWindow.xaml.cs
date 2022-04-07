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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;

namespace BCS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        BCScanner bcs = new BCScanner();
        List<BCScanner> bcsList = new List<BCScanner>();

        public MainWindow()
        {
            InitializeComponent();
            InitialBinding();
            bcs.TCPClientStart();
        }

        public void InitialBinding() 
        {
            //LB_BCDisplay.ItemsSource = bcsList;
            //LB_BCDisplay.DisplayMemberPath = bcsList.Count.ToString();
            LB_BCDisplay.DataContext = bcs;
            Debug.WriteLine("test and test again");
        }

        private void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            bcs.TCPClientWrite("T");
        }

        private void OnQuitButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }  
}