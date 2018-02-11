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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MultiDownloader.Model;
using MultiDownloader.ViewModels;

namespace MultiDownloader.Views
{
    /// <summary>
    /// Interaction logic for ucPrinter.xaml
    /// </summary>
    public partial class ucPrinter : UserControl
    {
        public ucPrinter()
        {
            InitializeComponent();
           // this.DataContext = new PrinterViewModel(printerProps);
        }

        MainWindowViewModel mainWindowViewModel = null;
        public ucPrinter(PrinterProperties printerProps, MainWindowViewModel _mainVM)
        {
            InitializeComponent();
            this.DataContext = new PrinterViewModel(printerProps, _mainVM);
        }
    }
}
