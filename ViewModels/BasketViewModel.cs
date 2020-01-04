using Aurora.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aurora.ViewModels
{
    public class BasketViewModel
    {
        public Basket Basket { get; set; }
        public decimal BasketTotal { get; set; }
    }
}