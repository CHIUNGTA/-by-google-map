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

namespace 距離計算
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("請輸入轉換文字檔(.csv)之連結地址");
            var FileAddress = Console.ReadLine();
            StreamWriter SuccessFile = new StreamWriter("D:\\經緯度轉距離完成檔.txt");
            Again:
            try
            {
                //using (StreamReader SR = new StreamReader(add))
                    var file = new System.IO.StreamReader(FileAddress);
                    string Line=string.Empty;
                    Line = file.ReadLine();   //避免欄位名稱寫入
                    string[] FieldLine = Line.Split(',');
                    //將欄位名稱寫入txt檔中
                    SuccessFile.WriteLine(FieldLine[0]+","+ FieldLine[1] + "," + FieldLine[2] + "," + FieldLine[3] + "," + FieldLine[4] +","+ "Distance(m)");
                    while ((Line =file.ReadLine()) != null)
                    {
                        string[] ReadLine_Array = Line.Split(',');
                        string APIUrl = string.Format($"https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins={ReadLine_Array[1]+","+ ReadLine_Array[2]}&destinations={ReadLine_Array[3] + "," + ReadLine_Array[4]}&key=AIzaSyDTqOSVYjQX5Pkrt3RBJmSWgGLVmNQiy6k&language=zh-tw");
                        var buffer = new WebClient().DownloadData(APIUrl);
                        string data = Encoding.UTF8.GetString(buffer);
                        var obj = JsonConvert.DeserializeObject<Rootobject>(data);
                        Console.WriteLine("ID:" + ReadLine_Array[0] +"的距離為:"+ obj.rows[0].elements[0].distance.value.ToString() + "公尺");
                        SuccessFile.WriteLine(ReadLine_Array[0]+","+ReadLine_Array[1] + "," +ReadLine_Array[2] + "," + ReadLine_Array[3] + "," + ReadLine_Array[4] + "," + obj.rows[0].elements[0].distance.value);
                    }
                    SuccessFile.Close();
                    Console.WriteLine("轉檔完成，請至D:查看");
                    Console.ReadLine();
            
            }
            catch (Exception)
            {
                Console.WriteLine("請輸入正確連結或確認檔案是否正確");
                goto Again;
            }
        }
    }

}

