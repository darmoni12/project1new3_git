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
    /// Interaction logic for unitsWin.xaml
    /// </summary>
    public partial class unitsWin : Window
    {
        public IBL myBL = BLI.GetBL();
        public unitsWin()
        {
            InitializeComponent();
            unitsLV.ItemsSource = myBL.getAllUnits();
            hostsLV.ItemsSource = myBL.getAllHosts();

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)//remove unit
        {
            try
            {
                HostingUnit unit = (HostingUnit)(unitsLV.SelectedItem);
                myBL.removeHostingUnit(unit.HostingUnitKey);
                this.Close();
                unitsWin win = new unitsWin();
                win.ShowDialog();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//edit unit
        {
            try
            {
                HostingUnit unit = (HostingUnit)(unitsLV.SelectedItem);
                if (unit == null)
                    throw new NullReferenceException("לא נבחרה יחידת אירוח");
                editUnitWin win = new editUnitWin(unit);
                this.Close();
                win.ShowDialog();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)//add unit
        {
            Host host = new Host();
            addUnitWin win = new addUnitWin(host);
            this.Close();
            win.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//add host
        {
            addHostWin win = new addHostWin();
            this.Close();
            win.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//edit host win
        {
            MessageBox.Show("total commision is: "+myBL.getSumOfCommission() );
        }
    }
}

