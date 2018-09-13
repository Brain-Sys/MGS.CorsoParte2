using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS.CorsoParte2.ViewModels.Messages
{
    public class OpenNewViewMessage
    {
        public string ViewName { get; set; }
        public bool Modal { get; set; }
        public bool Maximized { get; set; }
    }
}