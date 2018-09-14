using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.CorsoParte2.ViewModels.Messages
{
    public class BrowseFileMessage
    {
        public string Title { get; set; } = "Seleziona un file...";
        public string Filter { get; set; } = "Immagini PNG (*.png)|*.png|Immagini JPEG (*.jpg)|*.jpg";
        public bool MultiSelect { get; set; } = false;
        public Action<string> BrowseCompleted { get; set; }
    }
}