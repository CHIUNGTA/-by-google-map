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


namespace 計算路線與行駛時間
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("請輸入轉換文字檔(.csv)之連結地址");
            //取得用戶轉換資料地址
            var FileAddress = Console.ReadLine();
            Console.WriteLine("請輸入交通類型(Driving,Waliking)");
            var type = Console.ReadLine();
            StreamWriter SuccessFile = new StreamWriter("D:\\路線距離與時間計算.txt");
            var file = new System.IO.StreamReader(FileAddress);
            string Line = string.Empty;
            Line = file.ReadLine();
            SuccessFile.WriteLine(Line+","+"距離"+"," +"時間" + "," +"類型");
            while ((Line = file.ReadLine()) != null)
            {
                string[] ReadLine_Array = Line.Split(',');
                string APIUrl = string.Format("https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&mode={2}&key=AIzaSyAf8uwaEFaa7-x153Q2XoNuNcn68ARjTxg&language=zh-tw",ReadLine_Array[2]+","+ReadLine_Array[1] , ReadLine_Array[4] + "," + ReadLine_Array[3] ,type);
                var buffer = new WebClient().DownloadData(APIUrl);
                string data = Encoding.UTF8.GetString(buffer);
                var obj = JsonConvert.DeserializeObject<Rootobject>(data);
                Console.WriteLine("ID:"+ReadLine_Array[0] +"行駛距離為"+obj.routes[0].legs[0].distance.value + "m," + "行駛時間為" +obj.routes[0].legs[0].duration.text);
                SuccessFile.WriteLine(ReadLine_Array[0] + "," + ReadLine_Array[1] + "," + ReadLine_Array[2] + "," + ReadLine_Array[3] + "," + ReadLine_Array[4] + ","+ obj.routes[0].legs[0].distance.value+","+ obj.routes[0].legs[0].duration.value+","+type);
            }
            SuccessFile.Close();
            Console.ReadLine();
        }
    }
}
