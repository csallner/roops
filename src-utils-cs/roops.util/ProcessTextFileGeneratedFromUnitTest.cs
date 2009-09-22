// Author: Mainul
// Description: This program will be used to generate a formatted text file from the Pex generated unit test results 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Roops
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFileFullPath = "C:\\LinearWithoutOverflow.txt";
            process(inputFileFullPath);
        }

        private static void process(string inputFileFullPath)
        {
            string outputFileFullPath = Path.GetDirectoryName(inputFileFullPath) + Path.GetFileNameWithoutExtension(inputFileFullPath) + "_out" + Path.GetExtension(inputFileFullPath);

            System.IO.StreamReader log_in;
            System.IO.StreamWriter log_out;
            string inputLine;
            char[] separators = { '-' };
            string[] parts;
            string methodName = "";
            int numberOfGoals = 0, goalIndex = 0;
            Dictionary<string, List<int>> methods = new Dictionary<string, List<int>>();

            log_in = new System.IO.StreamReader(inputFileFullPath);

            try
            {
                log_out = new System.IO.StreamWriter(outputFileFullPath, true);
            }
            catch (System.IO.IOException exc)
            {
                System.Console.WriteLine("Error: " + exc.Message + "Cannot open file.");
                return;
            }

            System.Console.SetIn(log_in);
            System.Console.SetOut(log_out);

            while (true)
            {
                inputLine = Console.ReadLine();
                if (inputLine == null)
                    break;

                parts = inputLine.Split(separators, 3, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 0)
                    methodName = parts[0].Split(':')[1].Trim();

                if (parts.Length > 1)
                    Int32.TryParse(parts[1].Split(':')[1].Trim(), out numberOfGoals);

                if (parts.Length > 2)
                    Int32.TryParse(parts[2].Split(':')[1].Trim(), out goalIndex);

                if (!methods.ContainsKey(methodName))
                {
                    List<int> goals = new List<int>();
                    goals.Add(numberOfGoals);
                    goals.Add(goalIndex);
                    methods.Add(methodName, goals);
                }

                else
                {
                    if (!methods[methodName].Contains(goalIndex))
                    {
                        methods[methodName].Add(goalIndex);
                    }
                }

                //System.Console.WriteLine(inputLine);
            }

            foreach (string method in methods.Keys)
            {
                Console.WriteLine("Goals " + (methods[method].Count - 1) + "/" + methods[method][0] + " is reached for method - " + method);
            }

            log_out.Close();
            log_in.Close();


        }

        private void WriteToFile(string textToWrite)
        {
            string outputFileFullPath = "c:\\logfile.txt";
            System.IO.StreamWriter log_out;

            try
            {
                log_out = new System.IO.StreamWriter(outputFileFullPath, false);
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
