using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 經緯度轉地址
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("請輸入轉換文字檔(.csv)之連結地址");
            var add = Console.ReadLine();
            StreamWriter successfile = new StreamWriter("D:\\經緯度轉地址檔.txt");

            again:
            try
            {
                //ReadTxtFile
                var file = new System.IO.StreamReader(add);
                //Create Success File & Fail File
                string line=string.Empty;
                line = file.ReadLine();
                successfile.WriteLine(line+","+"Addder");
                int y = 1;
                while ((line = file.ReadLine()) != null && y<=10)
                {
                    string[] ReadLine_Array = line.Split(',');
                    string APIUrl = string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&&sensor=false&language=zh-tw", ReadLine_Array[2]+","+ReadLine_Array[1]);
                    var buffer = new WebClient().DownloadData(APIUrl);
                    var json = Encoding.UTF8.GetString(buffer);
                    var obj = JsonConvert.DeserializeObject<Rootobject>(json);
                    
                    Console.WriteLine(ReadLine_Array[0] + "," + obj.results[0].formatted_address);
                    successfile.WriteLine(ReadLine_Array[0]+ "," + ReadLine_Array[1] + "," + ReadLine_Array[2] + "," + ReadLine_Array[3] + "," + obj.results[0].formatted_address);
                    y++;
                }
                successfile.Close();
                Console.WriteLine("OK，請至D:查看");
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("請輸入正確目標地址");
                goto again;
            }
        }

    }
}