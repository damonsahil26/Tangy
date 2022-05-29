using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Common
{
    public static class StaticData
    {
        public const string ShoppingCart = "ShoppingCart";

        public const string Order_Pending = "Pending";
        public const string Order_Confirmed = "Confirmed";
        public const string Order_Cancelled = "Cancelled";
        public const string Order_Refunded = "Refunded";
        public const string Order_Shipped = "Shipped";

        public const string Role_Admin = "Admin";
        public const string Role_Customer = "Customer";

        public const string Local_Token = "Jwt Token";
        public const string Local_UserDetails = "User Details";
        public const string Local_OrderDetails = "Local_OrderDetails";
    }
}
