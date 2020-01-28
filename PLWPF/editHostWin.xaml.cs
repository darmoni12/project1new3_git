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
    /// Interaction logic for editHostWin.xaml
    /// </summary>
    public partial class editHostWin : Window
    {
        Host host;
        public IBL myBL = BLI.GetBL();
        public editHostWin(Host Host)
        {
            InitializeComponent();
            this.host = Host;
            editHostGrid.DataContext = host;
            cb.ItemsSource = myBL.getAllBankBranch();
            cb.SelectedItem = host.BankBranchDetails;
        }

        private void Button_Click(object sender, RoutedEventArgs e)//update host
        {
            try
            {
                if (pass.Password == "")
                    throw new textExeption("Password");
                host.Password = pass.Password;
                if (host.PrivateName == "")
                    throw new textExeption("first name");
                if (host.FamilyName == "")
                    throw new textExeption("last name");
                if (host.PhoneNumber == "")
                    throw new textExeption("phone number");
                if (host.MailAddress == "")
                    throw new textExeption("mail address");
                if (cb.SelectedItem == null)
                    throw new NullReferenceException("לא נבחר סניף בנק");
                host.BankBranchDetails = (BankBranch)cb.SelectedItem;
                if (host.BankAccountNumber == 0)
                    throw new textExeption("Bank Account Number");
                host.CollectionClearance = true;                
                myBL.updateHost(host);
                this.Close();
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
