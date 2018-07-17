using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyQuickNote
{
    /// <summary>
    /// Interaction logic for QuickNotes.xaml
    /// </summary>
    public partial class QuickNotes : Window
    {
        List<Data> listData = Data.DocFileData("QuickNote.txt");

        public QuickNotes()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbNote.Text == "" || tbTag.Text == "")
                MessageBox.Show("Hãy nhập đủ note và tag", "Thông báo");
            else
            {
                ThemTagNot();
                MessageBox.Show("THÊM THÀNH CÔNG", "Thông báo");
                tbNote.Text = "";
            }
        }

        private void ThemTagNot()
        {
            string Text = tbTag.Text;                                  // Lấy chuỗi trong textbox
            string[] str = Text.Split(',');                            // Lấy các tag cách nhau bởi dấu ','
            int soChuoi = str.Length;
            for (int i = 0; i < soChuoi; ++i)
                str[i] = str[i].Trim();                                // Bỏ các khoảng trắng thừa
            for (int i = 0; i < soChuoi; ++i)                          // Bắt đầu kiểm tra và thêm vào
            {
                int soTag = listData.Count;
                // Kiểm tra note đã tồn tại
                if (Data.KiemTraTag(str[i].ToLower(), "QuickNote.txt"))
                {
                    for (int j = 0; j < soTag; ++j)
                    {
                        if (listData[j].Tag.ToLower() == str[i].ToLower())
                        {
                            listData[j].SoLanGoiTag += 1;
                            listData[j].Note.Add(tbNote.Text);
                        }
                    }
                    Data.GhiFileData(listData, "QuickNote.txt");
                }
                else
                {
                    Data data = new Data();
                    data.Tag = str[i].First().ToString().ToUpper() + str[i].Substring(1);
                    data.SoLanGoiTag = 1;
                    data.Note.Add(tbNote.Text);
                    listData.Add(data);
                    Data.GhiFileData(listData, "QuickNote.txt");
                }
            }
        }

        private void tbTag_TextChanged(object sender, TextChangedEventArgs e)
       {
            string[] textTag = tbTag.Text.Split(',');
            int soTag = listData.Count;
            List<SearchString> listStr = new List<SearchString>();
            int dem = 0;

            for(int i = 0; i < soTag; ++i)
            {
                if(listData[i].Tag.ToLower().Contains(textTag[textTag.Length-1].Trim().ToLower()))
                {
                    listStr.Add(new SearchString() { string1 = listData[i].Tag, num1 = listData[i].SoLanGoiTag });
                    dem++;
                }
            }
            if(dem > 0)
            {
                lvTag.ItemsSource = listStr;
                lvTag.Visibility = Visibility.Visible;
            }
            else
            {
                lvTag.ItemsSource = null;
                lvTag.Visibility = Visibility.Hidden;
            }
        }

        private void Chon_Tag(object sender, MouseButtonEventArgs e)
        {
            SearchString sString = (sender as TextBlock).DataContext as SearchString;
            string[] textTag = tbTag.Text.Split(',');
            textTag[textTag.Length - 1] = sString.string1;
            string Temp = "";
            for(int i = 0; i < textTag.Length; ++i)
            {
                Temp += textTag[i];
                if (i != textTag.Length - 1 && textTag.Length > 1)
                    Temp += ",";
            }
            tbTag.Text = Temp;
            tbTag.CaretIndex = tbTag.Text.Length;
            lvTag.Visibility = Visibility.Hidden;
        }

        private void Window_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && lvTag.Visibility != Visibility.Hidden)
            {
                List<SearchString> listString = new List<SearchString>();
                listString = (List<SearchString>)lvTag.ItemsSource;
                string[] textTag = tbTag.Text.Split(',');
                textTag[textTag.Length - 1] = listString[0].string1;
                string Temp = "";
                for (int i = 0; i < textTag.Length; ++i)
                {
                    Temp += textTag[i];
                    if (i != textTag.Length - 1 && textTag.Length > 1)
                        Temp += ",";
                }
                tbTag.Text = Temp;
                tbTag.CaretIndex = tbTag.Text.Length;
                lvTag.Visibility = Visibility.Hidden;
            }
            else if (e.Key == Key.Enter && lvTag.Visibility == Visibility.Hidden)
            {
                if (tbNote.Text == "" || tbTag.Text == "")
                    MessageBox.Show("Hãy nhập đủ note và tag", "Thông báo");
                else
                {
                    ThemTagNot();
                    MessageBox.Show("THÊM THÀNH CÔNG", "Thông báo");
                    tbNote.Text = "";
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.isRunningQuick = false;
        }
    }

    public class SearchString
    {
        public string string1 { get; set; }
        public int num1 { get; set; }
    }
}
