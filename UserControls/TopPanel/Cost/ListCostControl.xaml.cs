using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace UserControls.TopPanel.Cost
{
    /// <summary>
    /// ListCostControl.xaml etkileşim mantığı
    /// </summary>
    public partial class ListCostControl : UserControl
    {
        ICostDal _costDal = new EfCostDal();
        private IUnitDal _unitDal = new EfUnitDal();

        private int _listNo;
        //private bool _isNumberSelected = true;
        //private bool _isNameSelected = false;
        //private bool _isUnitSelected = false;
        //private bool _isUnitPriceSelected = false;

        public ListCostControl()
        {
            InitializeComponent();
        }

        private void ListCostControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            _listNo = 0;
            NumberList.Style = FindResource("ClickedListButton") as Style;
            ShowData();
        }

        private void NumberList_OnClick(object sender, RoutedEventArgs e)
        {
            _listNo = 0;
            NumberList.Style = FindResource("ClickedListButton") as Style;
            NameList.Style = FindResource("ListButton") as Style;
            UnitList.Style = FindResource("ListButton") as Style;
            UnitPriceList.Style = FindResource("ListButton") as Style;
            ShowData();
        }

        private void NameList_OnClick(object sender, RoutedEventArgs e)
        {
            _listNo = 1;
            NameList.Style = FindResource("ClickedListButton") as Style;
            NumberList.Style = FindResource("ListButton") as Style;
            UnitList.Style = FindResource("ListButton") as Style;
            UnitPriceList.Style = FindResource("ListButton") as Style;
            ShowData();
        }

        private void UnitList_OnClick(object sender, RoutedEventArgs e)
        {
            _listNo = 2;
            UnitList.Style = FindResource("ClickedListButton") as Style;
            NumberList.Style = FindResource("ListButton") as Style;
            NameList.Style = FindResource("ListButton") as Style;
            UnitPriceList.Style = FindResource("ListButton") as Style;
            ShowData();
        }

        private void UnitPriceList_OnClick(object sender, RoutedEventArgs e)
        {
            _listNo = 3;
            UnitPriceList.Style = FindResource("ClickedListButton") as Style;
            NumberList.Style = FindResource("ListButton") as Style;
            NameList.Style = FindResource("ListButton") as Style;
            UnitList.Style = FindResource("ListButton") as Style;
            ShowData();
        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            ShowData();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            Grid parent = (Parent as Grid);
            parent.Children.Clear();
            parent.Children.Add(new AddCostControl());
        }

        private List<Entities.Concrete.Cost> GetData()
        {
            List<Entities.Concrete.Cost> data = _costDal.GetAll();

            var query = _listNo switch
            {
                1 => from cost in data
                     orderby cost.Name
                     select new Entities.Concrete.Cost
                     {
                         Id = cost.Id,
                         Name = cost.Name,
                         UnitId = cost.UnitId,
                         UnitPrice = cost.UnitPrice
                     },
                2 => from cost in data
                     orderby cost.UnitId
                     select new Entities.Concrete.Cost
                     {
                         Id = cost.Id,
                         Name = cost.Name,
                         UnitId = cost.UnitId,
                         UnitPrice = cost.UnitPrice
                     },
                3 => from cost in data
                     orderby cost.UnitPrice
                     select new Entities.Concrete.Cost
                     {
                         Id = cost.Id,
                         Name = cost.Name,
                         UnitId = cost.UnitId,
                         UnitPrice = cost.UnitPrice
                     },
                _ => from cost in data
                     select new Entities.Concrete.Cost
                     {
                         Id = cost.Id,
                         Name = cost.Name,
                         UnitId = cost.UnitId,
                         UnitPrice = cost.UnitPrice
                     }
            };

            return query.ToList();
        }

        private void ShowData()
        {
            ListView.Items.Clear();

            int index = 1;

            List<Entities.Concrete.Cost> data = GetData();

            foreach (Entities.Concrete.Cost cost in data)
            {
                string unit = _unitDal.Get(u => u.Id == cost.UnitId).Name;

                Grid grid = new Grid
                {
                    Width = 588,
                    Height = 38
                };
                grid.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(30)});
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(350) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60) });

                TextBlock numberText = new TextBlock
                {
                    Text = index.ToString(),
                    FontSize = 11,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetColumn(numberText,0);
                grid.Children.Add(numberText);

                TextBlock nameText = new TextBlock
                {
                    Text = cost.Name,
                    FontSize = 13,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(10,0,0,0)
                };
                Grid.SetColumn(nameText, 1);
                grid.Children.Add(nameText);

                TextBlock unitText = new TextBlock
                {
                    Text = unit,
                    FontSize = 14,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetColumn(unitText, 2);
                grid.Children.Add(unitText);

                TextBlock unitPriceText = new TextBlock
                {
                    Text = cost.UnitPrice.ToString(CultureInfo.InvariantCulture),
                    FontSize = 13,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetColumn(unitPriceText, 3);
                grid.Children.Add(unitPriceText);

                StackPanel stackPanel = new StackPanel
                {
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                Button deleteButton = new Button
                {
                    Style = FindResource("DeleteButton") as Style,
                    Margin = new Thickness(0, 1, 0, 1)
                };
                deleteButton.Click += (_, _) =>
                {
                    _costDal.Delete(cost);
                    ShowData();
                };
                stackPanel.Children.Add(deleteButton);

                Button updateButton = new Button
                {
                    Style = FindResource("UpdateButton") as Style,
                    Margin = new Thickness(0, 1, 0, 1)
                };
                updateButton.Click += (_, _) =>
                {
                    UpdateCostControl control = new UpdateCostControl
                    {
                        Cost = cost
                    };

                    Grid parent = (Parent as Grid);
                    parent.Children.Clear();
                    parent.Children.Add(control);
                };
                stackPanel.Children.Add(updateButton);

                Grid.SetColumn(stackPanel,4);
                grid.Children.Add(stackPanel);

                ListView.Items.Add(grid);

                index++;
            }
        }
    }
}
