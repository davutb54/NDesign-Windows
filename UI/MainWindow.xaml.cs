using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControls.TopPanel.Cost;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BitmapImage Image { get; set; } = new BitmapImage(new Uri(@"C:\Users\HİTMACHİNE\Desktop\logo.png"));
        public String Username { get; set; } = Environment.UserName;

        public bool İsMaximized { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            if (!System.IO.Directory.Exists($@"C:\Users\{Environment.UserName}\AppData\Roaming\JobTracking"))
            {
                System.IO.Directory.CreateDirectory($@"C:\Users\{Environment.UserName}\AppData\Roaming\JobTracking");
            }

            if (!System.IO.File.Exists($@"C:\Users\{Environment.UserName}\AppData\Roaming\JobTracking\app.db"))
            {
                SQLiteConnection.CreateFile($@"C:\Users\{Environment.UserName}\AppData\Roaming\JobTracking\app.db");

                List<string> commands = new List<string>();
                commands.Add("create table if not Exists Costs (Id INTEGER PRIMARY KEY, Name VARCHAR,UnitId INT,UnitPrice DOUBLE)");
                commands.Add("create table if not exists Units (Id INTEGER PRIMARY KEY ,Name VARCHAR)");

                SQLiteConnection connection = new SQLiteConnection($@"Data Source=C:\Users\{Environment.UserName}\AppData\Roaming\JobTracking\app.db");
                connection.Open();

                foreach (string command in commands)
                {
                    SQLiteCommand cmd = new SQLiteCommand(command, connection);
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private void TopPanel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        #region Top Panel

        #region App



        #endregion

        #region Edit



        #endregion

        #region Data

        #region Cost

        private void AddCost_OnClick(object sender, RoutedEventArgs e)
        {
            UserControlGrid.Children.Clear();
            UserControlGrid.Children.Add(new AddCostControl());
        }

        #endregion

        #endregion

        #region Settings



        #endregion

        #region Window Buttons

        private void CloseButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void MaximizeButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (İsMaximized)
            {
                WindowState = WindowState.Normal;
                MaximizeButton.Source = new BitmapImage(new Uri(@"/Resources/Images/maximize.png", UriKind.Relative));
                İsMaximized = false;
            }
            else
            {
                WindowState = WindowState.Maximized;
                MaximizeButton.Source = new BitmapImage(new Uri(@"/Resources/Images/size.png", UriKind.Relative));
                İsMaximized = true;
            }
        }

        private void MinimizeButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        #endregion

        #endregion


    }
}