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
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace UserControls.TopPanel.Cost
{
    /// <summary>
    /// UpdateCostControl.xaml etkileşim mantığı
    /// </summary>
    public partial class UpdateCostControl : UserControl
    {
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
            
        }

        private void UnitPriceText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void ResetButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
