using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace roops.core.bv32.linear.noex.gods
{
    class PexHelper
    {
        public static void WriteToFile(string textToWrite)
        {
            string outputFileFullPath = "c:\\PexOutput.txt";
            System.IO.StreamWriter log_out;

            try
            {
                log_out = new System.IO.StreamWriter(outputFileFullPath, true);
            }
            catch (System.IO.IOException exc)
            {
                System.Console.WriteLine("Error: " + exc.Message + "Cannot open file.");
                return;
            }

            // Direct standard output to the log file. 
            System.Console.SetOut(log_out);
            System.Console.WriteLine(textToWrite);
            log_out.Close();
        }
    }
}
