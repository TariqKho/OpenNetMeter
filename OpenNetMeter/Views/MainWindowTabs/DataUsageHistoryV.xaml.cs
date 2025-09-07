using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenNetMeter.Models;
using OpenNetMeter.ViewModels;

namespace OpenNetMeter.Views
{
    /// <summary>
    /// Interaction logic for DataUsageHistoryV.xaml
    /// </summary>
    public partial class DataUsageHistoryV : UserControl
    {
        public DataUsageHistoryV()
        {
            InitializeComponent();
        }

        private void AllAppsData_LayoutUpdated(object sender, EventArgs e)
        {
            Total.Width = AllAppsData.Columns[0].ActualWidth;
            TotalDataRecv.Width = AllAppsData.Columns[1].ActualWidth;
            TotalDataSent.Width = AllAppsData.Columns[2].ActualWidth;
        }
        public void MouseDoubleClickBtn(object sender, MouseEventArgs e)
        {
            ViewNamesVM viewNamesVM = new ViewNamesVM();
            viewNamesVM.IsVisible = Visibility.Visible;

            var dataGrid = sender as DataGrid;
            if (dataGrid == null || dataGrid.SelectedItem == null)
                return;
            var SelectedItem = dataGrid.SelectedItem as MyProcess_Small;

            if (SelectedItem == null)
                return;

            string ItemId = "";
            string ItemName = "";
            string ItemPreferedName = "";
            //Original name else SearchPrefered

            dynamic namesObj = viewNamesVM.getOriginalName(SelectedItem.Name!);
            if (namesObj.Count > 0)
            {
                // Cast to List<List<object>>
                var namesList = ((List<List<object>>)namesObj)[0];
                // Access the first item in that inner list
                ItemId = namesList[0].ToString()!;
                ItemName = namesList[1].ToString()!;
                ItemPreferedName = namesList[2].ToString()!;

            }
            else
            {
                var PreferednamesObj = viewNamesVM.getPreferedName(SelectedItem.Name!);
                var namesList = ((List<List<object>>)PreferednamesObj)[0];
                // Access the first item in that inner list
                ItemId = namesList[0].ToString()!;
                ItemName = namesList[1].ToString()!;
                ItemPreferedName = namesList[2].ToString()!;
            }
            var dialog = new ThreeTextfieldBox(
                        ItemId,
                        ItemName,
                        ItemPreferedName
                    );
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                var res = viewNamesVM.setPreferedName(dialog.ProcessOriginal, dialog.ProcessPreferedName);
                if (res == 1)
                {
                    MessageBox.Show("Success", "OpenNetMeter", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Prefered Name already exist in database", "Error", MessageBoxButton.OK);
                    //prevent the form from closing
                    MouseDoubleClickBtn(sender, e);
                }

            }
        }
    }
}
