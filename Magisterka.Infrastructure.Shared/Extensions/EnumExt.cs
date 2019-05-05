using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Magisterka.Infrastructure.Shared.Enum;

namespace Magisterka.Infrastructure.Shared.Extensions
{
    public static class EnumExt
    {
        public static string GetEnumDescription(this System.Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }


        public static DayTypeEnum GetDayTypeEnumFromString(this string value)
        {
            var list = GetEnumValuesList<DayTypeEnum>();

            foreach (var item in list)
            {
                if (item.GetEnumDescription().ToLower() == value.ToLower())
                {
                    return item;
                }
            }

            return DayTypeEnum.Sunday;

        }

        public static List<T> GetEnumValuesList<T>() where T : struct
        {
            var type = typeof(T);

            if (!type.IsEnum) return null; // or throw exception

            return System.Enum.GetValues(type).Cast<T>().ToList();
        }
        



    }
}
