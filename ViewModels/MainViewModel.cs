using Prism.Commands;
using Prism.Mvvm;
using RoundMenu.CircleMenu;
using RoundMenu.Common;
using RoundMenu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RoundMenu.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region 属性
        private static CircleMenuItem SubMenu;
        private static ListMenuItem VeaEnum;
        public List<CircleMenuItemEnum> CircleEnumList = Enum.GetValues(typeof(CircleMenuItemEnum)).Cast<CircleMenuItemEnum>().ToList();

        private ObservableCollection<CircleMenuItem> m_subMenuItems;
        /// <summary>
        /// 圆形菜单属性类
        /// </summary>
        public ObservableCollection<CircleMenuItem> SubMenuItems
        {
            get { return m_subMenuItems; }
            set { SetProperty(ref m_subMenuItems, value); }
        }
        private ObservableCollection<ListMenuItem> m_menuEnumList;
        /// <summary>
        /// 按钮更换列表
        /// </summary>
        public ObservableCollection<ListMenuItem> MenuEnumList
        {
            get { return m_menuEnumList; }
            set { SetProperty(ref m_menuEnumList, value); }
        }
        #endregion

        /// <summary>
        /// 构造
        /// </summary>
        public MainViewModel()
        {
            SubMenuItems = new ObservableCollection<CircleMenuItem>();
            MenuEnumList = new ObservableCollection<ListMenuItem>();
            for (int i = 0; i < 8; i++)//这里应该是读取JSON文件  如果没有才来这里默认
            {
                SubMenuItems.Add(new CircleMenuItem() { Id = i + 1, Title = CircleEnumList[i].GetEnumDescription(), TitleIcon = CircleEnumList[i].ToString(), MenuEnum = CircleEnumList[i] });
            }
            for (int i = 0; i < CircleEnumList.Count; i++)
            {
                var itemsCount = SubMenuItems.Where(x => x.MenuEnum == CircleEnumList[i]).ToList().Count;
                if (itemsCount == 0)
                    MenuEnumList.Add(new ListMenuItem() { Title = CircleEnumList[i].GetEnumDescription(), TitleIcon = CircleEnumList[i].ToString(), MenuEnum = CircleEnumList[i] });
            }

        }

        #region ICmmand
        private DelegateCommand<RoutedEventArgs> m_nodeClickCommand;
        /// <summary>
        /// 按钮菜单点击
        /// </summary>
        public DelegateCommand<RoutedEventArgs> NodeClickCommand => m_nodeClickCommand ?? (m_nodeClickCommand = new DelegateCommand<RoutedEventArgs>(ExecuteNodeClickCommand));

        private DelegateCommand<object> m_csdeCommand;
        /// <summary>
        /// 切换按钮
        /// </summary>
        public ICommand CsdeCommand => m_csdeCommand ?? (m_csdeCommand = new DelegateCommand<object>(ExecuteCsdeCommand));
        #endregion
        #region Command事件
        /// <summary>
        /// 切换按钮
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteCsdeCommand(object obj)
        {
            Border dd = obj as Border;
            SubMenu = (CircleMenuItem)dd.DataContext;
            //VeaEnum = (ListMenuItem)dd.ContextMenu.Items.CurrentItem;//按钮列表选中
            VeaEnum = MenuEnumList.Where(x => x.MenuEnum == ListMenuItemEnum.EnuMy).FirstOrDefault();
            var sad = dd.ContextMenu.Items.CurrentPosition;
            var ddd = dd.ContextMenu.GetType().GetProperty("FocusedInfo", BindingFlags.Instance | BindingFlags.NonPublic);
            MenuEnumList.Remove(VeaEnum);
            MenuEnumList.Add(new ListMenuItem() { Title = SubMenu.Title, MenuEnum = SubMenu.MenuEnum, TitleIcon = SubMenu.MenuEnum.ToString() });
            SubMenuItems.ToList().ForEach(item =>
            {
                if (item.MenuEnum == SubMenu.MenuEnum)
                {
                    item.MenuEnum = VeaEnum.MenuEnum;
                    item.Title = VeaEnum.Title;
                    item.TitleIcon = VeaEnum.TitleIcon;
                }
            });
            //生成JSON文件保存起来 下次启动软件再读取
        }
        /// <summary>
        /// 圆盘菜单按钮点击事件
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ExecuteNodeClickCommand(RoutedEventArgs obj)
        {
            Button button = obj.OriginalSource as Button;
            try
            {
                if (!string.IsNullOrEmpty(button.Name)) return;
                var dataItem = ((FrameworkElement)obj.OriginalSource).DataContext;
                MessageBox.Show(((CircleMenuItem)dataItem).Id.ToString());
                var circleCtrl = (CircleMenuControl)obj.Source;
                //var suc = VisualStateManager.GoToState(circleCtrl, CircleMenuControl.VisualStateCollapsed, false);
                var bb = 1;
            }
            catch (Exception ex)
            {


            }

        }
        #endregion
    }
}
