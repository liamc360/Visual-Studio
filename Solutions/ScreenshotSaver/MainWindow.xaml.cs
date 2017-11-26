using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;


namespace ScreenshotSaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 9000;

        //Modifiers:
        private const uint MOD_NONE = 0x0000; //(none)
        private const uint MOD_ALT = 0x0001; //ALT
        private const uint MOD_CONTROL = 0x0002; //CTRL
        private const uint MOD_SHIFT = 0x0004; //SHIFT
        private const uint MOD_WIN = 0x0008; //WINDOWS
        //CAPS LOCK:
        private const uint VK_CAPITAL = 0x2C;


        private Dictionary<string, string> userSettings = new Dictionary<string, string>();
        private string configFileLocation = "";
        private string configFolderLocation = "";

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);


        public MainWindow()
        {
            LoadConfig();
            InitializeComponent();
            SetupGUI();

            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("Screenshot.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
            ShowInTaskbar = false;

        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        private void LoadConfig()
        {
            // The folder for the roaming current user 
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            configFolderLocation = System.IO.Path.Combine(folder, "ScreenshotSaver");

            // CreateDirectory will check if folder exists and, if not, create it.
            // If folder exists then CreateDirectory will do nothing.
            Directory.CreateDirectory(configFolderLocation);
            configFileLocation = configFolderLocation + "/UserSettings.conf";

            using (StreamWriter w = File.AppendText(configFileLocation))
            {
                if (new FileInfo(configFileLocation).Length == 0)
                {
                    w.WriteLine("ScreenshotFolder=" + Directory.GetCurrentDirectory());
                    w.WriteLine("CreateUniqueFolders=false");
                }
                
            }


            foreach (var row in File.ReadAllLines(configFileLocation))
                userSettings.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));


            //string x = data["ServerName"];

            //Console.WriteLine(data["ServerName"]);
        }

        private void SetupGUI()
        {
            txtFolderPath.Text = userSettings["ScreenshotFolder"];
            chkSeperateFolders.IsChecked = Convert.ToBoolean(userSettings["CreateUniqueFolders"]);
        }

        private IntPtr _windowHandle;
        private HwndSource _source;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            _windowHandle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);

            RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_NONE, VK_CAPITAL); //CTRL + CAPS_LOCK
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int PRINTSCREEN_HOTKEY = 0x0312;
            switch (msg)
            {
                case PRINTSCREEN_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            int vkey = (((int)lParam >> 16) & 0xFFFF);
                            if (vkey == VK_CAPITAL)
                            {
                                HandlePrintscreen();
                            }
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private async void HandlePrintscreen()
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                    Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap as System.Drawing.Image);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            var date = DateTime.Now.ToString("MM-dd-yy-Hmm-ss");
            string activeWindowName = GetSafeFilename(GetActiveWindowTitle().Trim());
            string newFolderLocation = userSettings["ScreenshotFolder"];
            string fileName = date + ".png";

            if (userSettings["CreateUniqueFolders"].Equals("True"))
            {
                if (activeWindowName != "")
                {
                    newFolderLocation = System.IO.Path.Combine(newFolderLocation, activeWindowName);
                    Directory.CreateDirectory(newFolderLocation);
                    newFolderLocation = newFolderLocation + "\\";
                }
            }
            else
            {
                fileName = activeWindowName + "-" + date + ".png";
            }


            try
            {
                bitmap.Save(newFolderLocation + fileName, ImageFormat.Png);
            }
            catch (Exception e)
            { 
                bitmap.Save(userSettings["ScreenshotFolder"] + date + ".png", ImageFormat.Png);
            }
            bitmap.Dispose();
            graphics.Dispose();
        }

        private string GetSafeFilename(string filename)
        {
            return string.Join("_", filename.Split(System.IO.Path.GetInvalidFileNameChars()));
        }

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }


        protected override void OnClosed(EventArgs e)
        {
            _source.RemoveHook(HwndHook);
            UnregisterHotKey(_windowHandle, HOTKEY_ID);
            base.OnClosed(e);
        }


        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            txtFolderPath.Text = dialog.SelectedPath + @"\";
            userSettings["ScreenshotFolder"] = dialog.SelectedPath + @"\";
            UpdateConfig();
        }

        private void chkSeperateFolders_Checked(object sender, RoutedEventArgs e)
        {
            userSettings["CreateUniqueFolders"] = chkSeperateFolders.IsChecked.ToString();
            UpdateConfig();
        }

        private void UpdateConfig()
        {
            string[] lines = System.IO.File.ReadAllLines(configFileLocation);

            int i = 0;
            foreach (var setting in userSettings)
            {
                lines[i] = setting.Key + "=" + setting.Value;
                i++;
            }
            File.WriteAllLines(configFileLocation, lines);
        }
    }

}
