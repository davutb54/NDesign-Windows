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

        public ListCostControl()
        {
            InitializeComponent();
        }

        private void GetData()
        {

        }
    }
}
