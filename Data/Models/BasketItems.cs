using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aurora.Data.Models
{
    public class BasketItem
    {
        public int BasketItemId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public string BasketId { get; set; }
    }
}
