using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebsiteTest1.Models
{
    public class CartItemModel
    {
        public virtual ProductItemModel ProductItem { get; set; }
        public int Quantity { get; set; }

        public CartItemModel(ProductItemModel productItem, int quantity)
        {
            this.ProductItem = productItem;
            this.Quantity = quantity;
        }

        private CartItemModel()
        {

        }
    }
}
