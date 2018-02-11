using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Common;
using MultiDownloader.Model;
using MultiDownloader.Views;
using System.Windows.Input;
using MultiDownloader.Commands;
using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;
using System.Timers;
using System.Configuration;



namespace MultiDownloader.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        Timer aTimer;
        private ObservableCollection<UserControl> _lstControls = new ObservableCollection<UserControl>();
        public ObservableCollection<UserControl> ControlList
        {
            get { return _lstControls; }
            set
            {
                _lstControls = value;
                OnPropertyChanged("ControlList");
            }
        }

        public ICommand StartPrintCommand
        {
            get;
            set;
        }

        public ICommand StopPrintCommand
        {
            get;
            set;
        }

        public ICommand ConnectCommand
        {
            get;
            set;
        }

        public ICommand DisconnectCommand
        {
            get;
            set;
        }

        public ICommand DownloadFileCommand
        {
            get;
            set;
        }

        public ICommand AddRemovePrinterCommand
        {
            get;
            set;
        }

        public string Version
        {
            get;
            set;
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public string _fileToDownload;
        public string FileToDownload
        {
            get { return _fileToDownload; }
            set
            {
                _fileToDownload = value;
                OnPropertyChanged("FileToDownload");
            }
        }

        public MainWindowViewModel()
        {
           
            string strPrinters = Common.LocalConfigurationSettings.GetConfiguration("Printers");
            string[] arr = strPrinters.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                PrinterProperties printerProps = new PrinterProperties();
                printerProps.IPAddress = arr[i].Trim();
                this.ControlList.Add(new ucPrinter(printerProps, this));
            }

            ConnectCommand = new UserCommands(ConnectToPrinter, CanConnectToPrinter);
            DownloadFileCommand = new UserCommands(DownloadFile, CanDownload);
            StartPrintCommand = new UserCommands(StartPrint, CanPrintStart);
            StopPrintCommand = new UserCommands(StopPrint, CanPrintStop);
            DisconnectCommand = new UserCommands(DisconnectToPrinter, CanConnectToPrinter);
            AddRemovePrinterCommand = new UserCommands(AddRemovePrinter, CanAddRemovePrinters);
            //Read the version info
            string ver = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Version = "Version : " + ver;

            string strLicKey = Common.LocalConfigurationSettings.GetConfiguration("License_Key");
            strLicKey = Cryptography.Decrypt(strLicKey);
            if (strLicKey.ToUpper().Trim() == "TRIAL")
            {
                this.Title = "Multidownloader";
                if (IsLicenseExpired())
                {
                    Environment.Exit(0);
                }
               
                //CreateRegistryKeys();

                ////Create a timer of 15 mins..
                //aTimer = new Timer(1000);
                //aTimer.Start();
                //aTimer.Elapsed += new ElapsedEventHandler(aTimer_Elapsed);
            }

            else if (strLicKey.ToUpper().Trim() == ver)
            {
                // CreateRegistryKeys();
                this.Title = "Multidownloader | Licensed Version";
            }

            else
            {
                CommonMethods.ShowErrorMessage("Invalid Key!!!", "Error");
                Environment.Exit(0);
            }
            
           
        }

        private void MakeSanityNOK()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["Sanity"].Value = "fDhzVMN9xpQ=";
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private bool IsLicenseExpired()
        {
            string sanity = Common.LocalConfigurationSettings.GetConfiguration("Sanity");
            sanity = Cryptography.Decrypt(sanity);
            if (sanity != "OK")
            {
                CommonMethods.ShowErrorMessage("Object reference not set to instance of object", "Error");
                return true;
            }
            DateTime dt = DateTime.Now;
            int day = dt.Day;
            int Month = dt.Month;
            int curYear = 2017;
            if (curYear > 2017)
            {
                CommonMethods.ShowErrorMessage("Object reference not set to instance of object", "Error");
                MakeSanityNOK();
                return true;
            }

            //Application will not function after 15th July..
            if ((Month >= 7) && (day >= 15))
            {
                CommonMethods.ShowErrorMessage("Object reference not set to instance of object", "Error");
                MakeSanityNOK();
                return true;
            }

            return false;
        }

        //Set trial of 16 hours..
        int iDuration = 16 * 60 * 60;
        void aTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
           
            string [] arrTitle = this.Title.Split('-');
            TimeSpan t = TimeSpan.FromSeconds(iDuration);

            string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                            t.Hours,
                            t.Minutes,
                            t.Seconds);

            this.Title = arrTitle[0].Trim() + " - Time Remaining = " + answer;
            iDuration--;
            if (iDuration == 0)
            {
                aTimer.Stop();
                Common.CommonMethods.ShowInfoMessage("Trial duration expired. Please restart application", "Information");
                Environment.Exit(0);
            }
            
        }

        int MAX_ATTEMPTS = 60;
        private void CreateRegistryKeys()
        {
            Microsoft.Win32.RegistryKey key;
            string keyString = "MultiDownloader";
            string []subKeys = Registry.CurrentUser.GetSubKeyNames();
            int keyCount = (from r in subKeys where r == keyString select r).Count();
           // Microsoft.Win32.Registry.CurrentUser.DeleteSubKey(keyString);
            if (keyCount == 0)
            {
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(keyString);
            }
            else
            {
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyString, true);
            }

           // string encryptedVersion = Cryptography.Encrypt(this.Version);
            object oVal =  key.GetValue(this.Version, "c2qY/x59knw=");
            string defaultVal = oVal.ToString();
            string strDecryptedVal = Cryptography.Decrypt(defaultVal);
            int iVal = Convert.ToInt32(strDecryptedVal);
            iVal += 1;
            string strMaxAttempts = Common.LocalConfigurationSettings.GetConfiguration("Max_Attempts");
            strMaxAttempts = Cryptography.Decrypt(strMaxAttempts);
            int MaxAttempts = Convert.ToInt32(strMaxAttempts);
            if (iVal > MAX_ATTEMPTS)
            {
                CommonMethods.ShowInfoMessage("Maximum trial attempts reached. Please ask license key from vendor", "Trial Expired");
                Environment.Exit(0);
            }
            this.Title = "Multidownloader| " + iVal.ToString() + "/" + MaxAttempts.ToString();
            string strEncryptedVal = Cryptography.Encrypt(iVal.ToString());
            key.SetValue(this.Version, strEncryptedVal);
            key.Close();  
        }

        public delegate void ConnectPrinter();
        public event ConnectPrinter ConnectPrinterEvent;

        public void ConnectToPrinter()
        {
            if (ConnectPrinterEvent != null)
            {
                this.ConnectPrinterEvent();
            }
        }

        public delegate void DisconnectPrinter();
        public event DisconnectPrinter DisconnectPrinterEvent;

        public void DisconnectToPrinter()
        {
            if (DisconnectPrinterEvent != null)
            {
                this.DisconnectPrinterEvent();
            }
        }

        public delegate void DownloadFileDelegate(string strFileName);
        public event DownloadFileDelegate DownloadFileEvent;

        public void OnFileDownload(string strFileName)
        {
            if (DownloadFileEvent != null)
            {
                this.DownloadFileEvent(strFileName);
            }
        }

        public delegate void StartPrintDelegate();
        public event StartPrintDelegate StartPrintEvent;
        public void OnStartPrint()
        {
            if (StartPrintEvent != null)
            {
                StartPrintEvent();
            }
        }


        public delegate void StopPrintDelegate();
        public event StopPrintDelegate StopPrintEvent;
        public void OnStopPrint()
        {
            if (StopPrintEvent != null)
            {
                StopPrintEvent();
            }
        }

     

        public bool CanConnectToPrinter()
        {
            return true;
        }

        public void DownloadFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Dale Files(*.dle*)|*.dle|Counter Files(*.ctr.txt*)|*.ctr.txt";
            dlg.Title = "Type File";
            Nullable <bool> b = dlg.ShowDialog();
            if (b == false)
                return;

            this.FileToDownload = dlg.FileName;
            OnFileDownload(this.FileToDownload);

          
        }

        public void AddRemovePrinter()
        {
            AddRemovePrinters obj = new AddRemovePrinters();
            obj.ShowDialog();
        }

        public bool CanAddRemovePrinters()
        {
            return true;
        }

        public bool CanDownload()
        {
            return true;
        }

        public void StartPrint()
        {
            OnStartPrint();
        }

        public bool CanPrintStart()
        {
            return true;
        }

        public void StopPrint()
        {
            OnStopPrint();
        }

        public bool CanPrintStop()
        {
            return true;
        }
    }
}
