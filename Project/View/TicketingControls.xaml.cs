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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project.View
{
    /// <summary>
    /// Interaction logic for TicketingControls.xaml
    /// </summary>
    public partial class TicketingControls : UserControl
    {
        public TicketingControls()
        {
            InitializeComponent();
        }

        private void BtnReserveringToevoegen_Click(object sender, RoutedEventArgs e)
        {
            AddMenuWrap.Height = 400;
            ZoekMenuWrap.Height = 0;
        }

        private void BtnZoek_Click(object sender, RoutedEventArgs e)
        {
            AddMenuWrap.Height = 0;
            ZoekMenuWrap.Height = 250;
        }

        private void BtnReserveringWijzigen_Click(object sender, RoutedEventArgs e)
        {
            AddMenuWrap.Height = 400;
            ZoekMenuWrap.Height = 0;
        }
    }
}
