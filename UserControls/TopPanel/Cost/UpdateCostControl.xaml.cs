using System;
using System.Collections.Generic;
using System.Globalization;
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
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace UserControls.TopPanel.Cost
{
    /// <summary>
    /// UpdateCostControl.xaml etkileşim mantığı
    /// </summary>
    public partial class UpdateCostControl : UserControl
    {
        public Entities.Concrete.Cost Cost { get; set; }

        private IUnitDal _unitDal = new EfUnitDal();
        private ICostDal _costDal = new EfCostDal();
        private int? _unitId;

        public UpdateCostControl()
        {
            InitializeComponent();

            EfUnitDal unitDal = new EfUnitDal();

            foreach (Unit unit in unitDal.GetAll())
            {
                MenuItem item = new MenuItem
                {
                    Header = unit.Name,
                    Visibility = Visibility.Visible
                };

                item.Click += (_, _) =>
                {
                    UnitMenu.Header = unit.Name;
                    _unitId = unit.Id;
                };

                UnitMenu.Items.Add(item);
            }
        }

        private void UnitPriceText_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]");
            e.Handled = regex.IsMatch(e.Text);
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

        private void ResetButton_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateCostControl control = new UpdateCostControl
            {
                Cost = Cost
            };

            Grid parent = (Parent as Grid);
            parent.Children.Clear();
            parent.Children.Add(control);
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameText.Text.Trim()))
            {
                ErrorText.Text = "Lütfen Bir İsim Giriniz";
            }
            else if (_unitId == null)
            {
                ErrorText.Text = "Lütfen Bir Birim Seçiniz";
            }
            else if (string.IsNullOrEmpty(UnitPriceText.Text))
            {
                ErrorText.Text = "Lütfen Birim Fiyat Giriniz";
            }
            else
            {
                Cost.Name = NameText.Text;
                Cost.UnitId = (int)_unitId;
                Cost.UnitPrice = double.Parse(UnitPriceText.Text.Replace(".", ""));

                _costDal.Update(Cost);

                Grid parent = (Parent as Grid);
                parent.Children.Clear();
                parent.Children.Add(new ListCostControl());
            }
        }

        private void UpdateCostControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            _unitId = Cost.UnitId;

            NameText.Text = Cost.Name;
            UnitMenu.Header = _unitDal.Get(u => u.Id == Cost.UnitId).Name;
            UnitPriceText.Text = Cost.UnitPrice.ToString(CultureInfo.InvariantCulture);

            Window window = Window.GetWindow(this);
            window.KeyDown += WindowOnKeyDown;
        }

        private void WindowOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UpdateButton_OnClick(sender, e);
            }
        }
    }
}
