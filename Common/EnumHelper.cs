using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoundMenu.Common
{
    /// <summary>
    /// 枚举助手
    /// </summary>
    public static class EnumHelper
    {
        private static ConcurrentDictionary<Enum, string> m_enumsDictionary = new ConcurrentDictionary<Enum, string>();
        /// <summary>
        /// 获取Description描述内容
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum enumValue)
        {
            string value = enumValue.ToString();
            //string DescriptionValue = m_enumsDictionary.GetValueOrDefault(enumValue);
            if (!m_enumsDictionary.ContainsKey(enumValue))
            {
                FieldInfo field = enumValue.GetType().GetField(value);
                object[] objs = field?.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
                if (objs == null || objs.Length == 0)    //当描述属性没有时，直接返回名称
                {
                    m_enumsDictionary.TryAdd(enumValue, value);
                    return value;
                }

                DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
                m_enumsDictionary.TryAdd(enumValue, descriptionAttribute.Description);
                return descriptionAttribute.Description;
            }
            else
            {
                return m_enumsDictionary.GetValueOrDefault(enumValue);
            }
        }

        public static T GetEnumByDescription<T>(string description) where T : Enum
        {
            Array enums = Enum.GetValues(typeof(T));
            foreach (var item in enums)
            {
                if (GetEnumDescription((T)item) == description)
                {
                    return (T)item;
                }
            }
            return default(T);
        }

        public static List<(string des, string name, int num)> GetEnumList(Type enumValue)
        {
            List<(string, string, int)> list = new List<(string, string, int)>();
            IEnumerable<FieldInfo> EnumTemp = enumValue.GetFields().Where(u => u.Name != "value__");
            foreach (FieldInfo item in EnumTemp)
            {
                if (item.Name == "value__")
                {
                    continue;
                }
                object[] objs = item?.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
                if (objs == null || objs.Length == 0) continue;
                DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
                var name = item.Name;
                int num = (int)Enum.Parse(enumValue, name);
                list.Add((descriptionAttribute.Description, name, num));
            }
            return list;

        }

        /// <summary>
        /// Flag 枚举 获取单项列表
        /// 会过滤 default
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static List<TEnum> GetSingleEnums<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
        {
            List<TEnum> enums = new List<TEnum>();
            foreach (TEnum item in Enum.GetValues<TEnum>())
            {
                if (item.Equals(default(TEnum)))
                {
                    continue;
                }

                if (enumValue.HasFlag(item))
                {
                    enums.Add(item);
                }
            }

            return enums;
        }

        public static string GetFlagEnumDescription<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
        {
            List<TEnum> enums = GetSingleEnums(enumValue);
            if (enums.Count > 0)
            {
                return string.Join(",", enums.Select(it => it.GetEnumDescription()));
            }

            return string.Empty;
        }
    }
}
