using System;
using System.Collections.Generic;

namespace ADBackend.objects 
{
    public class basketObject : Item
    {
       public List<basketItem> BasketItems { get; set; }

    }

    public class basketItem
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
