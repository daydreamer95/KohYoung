using KohYoung.KohYoung;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KohYoung.Unit3;

namespace KohYoung
{
    class Program
    {
        static void Main(string[] args)
        {
            Unit3 unit3 = new Unit3();
            List<Person> lstPerson = unit3.Read("D:\\kohyoungunit3.txt");
            unit3.Save(lstPerson, "D:\\TESTKOHYOUNG");
        }
    }
}
