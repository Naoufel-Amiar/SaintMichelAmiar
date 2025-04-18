using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintMichel.Model
{
    public class RootUserMessages
    {
        public bool exists { get; set; }
        public bool verified { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
}
