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
    /// Interaction logic for editUnitWin.xaml
    /// </summary>
    public partial class editUnitWin : Window
    {
        public IBL myBL = BLI.GetBL();
        public HostingUnit Unit { get; set; }

        public editUnitWin(HostingUnit unit)
        {
            InitializeComponent();
            this.Unit = unit;
            editUnitWinGrid.DataContext = Unit;
            ownerCB.SelectedItem = myBL.getHost(Unit.OwnerHostKey);
            areaCB.SelectedItem = Unit.Area;
            typeCB.SelectedItem = Unit.Type;
            areaCB.ItemsSource = Enum.GetValues(typeof(Area)).Cast<Area>().Where(area => area != Area.All);
            typeCB.ItemsSource = Enum.GetValues(typeof(HostingType)).Cast<HostingType>();
            ownerCB.ItemsSource = myBL.getAllHosts();
            ownerCB.SelectedIndex = Unit.OwnerHostKey - Configuration.SerialNumBonus;
            jacuzziCB.IsChecked = Unit.Jacuzzi;
            gardenCB.IsChecked = Unit.Garden;
            poolCB.IsChecked = Unit.Pool;
            childrenAtractionCB.IsChecked = Unit.ChildrensAttractions;
            breakfastCB.IsChecked = Unit.Breakfast;
            HBCB.IsChecked = Unit.HB;
            FBCB.IsChecked = Unit.FB;
            bedOnlyCB.IsChecked = Unit.BedOnly;
            freeParkingCB.IsChecked = Unit.FreeParking;
            freeWifiCB.IsChecked = Unit.FreeWifi;
        }

        private void Button_Click(object sender, RoutedEventArgs e)//update
        {
            Unit.OwnerHostKey = ((Host)ownerCB.SelectedItem).HostKey;
            Unit.Area = (Area)areaCB.SelectedItem;
            Unit.Type = (HostingType)typeCB.SelectedItem;

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
            try
            {
                myBL.updateHostingUnit(Unit);
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
