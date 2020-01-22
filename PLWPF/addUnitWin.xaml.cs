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
    /// Interaction logic for addUnitWin.xaml
    /// </summary>
    public partial class addUnitWin : Window
    {
        public IBL myBL = BLI.GetBL();
        public addUnitWin()
        {
            InitializeComponent();
            areaCB.ItemsSource = Enum.GetValues(typeof(Area)).Cast<Area>().Where(area => area != Area.All);
            typeCB.ItemsSource = Enum.GetValues(typeof(HostingType)).Cast<HostingType>();
            ownerCB.ItemsSource = myBL.getAllHosts();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HostingUnit Unit = new HostingUnit();
                Unit.Diary = new Diary();
                Unit.OwnerHostKey = ((Host)ownerCB.SelectedItem).HostKey;
                Unit.Area = (Area)areaCB.SelectedItem;
                Unit.Type = (HostingType)typeCB.SelectedItem;
                if(unitNameTB.Text=="")
                    throw new textExeption("unit name");
                Unit.HostingUnitName = unitNameTB.Text;
                if(addressTB.Text=="")
                    throw new textExeption("address");
                Unit.Address = addressTB.Text;
                Unit.MaxPeople = int.Parse(maxPeopleTB.Text);
                Unit.Jacuzzi = (bool)jacuzziCB.IsChecked;
                Unit.Garden = (bool)gardenCB.IsChecked;
                Unit.Pool = (bool)poolCB.IsChecked;
                Unit.ChildrensAttractions = (bool)childrenAtractionCB.IsChecked;
                Unit.Breakfast = (bool)breakfastCB.IsChecked;
                Unit.HB = (bool)HBCB.IsChecked;
                Unit.FB = (bool)FBCB.IsChecked;
                Unit.BedOnly = (bool)bedOnlyCB.IsChecked;
                Unit.FreeParking = (bool)freeParkingCB.IsChecked;
                Unit.FreeWifi = (bool)freeWifiCB.IsChecked;
                myBL.addHostingUnit(Unit);
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
