using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecieptGenerator
{
    class Item
    {
        public string name { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public int amount { get; set; }

        public Item(string iname,int iprice, int iquantity)
        {
            this.name = iname;
            this.price = iprice;
            this.quantity = iquantity;
            this.amount = this.price * this.quantity;
        }
    }
}
