using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundMenu.Models
{
    /// <summary>
    /// 圆形菜单属性类
    /// </summary>
    public class CircleMenuItem : BindableBase
    {
        public CircleMenuItem()
        {
        }
        /// <summary>
        /// 编号
        /// </summary>
        private int m_id;
        public int Id
        {
            get { return m_id; }
            set { SetProperty(ref m_id, value); }
        }
        private string m_title;
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string Title
        {
            get { return m_title; }
            set { SetProperty(ref m_title, value); }
        }
        private string m_titleIcon;
        /// <summary>
        /// 按钮Icon
        /// </summary>
        public string TitleIcon
        {
            get { return m_titleIcon; }
            set { SetProperty(ref m_titleIcon, value); }
        }
        private CircleMenuItemEnum m_menuEnum;
        /// <summary>
        /// 按钮枚举
        /// </summary>
        public CircleMenuItemEnum MenuEnum
        {
            get { return m_menuEnum; }
            set { SetProperty(ref m_menuEnum, value); }
        }
    }
    /// <summary>
    /// 按钮列表类
    /// </summary>
    public class ListMenuItem : BindableBase
    {
        private string m_title;
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string Title
        {
            get { return m_title; }
            set { SetProperty(ref m_title, value); }
        }
        private string m_titleIcon;
        /// <summary>
        /// 按钮Icon
        /// </summary>
        public string TitleIcon
        {
            get { return m_titleIcon; }
            set { SetProperty(ref m_titleIcon, value); }
        }
        private CircleMenuItemEnum m_menuEnum;
        /// <summary>
        /// 按钮枚举属性
        /// </summary>
        public CircleMenuItemEnum MenuEnum
        {
            get { return m_menuEnum; }
            set { SetProperty(ref m_menuEnum, value); }
        }
    }

    public static class ListMenuItemEnum
    {
        public static CircleMenuItemEnum EnuMy { get; set; }
    }
    /// <summary>
    /// 按钮枚举
    /// </summary>
    public enum CircleMenuItemEnum
    {
        [Description("库位启动")]
        LocationStart,
        [Description("多库位启动")]
        LocationStarts,
        [Description("库位续接")]
        LocationContinue,
        [Description("库位暂停")]
        LocationPause,
        [Description("库位停止")]
        LocationStop,
        [Description("压床压合")]
        PressMerge,
        [Description("压床脱开")]
        PressApart,
        [Description("设置工步")]
        SettingSTEP,
        [Description("清除异常")]
        ClearAbnormal
    }
}
