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
    /// Interaction logic for requestsWindow.xaml
    /// </summary>
    public partial class requestsWindow : Window
    {
        public IBL myBL = BLI.GetBL();
        public requestsWindow(HostingUnit unit)
        {
            InitializeComponent();
            requestsLV.ItemsSource = myBL.getRequestIf(req=>unit.fitCheck(req));
        }
    }
}
