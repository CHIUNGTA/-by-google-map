using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Read
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("輸入地址:");
            var add = Console.ReadLine();
            try
            {
                using (StreamReader SR = new StreamReader(add))

                {
                    string Line;
                    while ((Line = SR.ReadLine()) != null)
                    {
                        string[] ReadLine_Array = Line.Split(',');
                        Console.WriteLine(ReadLine_Array[1] + "," + ReadLine_Array[2] + "," + ReadLine_Array[3] + "," + ReadLine_Array[4]);
                        //這邊可以自行發揮

                    }
                    Console.ReadLine();
                }
            }
            catch (IOException)
            { }
            catch (NullReferenceException)
            { }
            catch (FormatException)
            { }

        }
    }
}
