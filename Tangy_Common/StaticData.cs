using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Common
{
    public static class StaticData
    {
        public static string ShoppingCart { get; set; } = "ShoppingCart";
        public static string Order_Pending { get; set; } = "Pending";
        public static string Order_Confirmed { get; set; } = "Confirmed";
        public static string Order_Cancelled { get; set; } = "Cancelled";
        public static string Order_Refunded { get; set; } = "Refuned";
        public static string Order_Shipped { get; set; } = "Shipped";
    }
}
