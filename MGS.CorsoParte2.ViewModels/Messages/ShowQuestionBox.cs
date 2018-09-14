using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.CorsoParte2.ViewModels.Messages
{
    public class ShowQuestionBox : ShowMessageBox
    {
        public Action Yes { get; set; }
        public Action No { get; set; }
        public Action Cancel { get; set; }

        public ShowQuestionBox(string title, string message)
            : base(title, message)
        {

        }
    }
}