using GalaSoft.MvvmLight.CommandWpf;
using MGS.CorsoParte2.DomainModel;
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

        public RelayCommand AddDoorCommand { get; set; }

        public MainMenuViewModel()
        {
            Init();

            this.AddDoorCommand = new RelayCommand(() => {
                this.Doors.Insert(0, new Door());
            });
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
            this.IsBusy = false;
        }

        public override void Init2()
        {
            
        }
    }
}