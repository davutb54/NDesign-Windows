using System;
using System.Collections.Generic;
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

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BitmapImage Image { get; set; } = new BitmapImage(new Uri("C:\\Users\\HİTMACHİNE\\Desktop\\logo.png"));
        public String Username { get; set; } = "Deneme Sürümü";

        public bool İsMaximized { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void TopPanel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void MaximizeButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (İsMaximized)
            {
                WindowState = WindowState.Normal;
                MaximizeButton.Source = new BitmapImage(new Uri(@"/Resources/Images/maximize.png",UriKind.Relative));
                İsMaximized = false;
            }
            else
            {
                WindowState = WindowState.Maximized;
                MaximizeButton.Source = new BitmapImage(new Uri(@"/Resources/Images/size.png",UriKind.Relative));
                İsMaximized = true;
            }
        }

        private void MinimizeButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}