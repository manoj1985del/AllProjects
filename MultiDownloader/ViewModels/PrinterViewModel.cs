using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiDownloader.Model;
using System.Windows;
using JP_NANO;
using System.Windows.Input;
using MultiDownloader.Commands;
using Microsoft.Win32;
using System.IO;
using MultiDownloader.Views;


namespace MultiDownloader.ViewModels
{
    public class PrinterViewModel : ViewModelBase
    {
        #region ctor
        MainWindowViewModel mainWindowViewModel = null;
        public JPNANO g_objJPNano = null;
        private System.Timers.Timer aTimer = new System.Timers.Timer(10 * 1000);
        public PrinterViewModel(PrinterProperties printerProps, MainWindowViewModel _mainVM)
        {
            this.IPAddress = printerProps.IPAddress;
            g_objJPNano = new JPNANO(this.IPAddress);
            this.mainWindowViewModel = _mainVM;
            DownloadFileCommand = new UserCommands(DownloadFile, CanDownload);
            SettingsCommand = new UserCommands(ShowSettings, CanDownload);
            this.PingStatus = g_objJPNano.PingHost(this.IPAddress);
            this.mainWindowViewModel.ConnectPrinterEvent += new MainWindowViewModel.ConnectPrinter(mainWindowViewModel_ConnectPrinterEvent);
            this.mainWindowViewModel.DownloadFileEvent += new MainWindowViewModel.DownloadFileDelegate(mainWindowViewModel_DownloadFileEvent);
            this.mainWindowViewModel.StartPrintEvent += new MainWindowViewModel.StartPrintDelegate(mainWindowViewModel_StartPrintEvent);
            this.mainWindowViewModel.StopPrintEvent += new MainWindowViewModel.StopPrintDelegate(mainWindowViewModel_StopPrintEvent);
            this.mainWindowViewModel.DisconnectPrinterEvent += new MainWindowViewModel.DisconnectPrinter(mainWindowViewModel_DisconnectPrinterEvent);
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(aTimer_Elapsed);
            aTimer.Start();
        }

        void mainWindowViewModel_DisconnectPrinterEvent()
        {
            bool bRet = g_objJPNano.DisconnectToPrinter();
            if (bRet)
            {
                this.IsConnected = false;
                this.PrintStatus = false;
            }
        }

        void mainWindowViewModel_StopPrintEvent()
        {
            g_objJPNano.StartPrint = 2;
            this.PrintStatus = false;
        }
        
        void mainWindowViewModel_StartPrintEvent()
        {
            g_objJPNano.StartPrint = 1;
            this.PrintStatus = true;
        }

        void mainWindowViewModel_DownloadFileEvent(string strFileName)
        {
            
            this.DownloadFileToPrinter(strFileName);
            
        }

        private void DownloadFileToPrinter(string strFileName)
        {
            this.Status = "Downloading";
            bool bDownload = g_objJPNano.DownloadFileToPrinter(strFileName);
            if (bDownload)
            {

                this.Status = "Download Successful";
                strFileName = Path.GetFileName(strFileName);
                g_objJPNano.SetActiveMessage(strFileName);
                this.ActiveMessage = g_objJPNano.ActiveMessage;
            }
            else
                this.Status = "Download Fail";
        }

        void aTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.PingStatus = g_objJPNano.PingHost(this.IPAddress);
        }

        void mainWindowViewModel_ConnectPrinterEvent()
        {
            if (this.IsConnected)
                return;
            this.Status = "Connecting";
           
            this.IsConnected = g_objJPNano.ConnectToPrinter(this.IPAddress, 10100, "0001", "0001", JPNANO.AccessLevel.Administrator);
            this.ActiveMessage = g_objJPNano.ActiveMessage;
            if (this.IsConnected)
            {
                this.Status = "Connected";
            }
            else
                this.Status = "Not Connected";
            
           
        }
        #endregion
        #region properties
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

        private bool _pingStatus = false;
        public bool PingStatus
        {
            get { return _pingStatus; }
            set
            {
                _pingStatus = value;
                OnPropertyChanged("PingStatus");
            }
        }

        private bool _isConnected = false;
        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged("IsConnected");
            }
        }

        private bool _printStatus;
        public bool PrintStatus
        {
            get { return _printStatus; }
            set
            {
                _printStatus = value;
                OnPropertyChanged("PrintStatus");
            }
        }
        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        private long _curValue = 0;
        public long CurValue
        {
            get { return _curValue; }
            set
            {
                _curValue = value;
                OnPropertyChanged("CurValue");
            }
        }

        private long _maxValue = 100;
        public long MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                OnPropertyChanged("MaxValue");
            }
        }

        private string _activeMsg = "";
        public string ActiveMessage
        {
            get { return _activeMsg; }
            set
            {
                _activeMsg = value;
                OnPropertyChanged("ActiveMessage");
            }
        }

        public ICommand DownloadFileCommand
        {
            get;
            set;
        }

        public ICommand SettingsCommand
        {
            get;
            set;
        }

        public void ShowSettings()
        {
            Settings objSettings = new Settings(this);
            objSettings.ShowDialog();
        }
        public void DownloadFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Dale Files(*.dle*)|*.dle|Counter Files(*.ctr.txt*)|*.ctr.txt";
            dlg.Title = "Type File";
            Nullable<bool> b = dlg.ShowDialog();
            if (b == false)
                return;

            //this.DownloadFileToPrinter(dlg.FileName);
            
        }

        public bool CanDownload()
        {
            return this.IsConnected;
        }

        #endregion
    }
}
