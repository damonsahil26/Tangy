using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_DataAccess
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }
        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [Required]
        [Display(Name = "Shipping Date")]
        public DateTime ShippingDate { get; set; }
        [Required]
        public string Status { get; set; }

        //Related To Stripe Payment
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

        [Required, Display(Name = "Name")]
        public string Name { get; set; }

        [Required, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string State { get; set; }

        [Required]

        public string City { get; set; }

        [Required, Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required, Display(Name = "Address")]
        public string Address { get; set; }
        public string? Tracking { get; set; }
        public string? Carrier { get; set; }
    }
}
