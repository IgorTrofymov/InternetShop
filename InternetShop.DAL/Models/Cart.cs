using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Models
{
    public class Cart
    {
        List<CartLine> lineCollection = new List<CartLine>();
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.FirstOrDefault(p => p.Product.ProductId == product.ProductId);
            if (line == null)
            {
                lineCollection.Add(new CartLine {Product = product, Quantity = quantity});
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveItem(Product product)
        {
            lineCollection.RemoveAll(u => u.Product.ProductId == product.ProductId);
        }

        public decimal ComputeTotalValue()
        {
            return (decimal)lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
