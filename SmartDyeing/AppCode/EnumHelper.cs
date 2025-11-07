using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CHNSpec.Device.COM.Demo
{
    public static class EnumHelper
    {
        /// <summary> 
        /// 获得枚举类型数据项（不包括空项）  text为枚举描述 value为枚举值 name为枚举名称
        /// </summary> 
        /// <param name="enumType">枚举类型</param> 
        /// <returns></returns> 
        public static ArrayList GetItems(this Type enumType)
        {
            if (!enumType.IsEnum)
                throw new InvalidOperationException();

            ArrayList list = new ArrayList();

            // 获取Description特性 
            Type typeDescription = typeof(DescriptionAttribute);
            // 获取枚举字段
            FieldInfo[] fields = enumType.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (!field.FieldType.IsEnum)
                    continue;
                // 获取枚举值
                int value = (int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
                //获取枚举名称
                string name = enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null).ToString();
                string text = string.Empty;
                object[] array = field.GetCustomAttributes(typeDescription, false);

                if (array.Length > 0) text = ((DescriptionAttribute)array[0]).Description;
                else text = field.Name; //没有描述，直接取值

                //添加到列表
                //list.Add(new { Value = value, Text = text, Name = name });
                list.Add(new DictionaryEntry(value, text));
            }
            return list;
        }

        /// <summary>
        /// 枚举获取对应的注释属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum value)
        {
            if (value == null)
                return "";

            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return string.Empty;
            object[] attribArray = fieldInfo.GetCustomAttributes(false);
            if (attribArray.Length == 0)
                return value.ToString();
            else
                return (attribArray[0] as DescriptionAttribute).Description;
        }
    }
}
