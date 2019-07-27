using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Model
{
    public class SaleModel
    {
        public SaleModel(string[] array)
        {
            Id = Convert.ToInt32(array[0]);
            CodSale = Convert.ToInt32(array[1]);
            SaleItemList = new List<SaleItem>();

            foreach (var item in array[2].Split(','))
                SaleItemList.Add(new SaleItem(item.Replace("[", "").Replace("]", "").Split('-')));

            SalesmanName = array[3];

            Total = SaleItemList.Sum(si => si.Quantity * si.Price);
        }

        public int Id { get; set; }
        public int CodSale { get; set; }
        public List<SaleItem> SaleItemList { get; set; }
        public string SalesmanName { get; set; }
        public decimal Total { get; set; }
    }
    
    public class SaleItem
    {
        public SaleItem(string[] array)
        {
            Id = Convert.ToInt32(array[0]);
            Quantity = Convert.ToInt32(array[1]);
            Price = Convert.ToDecimal(array[2]);
        }

        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
