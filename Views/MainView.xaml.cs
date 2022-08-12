using RoundMenu.Models;
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

namespace RoundMenu.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }
        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = e.OriginalSource as MenuItem;
            var MenuName = (ListMenuItem)menuItem.DataContext;
            ListMenuItemEnum.EnuMy = MenuName.MenuEnum;
        }
    }
}
