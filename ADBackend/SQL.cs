using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADBackend
{
    public class SQL
    {
        public const string GetItems = @"
        SELECT TOP (1000)
	        [ItemID]
              ,[Name]
              ,[Description]
              ,[Price]
              ,[StockCount]
              ,[ProductImage]
          FROM [dbo].[Items_Table]
        ";

        public const string GetItemByID = @"
        SELECT TOP (1000)
	        [ItemID]
              ,[Name]
              ,[Description]
              ,[Price]
              ,[StockCount]
              ,[ProductImage]
        FROM [dbo].[Items_Table]
        WHERE ItemId = @Id    
        ";

        public const string Create = @"
        INSERT INTO [dbo].[Items_Table]
           ([ItemID]
           ,[Name]
           ,[Description]
           ,[Price]
           ,[StockCount]
           ,[ProductImage])
     VALUES
            (@Name,
             @Price,
             @Description,
             @StockCount,
             @ProductImage)
         ";
    }
}
