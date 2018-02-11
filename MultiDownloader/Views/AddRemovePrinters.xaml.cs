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
using MultiDownloader.ViewModels;

namespace MultiDownloader.Views
{
    /// <summary>
    /// Interaction logic for AddRemovePrinters.xaml
    /// </summary>
    public partial class AddRemovePrinters : Window
    {
        public AddRemovePrinters()
        {
            InitializeComponent();
            this.DataContext = new AddRemovePrinterViewModel();
        }

      
    }
}
