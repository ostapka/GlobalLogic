using System;
using System.Configuration;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;

namespace Test
{
    class Program
    {
        private static string fileName = ConfigurationManager.AppSettings.Get("fileName");
        private static DirectoriesInfo info;

        static void Main(string[] args)
        {
            Run();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        /// <summary>
        /// Method runs program
        /// </summary>
        public static void Run()
        {
            int i = 0;
            while (i == 0)
            {
                try
                {
                    Console.Write("Input path: ");
                    string path = Console.ReadLine();
                    info = new DirectoriesInfo(path);
                    Select();
                    i = 1;
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    Console.WriteLine("Try again...");
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    Console.WriteLine("Try again...");
                }
            }
        }

        /// <summary>
        /// Method provides selection between JsonSerializer or Newtonsoft
        /// </summary>
        public static void Select()
        {
            Console.Write("What do you want to use JsonSerializer or Newtonsoft? ");
            string answer = Console.ReadLine();
            if (answer == "JsonSerializer")
            {
                Serialize();
            }
            else if (answer == "Newtonsoft")
            {
                NewtonsoftSerialize();
            }
            else
            {
                throw new FormatException("Your answer is incorrect. You must specify JsonSerializer or Newtonsoft.");
            }
        }

        /// <summary>
        /// Serialize with help JsonSerializer
        /// </summary>
        public static void Serialize()
        {
            Stream file = new FileStream(fileName, FileMode.Create);
            DataContractJsonSerializer ser = new
                                           DataContractJsonSerializer(typeof(DirectoriesInfo));
            ser.WriteObject(file, info);
            file.Close();
        }

        /// <summary>
        /// Serialize with help Newtonsoft.Json
        /// </summary>
        public static void NewtonsoftSerialize()
        {
            using (StreamWriter file = new StreamWriter(fileName))
            {
                string serialized = JsonConvert.SerializeObject(info, Formatting.Indented);
                file.WriteLine(serialized);
            }
        }
    }
}
