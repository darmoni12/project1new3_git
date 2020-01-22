using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
    /// Interaction logic for hostsWin.xaml
    /// </summary>
    public partial class hostsWin : Window
    {
        public IBL myBL = BLI.GetBL();
        public hostsWin()
        {
            InitializeComponent();
            lv.ItemsSource = myBL.getAllHosts();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//edit
        {
            try
            {
                Host host = (Host)(lv.SelectedItem);
                if (host == null)
                    throw new NullReferenceException("לא נבחר מארח");
                editHostWin win = new editHostWin(host);
                this.Close();
                win.ShowDialog();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//add
        {
            addHostWin win = new addHostWin();
            this.Close();
            win.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//enter
        {
            try
            {
                Host host = (Host)(lv.SelectedItem);
                if (host == null)
                    throw new NullReferenceException("לא נבחר מארח");
                if (!host.CheckPassword(password.Password))
                    throw new wrongPasswordException("סיסמא שגויה");
                hostWin win = new hostWin(host);
                this.Close();
                win.ShowDialog();
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }

   
}
