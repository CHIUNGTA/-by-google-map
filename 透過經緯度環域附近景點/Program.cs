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
            var add = Console.ReadLine();
            StreamWriter SuccessFile = new StreamWriter("D:\\經緯度鄰近之景點.txt");
            var file = new System.IO.StreamReader(add);
            string Line = string.Empty;
            Line = file.ReadLine();
            SuccessFile.WriteLine(Line + "," + "查詢結果" + "," + "X" + "," + "Y");
            Line = Get_local_information(SuccessFile, file);
            SuccessFile.Close();
            Console.WriteLine("轉檔已完成!");
            Console.ReadLine();
        }

        private static string Get_local_information(StreamWriter SuccessFile, StreamReader file)
        {
            string Line;
            while ((Line = file.ReadLine()) != null)
            {
                try
                {
                    string[] ReadLine_Array = Line.Split(',');
                    string APIUrl = string.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0}&radius={1}&type={2}&key=AIzaSyAf8uwaEFaa7-x153Q2XoNuNcn68ARjTxg&language=zh-tw", ReadLine_Array[2] + "," + ReadLine_Array[1], ReadLine_Array[4], ReadLine_Array[5]);
                    var buffer = new WebClient().DownloadData(APIUrl);
                    string data = Encoding.UTF8.GetString(buffer);
                    var obj = JsonConvert.DeserializeObject<Rootobject>(data);
                    foreach (var x in obj.results)
                    {
                        Console.WriteLine(ReadLine_Array[0] + "查詢結果為...." + x.name + "," + x.geometry.location.lng + "," + x.geometry.location.lat);
                        SuccessFile.WriteLine(ReadLine_Array[0] + "," + ReadLine_Array[1] + "," + ReadLine_Array[2] + "," + ReadLine_Array[3] + "," + ReadLine_Array[4] + "," + ReadLine_Array[5] + "," + x.name + "," + x.geometry.location.lng + "," + x.geometry.location.lat
                            );
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
