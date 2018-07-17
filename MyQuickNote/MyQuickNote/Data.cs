using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyQuickNote
{   
    // Lớp data dùng để lưu và load dữ liệu
    public class Data
    {
        public Data()
        {
            Tag = "";
            Note = new List<string>();
            SoLanGoiTag = 0;
        }

        public string Tag { get; set; }
        public List<string> Note { get; set; }
        public int SoLanGoiTag { get; set; }                   // Dùng để lưu số lần tag được sử dụng để lưu notes

        public static bool KiemTraTag(string tenTag, string tenfile)             // Kiểm tra tên tags có tồn tại hay chưa
        {
            List<Data> listData = DocFileData(tenfile);
            int soTag = listData.Count;
            for (int i = 0; i < soTag; ++i)
            {
                if (listData[i].Tag.ToLower() == tenTag)
                    return true;
            }
            return false;
        }

        public static void GhiFileData(List<Data> listData, string tenfile)      // Ghi tag và node vào file
        {
            using (StreamWriter sw = new StreamWriter(tenfile))
            {
                int soTag = listData.Count;
                for(int i = 0; i < soTag; ++i)
                {
                    sw.WriteLine(listData[i].Tag);
                    sw.WriteLine(listData[i].SoLanGoiTag);
                    for(int j = 0; j < listData[i].SoLanGoiTag; ++j)
                    {
                        sw.WriteLine(listData[i].Note[j]);
                    }
                }
            }
        }

        public static List<Data> DocFileData(string tenfile)                     // Đọc note và tag từ file
        {
            List<Data> listData = new List<Data>();
            using (StreamReader sr = new StreamReader(tenfile))
            {
                string tag;
                int solangoitag;
                
                while((tag = sr.ReadLine()) != null)
                {
                    Data dt = new Data();
                    dt.Tag = tag;
                    solangoitag = int.Parse(sr.ReadLine());
                    dt.SoLanGoiTag = solangoitag;
                    for(int i = 0; i < solangoitag; ++i)
                    {
                        dt.Note.Add(sr.ReadLine());
                    }
                    listData.Add(dt);
                }
            }
            return listData;
        }
    }
}
