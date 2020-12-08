using System;
using System.Collections.Generic;

namespace ADBackend.objects 
{
    public class BasketObject : Item
    {
        public string Id { get; set; }
        public string Uid { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public double OrderTotal { get; set; }

    }

    public class BasketItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
