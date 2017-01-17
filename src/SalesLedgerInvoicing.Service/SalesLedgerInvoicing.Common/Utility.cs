using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SalesLedgerInvoicing.Common
{
    public static class Utility
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes.Any() &&
                attributes.Length > 0)
                return attributes[0].Description;

            return value.ToString();
        }

    }
}
