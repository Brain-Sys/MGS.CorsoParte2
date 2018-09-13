﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.CorsoParte2.ViewModels
{
    public class ApplicationViewModelBase : ViewModelBase
    {
        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                base.RaisePropertyChanged();
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value;
                base.RaisePropertyChanged();
                base.RaisePropertyChanged(nameof(IsNotBusy));
            }
        }

        public bool IsNotBusy
        {
            get
            {
                return !this.IsBusy;
            }
        }
    }
}