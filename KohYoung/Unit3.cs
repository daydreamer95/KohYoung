using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace KohYoung
{
    public class Unit3
    {
        public struct Person
        {
            public int id;
            public string first_name;
            public string last_name;
        }

        public void Process()
        {
            
        }
        /// <summary>
        /// Read txt file and return Person
        /// </summary>
        /// <param name="pathFile">Path txt</param>
        /// <returns></returns>
        public List<Person> Read(string pathFile)
        {
            string fileText = "";
            List<Person> result = new List<Person>();

            var fileStream = new FileStream(pathFile, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                fileText = streamReader.ReadToEnd();
            }
            string[] stringSeparators = new string[] { "\r\n" };
            string[] lines = fileText.Split(stringSeparators, StringSplitOptions.None);
            if (lines != null)
                foreach (var line in lines)
                {
                    string[] properties = line.Split(',');
                    Person newPerson = new Person();
                    Int32.TryParse(properties[0],out newPerson.id) ;
                    newPerson.first_name = properties[1];
                    newPerson.last_name = properties[2];
                    result.Add(newPerson);
                }
            return result;
        }

        public void Save(List<Person> lstPerson,string pathToSave)
        {
            string stringToSave = string.Join("\r\n",lstPerson.Select(person => $"{person.id}, {person.first_name}, {person.last_name}").ToArray());
            if (!String.IsNullOrEmpty(stringToSave))
            {
                //check directory folder
                if (Directory.Exists(pathToSave))
                    ProcessSaveFile(stringToSave, pathToSave);
                else
                    ProcessNotExistFolder(stringToSave, pathToSave);
            }
            else
            {
                Console.WriteLine("Check your list Person!");
            }
        }
        private void ProcessSaveFile(string textToSave,string pathToSave)
        {
            string pathFileToSave = Path.Combine(pathToSave, "result.txt");
            if (File.Exists(pathFileToSave))
            {
                pathFileToSave = Path.Combine(pathToSave, "result" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
            }
            FileStream fParameter = new FileStream(pathFileToSave, FileMode.Create, FileAccess.Write);
            StreamWriter m_WriterParameter = new StreamWriter(fParameter);
            m_WriterParameter.BaseStream.Seek(0, SeekOrigin.End);
            m_WriterParameter.Write(textToSave);
            m_WriterParameter.Flush();
            m_WriterParameter.Close();
        }

        private void ProcessNotExistFolder(string stringToSave,string pathToSave)
        {
            Console.WriteLine("Cannot found dictory to save file.Creating new one!");
            Directory.CreateDirectory(pathToSave);
            var dInfo = new DirectoryInfo(pathToSave);
            var dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);

            ProcessSaveFile(stringToSave, pathToSave);
        }
    }
}
