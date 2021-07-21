using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G1_Website_Tour.Models
{
    public class CartItem
    {
        public tour tours { get; set; }
        public int quantity { get; set; }
    }
}