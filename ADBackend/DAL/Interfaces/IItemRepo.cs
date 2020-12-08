using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADBackend.objects;

namespace ADBackend.DAL.Interfaces
{
    public interface IItemRepo
    {
        //IEnumerable GetAllItems();
        //IEnumerable GetItemByID(int Id);

        IEnumerable GetAllItemsDS();
        IEnumerable GetItemByIdDS(int Id);
        IEnumerable<BasketObject> GetOrders(string uid);
        IEnumerable<BasketObject> GetAdminOrders(string uid);

        bool CreateItem(ItemsObject itemsObject);
        bool DeleteItem(int id);
        bool UpdateItem(ItemsObject itemsObject);

        bool CreateOrder(BasketObject basket, string uid);
    }
}
