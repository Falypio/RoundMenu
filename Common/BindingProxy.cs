using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RoundMenu.Common
{
    /// <summary>
    /// 绑定代理
    /// </summary>
    public class BindingProxy : Freezable
    {
        public Brush BackgroundColor
        {
            get { return Brushes.Transparent; }
        }
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }
        public object Data
        {
            get { return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));

    }
}
