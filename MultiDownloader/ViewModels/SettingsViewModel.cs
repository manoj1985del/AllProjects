using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JP_NANO;
using System.Windows.Input;
using MultiDownloader.Commands;

namespace MultiDownloader.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        PrinterViewModel printerViewMdoel;
        public SettingsViewModel(PrinterViewModel _printerViewModel)
        {
            this.printerViewMdoel = _printerViewModel;
            JP_NANO.JPNANO.Direction objectDir = this.printerViewMdoel.g_objJPNano.ObjectDirection;
            JPNANO.Orientation objOrientation = this.printerViewMdoel.g_objJPNano.MessageOrientation;
            if (objectDir == JPNANO.Direction.LTR)
            {
                this.IsForwardDirection = true;
                this.IsReverseDirection = false;
            }

            if (objectDir == JPNANO.Direction.RTL)
            {
                this.IsForwardDirection = false;
                this.IsReverseDirection = true;
            }

            if (objOrientation == JPNANO.Orientation.Normal)
            {
                this.IsNormal = true;
                this.IsReverse = false;
                this.IsInverted = false;
                this.IsInvertedReverse = false;
            }

            if (objOrientation == JPNANO.Orientation.Inverted)
            {
                this.IsNormal = false;
                this.IsReverse = false;
                this.IsInverted = true;
                this.IsInvertedReverse = false;
            }

            if (objOrientation == JPNANO.Orientation.Reverse)
            {
                this.IsNormal = false;
                this.IsReverse = true;
                this.IsInverted = false;
                this.IsInvertedReverse = false;
            }

            if (objOrientation == JPNANO.Orientation.InvertedReverse)
            {
                this.IsNormal = false;
                this.IsReverse = false;
                this.IsInverted = false;
                this.IsInvertedReverse = true;
            }

            this.ObjectDirectionCommand = new UserCommands(SetObjectDirection, CanExecute);
            this.OrientationCommand = new UserCommands(SetObjectOrientation, CanExecute);

            
        }

        private bool _forward = false;
        public bool IsForwardDirection
        {
            get { return _forward; }
            set
            {
                _forward = value;
                OnPropertyChanged("IsForwardDirection");
            }
        }

        private bool _reverse = false;
        public bool IsReverseDirection
        {
            get { return _reverse; }
            set
            {
                _reverse = value;
                OnPropertyChanged("IsReverseDirection");
            }
        }

        private bool _isNormal = false;
        public bool IsNormal
        {
            get { return _isNormal; }
            set
            {
                _isNormal = value;
                OnPropertyChanged("IsNormal");
            }
        }

        private bool _isInverted = false;
        public bool IsInverted
        {
            get { return _isInverted; }
            set
            {
                _isInverted = value;
                OnPropertyChanged("IsInverted");
            }
        }

        private bool _isReverse = false;
        public bool IsReverse
        {
            get { return _isReverse; }
            set
            {
                _isReverse = value;
                OnPropertyChanged("IsReverse");
            }
        }

        private bool _isInvertedReverse = false;
        public bool IsInvertedReverse
        {
            get { return _isInvertedReverse; }
            set
            {
                _isInvertedReverse = value;
                OnPropertyChanged("IsInvertedReverse");
            }
        }

        public void SetObjectDirection()
        {
            if (this.IsForwardDirection == true)
            {
                this.printerViewMdoel.g_objJPNano.ObjectDirection = JPNANO.Direction.LTR;
            }

            if (this.IsReverseDirection == true)
            {
                this.printerViewMdoel.g_objJPNano.ObjectDirection = JPNANO.Direction.RTL;
            }
            Common.CommonMethods.ShowInfoMessage("Object Direction set successfully", "Information");
        }

        public void SetObjectOrientation()
        {
            if (this.IsInverted == true)
            {
                this.printerViewMdoel.g_objJPNano.MessageOrientation = JPNANO.Orientation.Inverted;
            }

            if (this.IsReverse == true)
            {
                this.printerViewMdoel.g_objJPNano.MessageOrientation = JPNANO.Orientation.Reverse;
            }

            if (this.IsNormal == true)
            {
                this.printerViewMdoel.g_objJPNano.MessageOrientation = JPNANO.Orientation.Normal;
            }

            if (this.IsInvertedReverse == true)
            {
                this.printerViewMdoel.g_objJPNano.MessageOrientation = JPNANO.Orientation.InvertedReverse;
            }

            Common.CommonMethods.ShowInfoMessage("Message Orientation set successfully", "Information");
        }

        public bool CanExecute()
        {
            return true;
        }

        public ICommand ObjectDirectionCommand
        {
            get;
            set;
        }

        public ICommand OrientationCommand
        {
            get;
            set;
        }

    }
}
