using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLibrary
{
    public class Logger
    {
        public void WriteMessage(string FileName, String Message)
        {
            try
            {


                if (File.Exists(FileName))
                {
                    System.IO.StreamWriter file =
                     new System.IO.StreamWriter(FileName, true);
                    file.WriteLine(Message);
                    file.Close();
                }
                else
                {
                    // Write the string to a file.
                    System.IO.StreamWriter file = new System.IO.StreamWriter(FileName);
                    file.WriteLine(Message);
                    file.Close();
                }
            }
            catch
            {

            }

        }


    }
}
