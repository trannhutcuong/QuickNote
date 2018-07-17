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
    /// Interaction logic for ViewNotes.xaml
    /// </summary>
    public partial class ViewNotes : Window
    {
        private List<Data> listData = Data.DocFileData("QuickNote.txt");
        

        public ViewNotes()
        {
            InitializeComponent();
            int soTag = listData.Count;
            List<Tag> list = new List<Tag>();

            for(int i = 0; i < soTag; ++i)
                list.Add(new Tag() { tag = listData[i].Tag, soluongtag = listData[i].SoLanGoiTag });
            lvTags.ItemsSource = list;            
        }

        private void Chon_Note_Tag(object sender, MouseButtonEventArgs e)
        {
            Tag Tg = (sender as TextBlock).DataContext as Tag;
            int soTag = listData.Count;
            List<Note> listNote = new List<Note>();

            for (int i = 0; i < soTag; ++i)
                if (Tg.tag == listData[i].Tag)
                {
                    for (int j = 0; j < listData[i].SoLanGoiTag; ++j)
                        listNote.Add(new Note() { note = listData[i].Note[j] });
                }
            lvNotes.ItemsSource = listNote;
            tbFull.Text = "";
        }

        private void Xem_Full_Note(object sender, MouseButtonEventArgs e)
        {
            Note nt = (sender as TextBlock).DataContext as Note;
            tbFull.Text = nt.note;
        }

        private void Button_XoaTag_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("XÓA TAG NÀY VÀ CÁC NOTE LIÊN QUAN ?", "Xóa", MessageBoxButton.OKCancel);
            if(result == MessageBoxResult.OK)
            {
                Tag tg = (sender as Button).DataContext as Tag;
                int soTag = listData.Count;

                for (int i = 0; i < soTag; ++i)
                    if (listData[i].Tag == tg.tag)
                    {
                        listData.Remove(listData[i]);
                        break;
                    }
                // Cập nhật lại list tag
                Data.GhiFileData(listData, "QuickNote.txt");
                List<Tag> list = new List<Tag>();
                soTag = listData.Count;
                for (int i = 0; i < soTag; ++i)
                    list.Add(new Tag() { tag = listData[i].Tag, soluongtag = listData[i].SoLanGoiTag });
                lvTags.ItemsSource = list;
            }
        }

        private void Button_XoaNote_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("XÓA NOTE NÀY ?", "Xóa", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                Note nt = (sender as Button).DataContext as Note;
                int soTag = listData.Count;
                for(int i = 0; i < soTag; ++i)
                    for(int j = 0; j < listData[i].SoLanGoiTag; ++j)
                        if(nt.note == listData[i].Note[j])
                        {
                            listData[i].Note.Remove(listData[i].Note[j]);
                            listData[i].SoLanGoiTag -= 1;
                            break;
                        }
                // Cập nhật lại list tag
                Data.GhiFileData(listData, "QuickNote.txt");
                List<Tag> list = new List<Tag>();
                soTag = listData.Count;
                for (int i = 0; i < soTag; ++i)
                    list.Add(new Tag() { tag = listData[i].Tag, soluongtag = listData[i].SoLanGoiTag });
                lvTags.ItemsSource = list;
            }
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            lvNotes.ItemsSource = null;
            tbFull.Text = "";
            listData = Data.DocFileData("QuickNote.txt");
            int soTag = listData.Count;
            List<Tag> list = new List<Tag>();
            for (int i = 0; i < soTag; ++i)
                list.Add(new Tag() { tag = listData[i].Tag, soluongtag = listData[i].SoLanGoiTag });
            lvTags.ItemsSource = list;
        }

        private void Button_Thoat(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow.isRunningView = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.isRunningView = false;
        }
    }

    public class Tag
    {
        public string tag { get; set; }
        public int soluongtag { get; set; }
    }
    public class Note
    {
        public string note { get; set; }
    }    
}
