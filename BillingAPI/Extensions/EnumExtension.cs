using System;
using System.Collections.Generic;
using System.Linq;

namespace BillingAPI.Extensions
{
    /// <summary>
    /// Common Enum methods
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Parsing int to Enum of T instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int param)
        {
            var info = typeof(T);
            if (info.IsEnum)
            {
                T result = (T)Enum.Parse(typeof(T), param.ToString(), true);
                return result;
            }

            return default;
        }

        public static T ToEnum<T>(this string param)
        {
            var info = typeof(T);
            if (info.IsEnum)
            {
                T result = (T)Enum.Parse(typeof(T), param, true);
                return result;
            }

            return default;
        }
        public static string Description(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(false);

            dynamic displayAttribute = null;

            if (attributes.Any())
                displayAttribute = attributes.ElementAt(0);

            return displayAttribute?.Description ?? "Description Not Found";
        }

        public static List<string> GetDataSourceTypes<T>(this T enumType)
        {
            return Enum.GetNames(typeof(T)).ToList();
        }

        public static string GetEnumStringName<T>(this T param) where T : Enum
        {
            return Enum.GetName(typeof(T), param);
        }
    }
}
