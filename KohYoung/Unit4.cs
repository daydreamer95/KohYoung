using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KohYoung
{
    public class Unit4
    {
        const string path = "D:\\TESTKOHYOUNG\\Unit4KohYoung.txt";
        const int tableWidth = 73;

        public void Process()
        {
            //read file
            string textFile = ReadFileToString(path);
            PrintOnScreen(textFile);
        }

        public string ReadFileToString(string pathFileToRead)
        {
            string fileText = "";
            var fileStream = new FileStream(pathFileToRead, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                fileText = streamReader.ReadToEnd();
            }
            string[] stringSeparators = new string[] { "\r\n" };
            return fileText;
        }

        private void PrintOnScreen(string textFile)
        {
            string[] stringSeparators = new string[] { "\r\n" };
            string[] lines = textFile.Split(stringSeparators, StringSplitOptions.None);
            if (lines != null)
            {
                string[] header = lines[0].Split(',');
                PrintRow(header);
                PrintLine();
                //Remove header
                lines = lines.Skip(1).ToArray();
                //Print body value
                foreach (var line in lines)
                {
                    string[] properties = line.Split('|');
                    PrintRow(properties);
                    PrintLine();
                }
            }
        }

        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
