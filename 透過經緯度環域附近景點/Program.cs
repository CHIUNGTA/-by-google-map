using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace 透過經緯度環域附近景點
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("請輸入轉換的文字檔(.csv)之連結地址");
            var FileAddress = Console.ReadLine();
            Console.WriteLine("請輸入搜尋範圍半徑(m)");
            var radius = Console.ReadLine();
            Console.WriteLine("請輸入搜尋類型，如bus_station或是restaurant");
            string type = Console.ReadLine();
            StreamWriter SuccessFile = new StreamWriter("D:\\經緯度鄰近之景點.txt");
            System.Text.Encoding encode =System.Text.Encoding.GetEncoding("big5");
            var file = new System.IO.StreamReader(FileAddress,encode);
            string Line = string.Empty;
            Line = file.ReadLine();
            SuccessFile.WriteLine(Line + "," + "查詢結果" + "," + "X" + "," + "Y");
            Line = Get_local_information(SuccessFile, file , radius,type);
            SuccessFile.Close();
            Console.WriteLine("轉檔已完成，請至D:\\查看檔案");
            Console.ReadLine();
        }

        private static string Get_local_information(StreamWriter SuccessFile, StreamReader file ,string radius,string type)
        {
            string Line;
            while ((Line = file.ReadLine()) != null)
            {
                try
                {
                    string[] ReadLine_Array = Line.Split(',');
                    string APIUrl = string.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0}&radius={1}&type={2}&key=AIzaSyAf8uwaEFaa7-x153Q2XoNuNcn68ARjTxg&language=zh-tw", ReadLine_Array[2] + "," + ReadLine_Array[1],radius,type);
                    var buffer = new WebClient().DownloadData(APIUrl);
                    string data = Encoding.UTF8.GetString(buffer);
                    var obj = JsonConvert.DeserializeObject<Rootobject>(data);
                    foreach (var x in obj.results)
                    {
                        Console.WriteLine(ReadLine_Array[0] + "查詢結果為...." + x.name + "," + x.geometry.location.lng + "," + x.geometry.location.lat);
                        SuccessFile.WriteLine(ReadLine_Array[0] + "," + ReadLine_Array[1] + "," + ReadLine_Array[2] + "," + ReadLine_Array[3] + "," +radius + "," + type + "," + x.name + "," + x.geometry.location.lng + "," + x.geometry.location.lat);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("該目標無對應之景點");
                    Console.ReadLine();
                }
            }

            return Line;
        }
    }
}
