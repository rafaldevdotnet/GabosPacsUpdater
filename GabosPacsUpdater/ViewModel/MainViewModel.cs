using GabosPacsUpdater.Commands;
using GabosPacsUpdater.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GabosPacsUpdater.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            UpdateLight();


            StopPacsWadoRSCommand = new RelayCommand(StopPacsWadoRS);
            StopPacsDimseCommand = new RelayCommand(StopPacsDimse);
            StopPacsHangfireCommand = new RelayCommand(StopPacsHangfire);
            StartPacsWadoRSCommand = new RelayCommand(StartPacsWadoRS);
            StartPacsDimseCommand = new RelayCommand(StartPacsDimse);
            StartPacsHangfireCommand = new RelayCommand(StartPacsHangfire);
            UpdatePacsWadoRSCommand = new RelayCommand(UpdatesPacsWadoRS);
            UpdatePacsDimseCommand = new RelayCommand(UpdatesPacsDimse);
            UpdatePacsHangfireCommand = new RelayCommand(UpdatesPacsHangfire);
        }

        #region Light

        private Brush _statusPacsWadoRS;
        private Brush _statusPacsDimse;
        private Brush _statusPacsHangfire;
        private Brush _UpdatePacsWadoRS;
        private Brush _UpdatePacsDimse;
        private Brush _UpdatePacsHangfire;

        public Brush StatusPacsWadoRS
        {
            get { return _statusPacsWadoRS; }
            set { _statusPacsWadoRS = value; OnPropertyChanged(nameof(StatusPacsWadoRS)); }
        }
        public Brush StatusPacsDimse
        {
            get { return _statusPacsDimse; }
            set { _statusPacsDimse = value; OnPropertyChanged(nameof(StatusPacsDimse)); }
        }
        public Brush StatusPacsHangfire
        {
            get { return _statusPacsHangfire; }
            set { _statusPacsHangfire = value; OnPropertyChanged(nameof(StatusPacsHangfire)); }
        }
        public Brush UpdatePacsWadoRS
        {
            get { return _UpdatePacsWadoRS; }
            set { _UpdatePacsWadoRS = value; OnPropertyChanged(nameof(UpdatePacsWadoRS)); }
        }
        public Brush UpdatePacsDimse
        {
            get { return _UpdatePacsDimse; }
            set { _UpdatePacsDimse = value; OnPropertyChanged(nameof(UpdatePacsDimse)); }
        }
        public Brush UpdatePacsHangfire
        {
            get { return _UpdatePacsHangfire; }
            set { _UpdatePacsHangfire = value; OnPropertyChanged(nameof(UpdatePacsHangfire)); }
        }

        #endregion
        #region Set Light
        private void StatusLight()
        {

        }
        private void UpdateLight()
        {
            try
            {
                UpdatePacsWadoRS = UpdateService.IsUpToDate(Properties.Settings.Default.RemotePathWadoRS, Properties.Settings.Default.LocalPathWadoRS) ? Brushes.Green : Brushes.Red;
            }
            catch 
            {
                UpdatePacsWadoRS = Brushes.Gray;
            }
            try
            {
                UpdatePacsDimse = UpdateService.IsUpToDate(Properties.Settings.Default.RemotePathDimse, Properties.Settings.Default.LocalPathDimse) ? Brushes.Green : Brushes.Red;
            }
            catch
            {
                UpdatePacsDimse = Brushes.Gray;
            }
            try
            {
                UpdatePacsHangfire = UpdateService.IsUpToDate(Properties.Settings.Default.RemotePathHangfire, Properties.Settings.Default.LocalPathHangfire) ? Brushes.Green : Brushes.Red;
            }
            catch
            {
                UpdatePacsHangfire = Brushes.Gray;
            }
        }   
        #endregion

        #region Buttons

        #region Interface Commands
        public ICommand StopPacsWadoRSCommand { get; set; }
        public ICommand StopPacsDimseCommand { get; set; }
        public ICommand StopPacsHangfireCommand { get; set; }
        public ICommand StartPacsWadoRSCommand { get; set; }
        public ICommand StartPacsDimseCommand { get; set; }
        public ICommand StartPacsHangfireCommand { get; set; }
        public ICommand UpdatePacsWadoRSCommand { get; set; }
        public ICommand UpdatePacsDimseCommand { get; set; }
        public ICommand UpdatePacsHangfireCommand { get; set; }
        #endregion
        #region Implementation Commands
        private void StopPacsWadoRS(object obj)
        {
            string status = "";
            if (CommunicationService.StopServices("GabosPacsWadoRS", out status)) StatusPacsWadoRS = Brushes.Red;
        }
        private void StopPacsDimse(object obj)
        {
            string status = "";
            if (CommunicationService.StopServices("GabosPacsDIMSE", out status)) StatusPacsDimse = Brushes.Red;
        }
        private void StopPacsHangfire(object obj)
        {
            string status = "";
            if (CommunicationService.StopServices("GabosPacsHangfire", out status)) StatusPacsHangfire = Brushes.Red;
        }
        private void StartPacsWadoRS(object obj)
        {
            string status = "";
            if (CommunicationService.StartServices("GabosPacsWadoRS", out status)) StatusPacsWadoRS = Brushes.Green;
        }
        private void StartPacsDimse(object obj)
        {
            string status = "";
            if (CommunicationService.StartServices("GabosPacsDIMSE", out status)) StatusPacsDimse = Brushes.Green;
        }
        private void StartPacsHangfire(object obj)
        {
            string status = "";
            if (CommunicationService.StartServices("GabosPacsHangfire", out status)) StatusPacsHangfire = Brushes.Green;
        }
        private void UpdatesPacsWadoRS(object obj)
        {
            UpdateService.UpdatePacs(Properties.Settings.Default.RemotePathWadoRS, Properties.Settings.Default.LocalPathWadoRS);
        }
        private void UpdatesPacsDimse(object obj)
        {
            UpdateService.UpdatePacs(Properties.Settings.Default.RemotePathDimse, Properties.Settings.Default.LocalPathDimse);
        }
        private void UpdatesPacsHangfire(object obj)
        {
            UpdateService.UpdatePacs(Properties.Settings.Default.RemotePathHangfire, Properties.Settings.Default.LocalPathHangfire);
        }
        #endregion

        #endregion




        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                               new PropertyChangedEventArgs(propertyName));
        }
    }
}
