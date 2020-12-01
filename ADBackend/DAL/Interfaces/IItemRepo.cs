using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADBackend.DAL.Interfaces
{
    public interface IItemRepo
    {
        //IEnumerable GetAllItems();
        //IEnumerable GetItemByID(int Id);

        IEnumerable GetAllItemsDS();
        IEnumerable GetItemByIdDS(int Id);
        bool CreateItem(ItemsObject itemsObject);

    }
}
