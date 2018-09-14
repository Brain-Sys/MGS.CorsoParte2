using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.CorsoParte2.DomainModel
{
    public class Door : DomainObject
    {
        private string model;
        public string Model
        {
            get { return model; }
            set { model = value;
                base.RaisePropertyChanged(nameof(Model));
            }
        }

        private double height;
        public double Height
        {
            get { return height; }
            set { height = value;
                base.RaisePropertyChanged(nameof(Height));
            }
        }

        private double width;
        public double Width
        {
            get { return width; }
            set
            {
                width = value;
                base.RaisePropertyChanged(nameof(Width));
            }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set { price = value;
                base.RaisePropertyChanged(nameof(Price));
            }
        }

        private string photo;
        public string Photo
        {
            get { return photo; }
            set { photo = value;
                base.RaisePropertyChanged(nameof(Photo));
            }
        }

        public override string ToString()
        {
            return this.Model;
        }
    }
}