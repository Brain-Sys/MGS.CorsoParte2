using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MGS.CorsoParte2.DomainModel;
using MGS.CorsoParte2.ViewModels.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.CorsoParte2.ViewModels
{
    public class MainMenuViewModel : ApplicationViewModelBase
    {
        public ObservableCollection<Door> Doors { get; set; }

        private Door selectedDoor;
        public Door SelectedDoor
        {
            get { return selectedDoor; }
            set
            {
                selectedDoor = value;
                base.RaisePropertyChanged();
                base.RaisePropertyChanged(nameof(SelectedDoorDetails));
            }
        }

        public string SelectedDoorDetails
        {
            get
            {
                if (this.SelectedDoor != null)
                {
                    return $"{this.SelectedDoor.Width}cm X {this.SelectedDoor.Height}cm";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private string currentExporting;
        public string CurrentExporting
        {
            get { return currentExporting; }
            set
            {
                currentExporting = value;
                base.RaisePropertyChanged();
            }
        }

        public RelayCommand AddDoorCommand { get; set; }
        public RelayCommand DeleteDoorCommand { get; set; }
        public RelayCommand<Door> AttachPhotoCommand { get; set; }
        public RelayCommand ExportAllCommand { get; set; }
        public RelayCommand CancelExportAllCommand { get; set; }

        public MainMenuViewModel()
        {
            Init();

            this.AddDoorCommand = new RelayCommand(addDoorCommandExecute);
            this.DeleteDoorCommand = new RelayCommand(deleteDoorCommandExecute);
            this.AttachPhotoCommand = new RelayCommand<Door>(attachPhotoCommandExecute);
            this.ExportAllCommand = new RelayCommand(exportAllCommandExecute);
            this.CancelExportAllCommand = new RelayCommand(cancelExportAllCommandExecute);
        }

        private void cancelExportAllCommandExecute()
        {
            if (!cts.IsCancellationRequested)
            {
                cts.Cancel();
            }
        }

        private async void exportAllCommandExecute()
        {
            this.IsBusy = true;

            await Task.Run(async () =>
            {
                var clone = new List<Door>(this.Doors);

                foreach (var d in clone)
                {
                    this.CurrentExporting = d.Model;
#if DEBUG
                    await Task.Delay(200);
#endif

                    string filename = $"E:\\Export\\{d.Model}.dat";
                    File.WriteAllText(filename, $"{d.Price}");

                    if (cts.Token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, cts.Token);

            this.CurrentExporting = string.Empty;
            this.IsBusy = false;
        }

        private void attachPhotoCommandExecute(Door parameter)
        {
            var msg = new BrowseFileMessage();
            msg.BrowseCompleted = (string filename) =>
            {
                parameter.Photo = filename;
            };
            Messenger.Default.Send<BrowseFileMessage>(msg);
        }

        private void deleteDoorCommandExecute()
        {
            ShowQuestionBox msg = new ShowQuestionBox("Conferma!",
                "Sei sicuro di voler cancellare?");
            msg.Yes = () =>
            {
                if (this.Doors.Contains(this.SelectedDoor))
                {
                    this.Doors.Remove(this.SelectedDoor);
                }
            };

            Messenger.Default.Send<ShowQuestionBox>(msg);
        }

        private void addDoorCommandExecute()
        {
            bool existsEmptyRow = this.Doors.Any
                (d => d.Price == 0 && string.IsNullOrEmpty(d.Model));

            if (existsEmptyRow == false)
            {
                this.Doors.Insert(0, new Door());
            }
            else
            {
                // Message Box all'utente
                Messenger.Default.Send<ShowMessageBox>(
                    new ShowMessageBox("Errore!",
                    "Devi prima compilare la\r\nriga vuota già presente"));
            }
        }

        public async override void Init()
        {
            this.IsBusy = true;
            var list = new List<Door>();

            await Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Door d = new Door();
                    d.Height = base.Random.Next(100, 210);
                    d.Width = base.Random.Next(100, 210);
                    d.Price = base.Random.Next(100, 10000);
                    d.Model = $"Modello {base.Random.Next(1, 10000000)}";
                    list.Add(d);
                }
            });

            this.Doors = new ObservableCollection<Door>(list);
            base.RaisePropertyChanged(nameof(Doors));

            // this.SelectedDoor = this.Doors[5];

            this.IsBusy = false;
        }

        public override void Init2()
        {

        }
    }
}