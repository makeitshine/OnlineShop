using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aurora.Data.Models
{
    public class WishListItem
    {
        public int WishListItemId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public string WishListId { get; set; }
    }
}