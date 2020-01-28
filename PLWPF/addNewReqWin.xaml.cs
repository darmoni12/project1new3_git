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
    /// Interaction logic for addNewReqWin.xaml
    /// </summary>
    public partial class addNewReqWin : Window
    {
        public IBL myBL = BLI.GetBL();

        public addNewReqWin()
        {
            InitializeComponent();
            areaCB.ItemsSource = Enum.GetValues(typeof(Area)).Cast<Area>();
            typeCB.ItemsSource = Enum.GetValues(typeof(HostingType)).Cast<HostingType>();
            poolCB.ItemsSource = Enum.GetValues(typeof(Require)).Cast<Require>();
            jacuzziCB.ItemsSource = Enum.GetValues(typeof(Require)).Cast<Require>();
            gardenCB.ItemsSource = Enum.GetValues(typeof(Require)).Cast<Require>();
            childrenAtractionCB.ItemsSource = Enum.GetValues(typeof(Require)).Cast<Require>();
            foodCB.ItemsSource = Enum.GetValues(typeof(Food)).Cast<Food>();
            freeWifiCB.ItemsSource = Enum.GetValues(typeof(Require)).Cast<Require>();
            freeParkingCB.ItemsSource = Enum.GetValues(typeof(Require)).Cast<Require>();
        }
        private void submitButton(object sender, RoutedEventArgs e)
        {
            try
            {
                GuestRequest req = new GuestRequest() { };
                if (firstNameTB.Text == "")
                    throw new textExeption("first name");
                req.PrivateName = firstNameTB.Text;
                if (lastNameTB.Text == "")
                    throw new textExeption("last name");
                req.FamilyName = lastNameTB.Text;
                System.Net.Mail.MailAddress address = new System.Net.Mail.MailAddress(mailTB.Text);
                req.MailAddress = address.Address;
                req.ActiveStatus = true;
                req.Area = (Area)(Enum.Parse(typeof(Area), areaCB.Text, true));
                req.Type = (HostingType)(Enum.Parse(typeof(HostingType), typeCB.Text, true));
                req.Adults = int.Parse(adultTB.Text);
                req.Children = int.Parse(childrenTB.Text);
                req.Pool = (Require)(Enum.Parse(typeof(Require), poolCB.Text, true));
                req.Jacuzzi = (Require)(Enum.Parse(typeof(Require), jacuzziCB.Text, true));
                req.Garden = (Require)(Enum.Parse(typeof(Require), gardenCB.Text, true));
                req.ChildrensAttractions = (Require)(Enum.Parse(typeof(Require), childrenAtractionCB.Text, true));
                req.Food = (Food)(Enum.Parse(typeof(Food), foodCB.Text, true));
                req.FreeParking = (Require)(Enum.Parse(typeof(Require), freeParkingCB.Text, true));
                req.FreeWifi = (Require)(Enum.Parse(typeof(Require), freeWifiCB.Text, true));
               
                req.EntryDate = new MyDate(entryD.Text);
                req.ReleaseDate = new MyDate(releaseD.Text);
                if (req.EntryDate.Year != 2020 || req.ReleaseDate.Year != 2020)
                    throw new BE. yearExeption();
                req.RegistrationDate = MyDate.today();
                myBL.addRequest(req);
                MessageBox.Show("secces");
                this.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }

    
}
