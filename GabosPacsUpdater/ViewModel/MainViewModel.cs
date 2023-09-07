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
            StopPacsWadoRSCommand = new RelayCommand(StopPacsWadoRS);
            StopPacsDimseCommand = new RelayCommand(StopPacsDimse);
            StopPacsHangfireCommand = new RelayCommand(StopPacsHangfire);
            StartPacsWadoRSCommand = new RelayCommand(StartPacsWadoRS);
            StartPacsDimseCommand = new RelayCommand(StartPacsDimse);
            StartPacsHangfireCommand = new RelayCommand(StartPacsHangfire);
            UpdatePacsWadoRSCommand = new RelayCommand(UpdatesPacsWadoRS);
            UpdatePacsDimseCommand = new RelayCommand(UpdatesPacsDimse);
            UpdatePacsHangfireCommand = new RelayCommand(UpdatesPacsHangfire);

            
            //StatusLight();
            //UpdateLight();
            //SetButtons();
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
            set 
            { 
                _statusPacsWadoRS = value; 
                //ButtonStatusWadoRS = value == Brushes.Green ? "Zatrzymaj" : value == Brushes.Orange ? "Uruchom" : value == Brushes.Gray && UpdatePacsWadoRS == Brushes.Green ? "Zainstaluj" : "  ...  ";
                OnPropertyChanged(nameof(StatusPacsWadoRS)); 
            }
        }
        public Brush StatusPacsDimse
        {
            get { return _statusPacsDimse; }
            set 
            { 
                _statusPacsDimse = value;
                //ButtonStatusDimse = value == Brushes.Green ? "Zatrzymaj" : value == Brushes.Orange ? "Uruchom" : value == Brushes.Gray && UpdatePacsDimse == Brushes.Green ? "Zainstaluj" : "  ...  ";
                OnPropertyChanged(nameof(StatusPacsDimse)); 
            }
        }
        public Brush StatusPacsHangfire
        {
            get { return _statusPacsHangfire; }
            set 
            { 
                _statusPacsHangfire = value;
                //ButtonStatusHangfire = value == Brushes.Green ? "Zatrzymaj" : value == Brushes.Orange ? "Uruchom" : value == Brushes.Gray && UpdatePacsHangfire == Brushes.Green ? "Zainstaluj" : "  ...  ";
                OnPropertyChanged(nameof(StatusPacsHangfire)); 
            }
        }
        public Brush UpdatePacsWadoRS
        {
            get { return _UpdatePacsWadoRS; }
            set 
            { 
                _UpdatePacsWadoRS = value;
                //ButtonUpdateWadoRS = value == Brushes.Gray ? "Pobierz" : value == Brushes.Orange ? "Aktualizuj" :  "  ...  ";
                OnPropertyChanged(nameof(UpdatePacsWadoRS)); 
            }
        }
        public Brush UpdatePacsDimse
        {
            get { return _UpdatePacsDimse; }
            set 
            { 
                _UpdatePacsDimse = value;
                //ButtonUpdateDimse = value == Brushes.Gray ? "Pobierz" : value == Brushes.Orange ? "Aktualizuj" : "  ...  ";
                OnPropertyChanged(nameof(UpdatePacsDimse));
            }
        }
        public Brush UpdatePacsHangfire
        {
            get { return _UpdatePacsHangfire; }
            set 
            { 
                _UpdatePacsHangfire = value;
                //ButtonUpdateHangfire = value == Brushes.Gray ? "Pobierz" : value == Brushes.Orange ? "Aktualizuj" : "  ...  ";
                OnPropertyChanged(nameof(UpdatePacsHangfire));
            }
        }

        #endregion
        #region Set Light
        private void StatusLight()
        {
            string status = "";
            StatusPacsWadoRS = CommunicationService.CheckStatusServices("GabosPacs", out status)? Brushes.Green: status.Contains("Error: ")? Brushes.Gray : Brushes.Orange;
            StatusPacsDimse = CommunicationService.CheckStatusServices("GabosPacsDIMSE", out status) ? Brushes.Green : status.Contains("Error: ") ? Brushes.Gray : Brushes.Orange;
            StatusPacsHangfire = CommunicationService.CheckStatusServices("GabosPacsHangfireWorker", out status) ? Brushes.Green : status.Contains("Error: ") ? Brushes.Gray : Brushes.Orange;
        }
        private void UpdateLight()
        {
            try
            {
                UpdatePacsWadoRS = UpdateService.IsUpToDate(Properties.Settings.Default.RemotePathWadoRS, Properties.Settings.Default.LocalPathWadoRS) ? Brushes.Green : Brushes.Orange;
            }
            catch 
            {
                UpdatePacsWadoRS = Brushes.Gray;
            }
            try
            {
                UpdatePacsDimse = UpdateService.IsUpToDate(Properties.Settings.Default.RemotePathDimse, Properties.Settings.Default.LocalPathDimse) ? Brushes.Green : Brushes.Orange;
            }
            catch
            {
                UpdatePacsDimse = Brushes.Gray;
            }
            try
            {
                UpdatePacsHangfire = UpdateService.IsUpToDate(Properties.Settings.Default.RemotePathHangfire, Properties.Settings.Default.LocalPathHangfire) ? Brushes.Green : Brushes.Orange;
            }
            catch
            {
                UpdatePacsHangfire = Brushes.Gray;
            }
        }
        #endregion

        #region Buttons
        #region Names
        private string _buttonStatusWadoRS;

        public string ButtonStatusWadoRS
        {
            get { return _buttonStatusWadoRS; }
            set 
            { 
                _buttonStatusWadoRS = value;
                OnPropertyChanged(nameof(ButtonStatusWadoRS));
            }
        }
        private string _buttonStatusDimse;

        public string ButtonStatusDimse
        {
            get { return _buttonStatusDimse; }
            set
            {
                _buttonStatusDimse = value;
                OnPropertyChanged(nameof(ButtonStatusDimse));
            }
        }
        private string _buttonStatusHangfire;

        public string ButtonStatusHangfire
        {
            get { return _buttonStatusHangfire; }
            set
            {
                _buttonStatusHangfire = value;
                OnPropertyChanged(nameof(ButtonStatusHangfire));
            }
        }

        private string _buttonUpdateWadoRS;

        public string ButtonUpdateWadoRS
        {
            get { return _buttonUpdateWadoRS; }
            set
            {
                _buttonUpdateWadoRS = value;
                OnPropertyChanged(nameof(ButtonUpdateWadoRS));
            }
        }
        private string _buttonUpdateDimse;

        public string ButtonUpdateDimse
        {
            get { return _buttonUpdateDimse; }
            set
            {
                _buttonUpdateDimse = value;
                OnPropertyChanged(nameof(ButtonUpdateDimse));
            }
        }
        private string _buttonUpdateHangfire;

        public string ButtonUpdateHangfire
        {
            get { return _buttonUpdateHangfire; }
            set
            {
                _buttonUpdateHangfire = value;
                OnPropertyChanged(nameof(ButtonUpdateHangfire));
            }
        }
        #endregion
        #region Set Names
        private void SetButtons()
        {
            ButtonStatusWadoRS = StatusPacsWadoRS == Brushes.Green ? "Zatrzymaj" : StatusPacsWadoRS == Brushes.Orange ? "Uruchom" : StatusPacsWadoRS == Brushes.Gray && UpdatePacsWadoRS == Brushes.Green ? "Zainstaluj" : "  ...  ";
            ButtonStatusDimse = StatusPacsDimse == Brushes.Green ? "Zatrzymaj" : StatusPacsDimse == Brushes.Orange ? "Uruchom" : StatusPacsDimse == Brushes.Gray && UpdatePacsDimse == Brushes.Green ? "Zainstaluj" : "  ...  ";
            ButtonStatusHangfire = StatusPacsHangfire == Brushes.Green ? "Zatrzymaj" : StatusPacsHangfire == Brushes.Orange ? "Uruchom" : StatusPacsHangfire == Brushes.Gray && UpdatePacsHangfire == Brushes.Green ? "Zainstaluj" : "  ...  ";
            ButtonUpdateWadoRS = UpdatePacsWadoRS == Brushes.Gray ? "Pobierz" : UpdatePacsWadoRS == Brushes.Orange ? "Aktualizuj" : "  ...  ";
            ButtonUpdateDimse = UpdatePacsDimse == Brushes.Gray ? "Pobierz" : UpdatePacsDimse == Brushes.Orange ? "Aktualizuj" : "  ...  ";
            ButtonUpdateHangfire = UpdatePacsHangfire == Brushes.Gray ? "Pobierz" : UpdatePacsHangfire == Brushes.Orange ? "Aktualizuj" : "  ...  ";
        }
        #endregion
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
