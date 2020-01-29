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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ordersWin.xaml
    /// </summary>
    public partial class ordersWin : Window
    {
        public IBL myBL = BLI.GetBL();
        public ordersWin()
        {
            InitializeComponent();
            lv.ItemsSource = myBL.getAllOrders();
        }
    }
}
