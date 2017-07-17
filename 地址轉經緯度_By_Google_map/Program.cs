using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace 地址轉經緯度_By_Google_map
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("請輸入轉換文字檔(.txt)之連結地址");
            string FileAddress = Console.ReadLine();
            try
            {
                //ReadTxtFile
                System.Text.Encoding encode = System.Text.Encoding.GetEncoding("big5");
                StreamReader file = new StreamReader(FileAddress, encode);
                //Create Success File & Fail File
                StreamWriter SuccessFile = new StreamWriter("D:\\轉檔完成檔-成功.txt");
                StreamWriter FailFile = new StreamWriter("D:\\轉檔完成檔-失敗.txt");
                string line;
                var Id = 1;
                while ((line = file.ReadLine()) != null && Id <= 200)
                {
                    //Rootobject obj = NewMethod(line);
                    string APIUrl = string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&&sensor=false&language=zh-tw", line);
                    var buffer = new WebClient().DownloadData(APIUrl);
                    var json = Encoding.UTF8.GetString(buffer);
                    var obj = JsonConvert.DeserializeObject<Rootobject>(json);
                    try
                    {
                        Console.WriteLine(Id + ":" + line + "," + obj.results[0].geometry.location.lat + "," + obj.results[0].geometry.location.lng);
                        SuccessFile.WriteLine(Id + "," + line + "," + obj.results[0].geometry.location.lat + "," + obj.results[0].geometry.location.lng);
                    }
                    catch
                    {
                        Console.WriteLine($"第{Id}筆資料錯誤:" + line);
                        FailFile.WriteLine($"第{Id}筆資料錯誤" + line);
                    }
                    Id++;
                }
                SuccessFile.Close();
                FailFile.Close();
                Console.WriteLine("OK，請至D:查看");
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("請輸入正確目標地址");
            }
        }

        //private static Rootobject NewMethod(string line)
        //{

        //    //return obj;
        //}
    }
}
