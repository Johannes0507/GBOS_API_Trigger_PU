using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DrLightCutterAPITrigger.Helper
{
    public class EnumHelper
    {
        /// <summary>
        /// Get Enum Description
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
