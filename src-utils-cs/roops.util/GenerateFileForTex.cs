using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Latex
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length >= 3)
            {
                if (!File.Exists(args[0]) || !File.Exists(args[1]))
                {
                    Console.WriteLine("Invalid file specified" + args[0] + args[1]);
                }
                else
                {
                    //BuildLatexTableFile("c:\\PexOutput_out.txt", "c:\\DscOutput.txt", "c:\\table1.tex");
                    BuildLatexTableFile(args[0], args[1], args[2]);
                }
            }

            else
            {
                Console.WriteLine("Incorrect arguments.\n");
            }
        }

        private static void BuildLatexTableFile(string pexInputFileFullPath, string dscInputFileFullPath, string outputFileFullPath)
        {
            System.IO.StreamReader pex_in, dsc_in;
            System.IO.StreamWriter table_out;

            pex_in = new System.IO.StreamReader(pexInputFileFullPath);
            dsc_in = new System.IO.StreamReader(dscInputFileFullPath);

            try
            {
                table_out = new System.IO.StreamWriter(outputFileFullPath, false);
            }
            catch (System.IO.IOException exc)
            {
                System.Console.WriteLine("Error: " + exc.Message + "Cannot open file.");
                return;
            }
            
            System.Console.SetIn(pex_in);
            Dictionary<string, List<int>> pexData = processPexInputFiletoBuildData();
            pex_in.Close();
            
            System.Console.SetIn(dsc_in);
            Dictionary<string, List<int>> dscData = processDscInputFiletoBuildData(); 
            dsc_in.Close();
            
            System.Console.SetOut(table_out);
            System.Console.WriteLine("\\begin{table}");
            System.Console.WriteLine("{Table: " + Path.GetFileNameWithoutExtension(outputFileFullPath) + "}");
            System.Console.WriteLine("\\begin{center}");
            System.Console.WriteLine("\\begin{tabular}{c|c|c|c}");
            System.Console.WriteLine("\\hline");
            Console.WriteLine("\n\n");

            Console.WriteLine(String.Format("{0,-50}     &       Goals       &  Number of Goals  &  Number of Goals       \\\\", "MethodName(parameter list)"));
            Console.WriteLine(String.Format("{0,-50}     &                   &  reached by PEX   &  reached by DSC        \\\\\\hline\\hline", ""));
            Console.WriteLine("\n\n");

            foreach (string method in pexData.Keys)
            {
                if (dscData.ContainsKey(method))
                {
                    Console.WriteLine(String.Format("{0,-50}     &       {1,5}       &       {2,5}       &       {3,5}             \\\\", method.Length > 50 ? method.Substring(0,47)+"..." : method, pexData[method][0], pexData[method][1], dscData[method][1]));
                }
            }

            Console.WriteLine("\n\n");
            System.Console.WriteLine("\\hline");
            System.Console.WriteLine("\\end{tabular}");
            System.Console.WriteLine("\\end{center}");
            System.Console.WriteLine("\\label{table-failed}");
            System.Console.WriteLine("\\end{table}");

            table_out.Close();
        }

        private static Dictionary<string, List<int>> processPexInputFiletoBuildData()
        {
            string inputLine;
            char[] separators = { '-' };
            string[] parts;
            string methodName = "";
            int numberOfGoals = 0, goalIndex = 0;
            Dictionary<string, List<int>> methods = new Dictionary<string, List<int>>();

            while (true)
            {
                inputLine = Console.ReadLine();
                if (inputLine == null)
                    break;

                parts = inputLine.Split(separators, 4, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 1)
                {
                    Int32.TryParse(parts[1].Split('/')[0].Trim(), out goalIndex);
                    Int32.TryParse(parts[1].Split('/')[1].Trim(), out numberOfGoals);
                }

                if (parts.Length > 3)
                {
                    methodName = parts[3].Trim().Split('(')[0].Trim();
                    // methodName = parts[3].Trim(); // -- for overloading Methods

                    if (!methods.ContainsKey(methodName))
                    {
                        List<int> goals = new List<int>();
                        goals.Add(numberOfGoals);
                        goals.Add(goalIndex);
                        methods.Add(methodName, goals);
                    }
                }

            }

            return methods;
        }



        private static Dictionary<string, List<int>> processDscInputFiletoBuildData()
        {
            string inputLine;
            char[] separators = { ' ' };
            string[] parts;
            string methodName = "";
            int numberOfGoals = 0, goalIndex = 0;
            Dictionary<string, List<int>> methods = new Dictionary<string, List<int>>();

            while (true)
            {
                inputLine = Console.ReadLine();
                if (inputLine == null)
                    break;

                parts = inputLine.Split(separators, 3, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 1)
                {
                    methodName = parts[1].Trim();
                }

                if (parts.Length > 2)
                {
                    Int32.TryParse(parts[2].Trim(), out goalIndex);
                    // Int32.TryParse(parts[2].Trim(), out numberOfGoals);

                    if (!methods.ContainsKey(methodName))
                    {
                        List<int> goals = new List<int>();
                        goals.Add(numberOfGoals);       // always zero for now
                        goals.Add(goalIndex);
                        methods.Add(methodName, goals);
                    }
                }

            }

            return methods;
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
                log_out = new System.IO.StreamWriter(outputFileFullPath, false);
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
                Console.WriteLine("Goals - " + (methods[method].Count - 1) + "/" + methods[method][0] + " - is reached for method - " + method);
            }

            log_out.Close();
            log_in.Close();


        }

 
    }
}
