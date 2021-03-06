﻿using System;
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
    /// Interaction logic for hostWin.xaml
    /// </summary>
    public partial class hostWin : Window
    {
        IBL myBL = BLI.GetBL();
        Host myHost { get; set; }
        public hostWin(Host host)
        {
            InitializeComponent();
            myHost = host;            
            unitCB.ItemsSource = myBL.getUnitsForHost(myHost);
            this.Title ="host:   "+ host.PrivateName;
        }

        private void unitCB_SelectionChanged(object sender, SelectionChangedEventArgs e)//select combo box
        {
            try
            {
                HostingUnit unit = (HostingUnit)(unitCB.SelectedItem);
                IEnumerable<Order> orderList = myBL.getOrdersByUnitKey(unit.HostingUnitKey);
                orderLV.ItemsSource = orderList;
                reqLV.ItemsSource = myBL.getRequestIf(request => unit.fitCheck(request) && !myBL.isInOrderList(request, orderList));
            }
            catch (Exception) { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//edit host
        {
            try
            {
                editHostWin win = new editHostWin(myHost);
                unitCB.SelectedIndex = -1;
                win.ShowDialog();
                unitCB.ItemsSource = myBL.getUnitsForHost(myHost);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//add unit
        {
            try
            {
                addUnitWin win = new addUnitWin(myHost);
                win.ShowDialog();
                unitCB.ItemsSource = myBL.getUnitsForHost(myHost);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//edit unit
        {
            try
            {
                HostingUnit unit = (HostingUnit)(unitCB.SelectedItem);
                if (unit == null)
                    throw new NullReferenceException("לא נבחרה יחידת אירוח");
                editUnitWin win = new editUnitWin(unit);
                win.ShowDialog();
                unitCB.ItemsSource = myBL.getUnitsForHost(myHost);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//remove unit
        {
            try
            {
                HostingUnit unit = (HostingUnit)(unitCB.SelectedItem);
                if (unit == null)
                    throw new NullReferenceException("לא נבחרה יחידת אירוח");
                myBL.removeHostingUnit(unit.HostingUnitKey);
                hostWin win = new hostWin(myHost);
                this.Close();
                win.ShowDialog();
                unitCB.ItemsSource = myBL.getUnitsForHost(myHost);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//send mail
        {
            try
            {
                HostingUnit unit = (HostingUnit)(unitCB.SelectedItem);
                GuestRequest req = (GuestRequest)(reqLV.SelectedItem);
                if (req == null)
                    throw new NullReferenceException("לא נבחרה דרישת אירוח");
                myBL.makeOrder(req.GuestRequestKey, unit.HostingUnitKey);//שולח את המייל כאן ויוצר הזמנה
                IEnumerable<Order> orderList = myBL.getOrdersByUnitKey(unit.HostingUnitKey);
                orderLV.ItemsSource = orderList;
                reqLV.ItemsSource = myBL.getRequestIf(request => unit.fitCheck(request) && !myBL.isInOrderList(request,orderList));
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)//אישור הזמנה
        {
            try
            {
                HostingUnit unit = (HostingUnit)(unitCB.SelectedItem);
                Order order = (Order)(orderLV.SelectedItem);
                if (order == null)
                    throw new NullReferenceException("לא נבחרה הזמנה");
                int commission = myBL.acceptOrder(order);
                MessageBox.Show("חויבת עמלת תיווך על סך "+commission+" שקלים");
                IEnumerable<Order> orderList = myBL.getOrdersByUnitKey(unit.HostingUnitKey);
                orderLV.ItemsSource = orderList;
                reqLV.ItemsSource = myBL.getRequestIf(request => unit.fitCheck(request) && !myBL.isInOrderList(request, orderList));
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)//דחיית הזמנה
        {
            try
            {
                HostingUnit unit = (HostingUnit)(unitCB.SelectedItem);
                Order order = (Order)(orderLV.SelectedItem);
                if (order == null)
                    throw new NullReferenceException("לא נבחרה הזמנה");
                myBL.rejectOrderSp(order);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
 
    }
}
