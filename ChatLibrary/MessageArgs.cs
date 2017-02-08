using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLibrary
{
    public class MessageArgs
    {

        public MessageArgs(string inMessage)
        {
            message = inMessage;
        }

        public string message { get; }
    
    }
}
