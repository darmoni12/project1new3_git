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
                unitsLV.ItemsSource = myBL.getAllUnits();
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
                win.ShowDialog();
                unitsLV.ItemsSource = myBL.getAllUnits();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)//add unit
        {
            try
            {
                Host host = (Host)(hostsLV.SelectedItem);
                if (host == null)
                    throw new NullReferenceException("לא נבחר מארח");
                addUnitWin win = new addUnitWin(host);
                win.ShowDialog();
                unitsLV.ItemsSource = myBL.getAllUnits();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//add host
        {
            addHostWin win = new addHostWin();
            win.ShowDialog();
            hostsLV.ItemsSource = myBL.getAllHosts();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)// הצג סך עמלות והזמנות שאושרו
        {
            try
            {
                int x = myBL.getAllOrders().Count(order => order.Status == OrderStatus.ReservationAprroved);
                if (x==0)
                    MessageBox.Show("לא אושרו הזמנות לכן אין עמלות בינתיים");
                else
                    MessageBox.Show("there is " + x + " orders that approved. \n" + "total commision is: " + myBL.getSumOfCommission());
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)//כפתור הצג דרישות 
        {
            try
            {
                requestsWindow win = new requestsWindow();
                win.ShowDialog();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)//כפתור הצג הזמנות
        {
            try
            {
                ordersWin win = new ordersWin();
                win.ShowDialog();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)//edit host
        {
            try
            {
                Host host = (Host)(hostsLV.SelectedItem);
                if (host == null)
                    throw new NullReferenceException("לא נבחר מארח");
                editHostWin win = new editHostWin(host);
                win.ShowDialog();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}

