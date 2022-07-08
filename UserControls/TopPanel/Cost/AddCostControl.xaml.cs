using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using DataAccess.Concrete.EntityFramework;

namespace UserControls.TopPanel.Cost
{
    /// <summary>
    /// AddCostControl.xaml etkileşim mantığı
    /// </summary>
    public partial class AddCostControl : UserControl
    {
        public AddCostControl()
        {
            InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Content = "Enter";

            EfCostDal costDal = new EfCostDal();
            costDal.Add(new Entities.Concrete.Cost
            {
                Name = NameText.Text,
                UnitId = 1,
                UnitPrice = int.Parse(UnitPriceText.Text.Replace(".",""))
            });
        }

        private void AddCostControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += WindowOnKeyDown;
        }

        private void WindowOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddButton_OnClick(sender, e);
            }
        }

        private void UnitPriceText_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (UnitPriceText.Text.Contains(","))
            {
                Regex regex = new Regex("[^0-9]");
                e.Handled = regex.IsMatch(e.Text);
            }
            else
            {
                Regex regex = new Regex("[^0-9,]");
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        private void UnitPriceText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string text = UnitPriceText.Text;
            string tempText = UnitPriceText.Text;

            text = text.Replace(".", "");
            tempText = tempText.Replace(".", "");

            while (text.Length > 3)
            {
                if (text.Contains(','))
                    text = text[..text.LastIndexOf(',')];
                if (text.Contains('.'))
                    text = text[..text.LastIndexOf('.')];

                if (text.Length > 3)
                {
                    text = text.Insert(text.Length - 3, ".");
                    tempText = tempText.Insert(text.Length - 4, ".");
                }
                else
                {
                    UnitPriceText.SelectionStart = UnitPriceText.Text.Length;
                    UnitPriceText.Text = tempText;
                    return;
                }
            }
        }
    }
}
