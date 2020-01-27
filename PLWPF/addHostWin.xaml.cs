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
    /// Interaction logic for addHostWin.xaml
    /// </summary>
    public partial class addHostWin : Window
    {
        public IBL myBL = BLI.GetBL();
        Host host = new Host();
        public addHostWin()
        {
            InitializeComponent();
            addHostGrid.DataContext = host;
            cb.ItemsSource = myBL.getAllBankBranch();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//add host
        {
            //try
            //{
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
                myBL.addHost(host);
                this.Close();
            //}
            //catch (Exception error)
            //{
            //    MessageBox.Show(error.Message);
            //}
        }
    }
}
