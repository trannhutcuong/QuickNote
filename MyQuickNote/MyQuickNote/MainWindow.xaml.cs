using DesktopWPFAppLowLevelKeyboardHook;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyQuickNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LowLevelKeyboardListener _listener;
        static public bool isRunningQuick = false;
        static public bool isRunningView = false;
        static public bool isRunningStatitistics = false;

        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Icon = new System.Drawing.Icon("notes.ico");
            notifyIcon.Visible = true;

            // Thêm Menu sau khi nhấn chuột phải
            System.Windows.Forms.ContextMenu notifyIconMenu = new System.Windows.Forms.ContextMenu();
            notifyIconMenu.MenuItems.Add("Quick notes (Windows+Alt)", new EventHandler(QuickNote));
            notifyIconMenu.MenuItems.Add("View notes", new EventHandler(ViewNote));
            notifyIconMenu.MenuItems.Add("View statitistics", new EventHandler(ViewStatitistic));
            notifyIconMenu.MenuItems.Add("Exit", new EventHandler(Exit));
            notifyIcon.ContextMenu = notifyIconMenu;

            this.Show();
            System.Threading.Thread.Sleep(3000);
            this.Hide();
            // Thông báo khởi động chương trình
            notifyIcon.ShowBalloonTip(5, "Thông báo", "Đã khởi động Quick notes", System.Windows.Forms.ToolTipIcon.Info);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _listener = new LowLevelKeyboardListener();
            _listener.OnKeyPressed += _listener_OnKeyPressed;

            _listener.HookKeyboard();
        }

        private void _listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            if (e.KeyPressed == Key.LWin)
                _listener.OnKeyPressed += _listener_OnKeyPressed1;
        }

        private void _listener_OnKeyPressed1(object sender, KeyPressedArgs e)
        {
            if (e.KeyPressed == Key.LeftAlt)
            {
                if (isRunningQuick == false)
                {
                    QuickNotes qNote = new QuickNotes();
                    qNote.Show();
                    isRunningQuick = true;
                    _listener.OnKeyPressed -= _listener_OnKeyPressed;
                }
                else MessageBox.Show("CỬA SỔ ĐANG CHẠY", "Thông báo");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _listener.UnHookKeyboard();
        }

        private void QuickNote(object sender, EventArgs e)
        {
            if (isRunningQuick == false)
            {
                QuickNotes qNote = new QuickNotes();
                qNote.Show();
                isRunningQuick = true;
            }
            else MessageBox.Show("CỬA SỔ ĐANG CHẠY", "Thông báo");
        }

        private void ViewNote(object sender, EventArgs e)
        {
            if (isRunningView == false)
            {
                ViewNotes vNote = new ViewNotes();
                vNote.Show();
                isRunningView = true;
            }
            else MessageBox.Show("CỬA SỔ ĐANG CHẠY", "Thông báo");
        }

        private void ViewStatitistic(object sender, EventArgs e)
        {
            if (isRunningStatitistics == false)
            {
                Statitistics stati = new Statitistics();
                stati.Show();
                isRunningStatitistics = true;
            }
            else MessageBox.Show("CỬA SỔ ĐANG CHẠY", "Thông báo");
        }

        private void Exit(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("THOÁT ỨNG DỤNG ?", "Thoát", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                    App.Current.Windows[intCounter].Close();
            }
        }
    }
}
