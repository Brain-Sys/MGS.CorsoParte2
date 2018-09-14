using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MGS.CorsoParte2.DomainModel;
using MGS.CorsoParte2.ViewModels.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            set { selectedDoor = value;
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

        public RelayCommand AddDoorCommand { get; set; }
        public RelayCommand DeleteDoorCommand { get; set; }

        public MainMenuViewModel()
        {
            Init();

            this.AddDoorCommand = new RelayCommand(addDoorCommandExecute);
            this.DeleteDoorCommand = new RelayCommand(deleteDoorCommandExecute);
        }

        private void deleteDoorCommandExecute()
        {
            ShowQuestionBox msg = new ShowQuestionBox("Conferma!",
                "Sei sicuro di voler cancellare?");
            msg.Yes = () => {
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

            await Task.Run(() => {
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