using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MultiDownloader.Model;
using System.Configuration;
using System.Windows.Input;
using MultiDownloader.Commands;

namespace MultiDownloader.ViewModels
{
    public class AddRemovePrinterViewModel : ViewModelBase
    {

        public AddRemovePrinterViewModel()
        {
            GetPrinterList();
            RemovePrinterCommand = new UserCommands(RemovePrinter, CanRemove);
            AddPrinterCommand = new UserCommands(AddPrinter, CanAddPrinter);
        }
        private ObservableCollection<PrinterProperties> _printerList = new ObservableCollection<PrinterProperties>();
        public ObservableCollection<PrinterProperties> PrinterList
        {
            get { return _printerList; }
            set
            {
                _printerList = value;
            }
        }

        private PrinterProperties _printerToRemove = null;
        public PrinterProperties PrinterToRemove
        {
            get { return _printerToRemove; }
            set
            {
                _printerToRemove = value;
                OnPropertyChanged("PrinterToRemove");
            }
        }

        private string _ipAddress;
        public string IPAddress
        {
            get { return _ipAddress; }
            set
            {
                _ipAddress = value;
                OnPropertyChanged("IPAddress");
            }
        }

        private void GetPrinterList()
        {
            string strPrinters = Common.LocalConfigurationSettings.GetConfiguration("Printers");
            string[] arr = strPrinters.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                PrinterProperties objProp = new PrinterProperties();
                objProp.IPAddress = arr[i].Trim();
                this.PrinterList.Add(objProp);
            }
        }

        public void RemovePrinter()
        {
            if (this.PrinterToRemove == null)
                return;
            string strPrinterToRemove = this.PrinterToRemove.IPAddress;
            int index = this.PrinterList.IndexOf(this.PrinterToRemove);
            this.PrinterList.RemoveAt(index);
            string strIPs = "";
            for (int i = 0; i < this.PrinterList.Count; i++)
            {
                PrinterProperties obj = this.PrinterList[i];
                strIPs += obj.IPAddress + ",";
            }
            strIPs = strIPs.Substring(0, strIPs.Length - 1);

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["Printers"].Value = strIPs;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public bool CanRemove()
        {
            if (this.PrinterList.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public ICommand RemovePrinterCommand
        {
            get;
            set;
        }

        public ICommand AddPrinterCommand
        {
            get;
            set;
        }

        public void AddPrinter()
        {
            for (int i = 0; i < PrinterList.Count; i++)
            {
                PrinterProperties printerProp = PrinterList[i];
                if (printerProp.IPAddress == this.IPAddress)
                {
                    Common.CommonMethods.ShowErrorMessage("Printer already exists", "Error");
                    return;
                }
               
            }

            PrinterProperties objProp = new PrinterProperties();
            objProp.IPAddress = this.IPAddress;
            this.PrinterList.Add(objProp);

            string strIPs = "";
            for (int i = 0; i < this.PrinterList.Count; i++)
            {
                PrinterProperties obj = this.PrinterList[i];
                strIPs += obj.IPAddress + ",";
            }
            strIPs = strIPs.Substring(0, strIPs.Length - 1);

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["Printers"].Value = strIPs;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            Common.CommonMethods.ShowInfoMessage("Printer Added Successfully.\nIf you don't wish to add another printer then restart the application", "Information");
        }
        public bool CanAddPrinter()
        {
            return true;
        }
    }
}
