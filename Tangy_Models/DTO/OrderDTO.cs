using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Models.DTO
{
    public class OrderDTO
    {
        public OrderHeaderDTO OrderHeader { get; set; } = new();
        public List<OrderDetailDTO> OrderDetail { get; set; }=new List<OrderDetailDTO>();
    }
}
