using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ADBackend.DAL.Interfaces;
using ADBackend.objects;
using Dapper;
using Google.Cloud.Datastore.V1;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ADBackend.DAL.Repository
{
    public class ItemRepo :  BaseRepository, IItemRepo
    {
        private readonly DatastoreDb _db;
        public ItemRepo(IOptions<ConnectionStrings> connectionStrings) : base(connectionStrings.Value)
        {
              _db = DatastoreDb.Create(connectionStrings.Value.DataStore);
        }

        public IEnumerable GetAllItemsDS()
        {

            Query query = new Query("Stock")
            {
            };
            var results = _db.RunQuery(query).Entities;
            return results.Select(x => new ItemsObject { Description = x.Properties["Description"].StringValue, ItemID = (int)x.Properties["ItemID"].IntegerValue, Name = x.Properties["Name"].StringValue, Price = (float)x.Properties["Price"].DoubleValue, StockCount = (int)x.Properties["Stock Count"].IntegerValue, ImageURL = x.Properties["Image URL"].StringValue }); ;
        }

        public IEnumerable GetItemByIdDS(int Id)
        {

            Query query = new Query("Stock")
            {
                Filter = Filter.And(Filter.Equal("ItemID",Id))
            };
            var results = _db.RunQuery(query).Entities;
            return results.Select(x => new ItemsObject { Description = x.Properties["Description"].StringValue, ItemID = (int)x.Properties["ItemID"].IntegerValue, Name = x.Properties["Name"].StringValue, Price = (float)x.Properties["Price"].DoubleValue, StockCount = (int)x.Properties["Stock Count"].IntegerValue, ImageURL = x.Properties["Image URL"].StringValue, }); ;
        }

        public bool CreateItem(ItemsObject itemsObject)
        {
            try
            {
                var task = new Entity()
                {
                    Key = _db.CreateKeyFactory("Stock").CreateIncompleteKey(),
                    ["ItemID"] = itemsObject.ItemID,
                    ["Name"] = itemsObject.Name,
                    ["Description"] = itemsObject.Description,
                    ["Price"] = itemsObject.Price,
                    ["Stock Count"] = itemsObject.StockCount,
                    ["Image URL"] = itemsObject.ImageURL
                };
                task.Key = _db.Insert(task);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e); 
                throw;
            }
}
        public bool CreateOrder(basketObject basket, string uid)
        {
            try
            {
                var task = new Entity()
                {
                    Key = _db.CreateKeyFactory("Orders").CreateIncompleteKey(),
                    ["Basket"] = JsonConvert.SerializeObject(basket.BasketItems),
                    ["Uid"] = uid,
                };
                task.Key = _db.Insert(task);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool UpdateItem(ItemsObject itemsObject)
        {
            try
            {
                Query query = new Query("Stock")
                {
                    Filter = Filter.And(Filter.Equal("ItemID", itemsObject.ItemID))
                };
                var results = _db.RunQuery(query).Entities;
                var item = results.FirstOrDefault();
                var task = new Entity()
                {
                    Key = _db.CreateKeyFactory("Stock").CreateKey(item.Key.Path.First().Id),
                    ["ItemID"] = itemsObject.ItemID,
                    ["Name"] = itemsObject.Name,
                    ["Description"] = itemsObject.Description,
                    ["Price"] = itemsObject.Price,
                    ["Stock Count"] = itemsObject.StockCount,
                    ["Image URL"] = itemsObject.ImageURL
                };
                task.Key = _db.Upsert(task);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public bool DeleteItem(int Id)
        {
            Query query = new Query("Stock")
            {
                Filter = Filter.And(Filter.Equal("ItemID", Id))
            };
            var results = _db.RunQuery(query).Entities;
            var item = results.FirstOrDefault();
            _db.Delete(new Key().WithElement("Stock", item.Key.Path.First().Id));
            return true;
        }

        //public  IEnumerable GetAllItems()
        //{
        //    ItemsObject itemsObject = new ItemsObject();

        //    var items = new List<object>(){new {name="Fish",price=1.99},new {name="Dog",price=199.99} };

        //    var result =  ExecuteFunc(qry => qry.Query<ItemsObject>(SQL.GetItems));

        //    return result;
        //}
        //public IEnumerable GetItemByID(int Id)
        //{
        //    ItemsObject itemsObject = new ItemsObject();

        //    var result = ExecuteFunc(qry => qry.Query<ItemsObject>(SQL.GetItemByID, new
        //    {
        //       Id = Id
        //    }));

        //    return result;
        //}
    }
}

