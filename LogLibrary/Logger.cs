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
        /// <summary>
        /// This will write a message that is being sent to it into a specified text file
        
        /// </summary>
        /// 
        /// <param name="FileName">
        ///  the name of the file to save into
        /// </param>
        /// 
        /// <param name="Message">
        ///  the line that gets saved into the text file
        /// </param>
        
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
