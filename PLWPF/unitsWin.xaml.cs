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
            lv.ItemsSource = myBL.getAllUnits();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//remove
        {
            try
            {
                HostingUnit unit = (HostingUnit)(lv.SelectedItem);
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

        private void Button_Click_1(object sender, RoutedEventArgs e)//edit
        {
            try
            {
                HostingUnit unit = (HostingUnit)(lv.SelectedItem);
                if (unit == null)
                    throw new NullReferenceException("לא נבחרה יחידת אירוח");
                editUnitWin win = new editUnitWin(unit);
                this.Close();
                win.ShowDialog();
             }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
            
            //try
            //{
            //    HostingUnit unit = (HostingUnit)(lv.SelectedItem);
            //    myBL.updateHostingUnit(unit);
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show(error.Message);
            //}
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//add unit
        {
            addUnitWin win = new addUnitWin();
            this.Close();
            win.ShowDialog();
        }
    }
}
