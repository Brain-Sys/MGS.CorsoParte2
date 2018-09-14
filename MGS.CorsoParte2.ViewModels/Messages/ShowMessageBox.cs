using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.CorsoParte2.ViewModels.Messages
{
    public class ShowMessageBox
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public ShowMessageBox(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }
    }
}