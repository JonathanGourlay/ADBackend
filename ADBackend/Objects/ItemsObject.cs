using System;

namespace ADBackend
{
    public class ItemsObject : Item
    {
        public int ItemID { get; set; }
        public string Name { get; set; }

        public int StockCount { get; set; }

        public float Price { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }
        
        
    }

    public class Item
    {
        public string Token { get; set; }
    }
}
