using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ADBackend.DAL.Interfaces;
using ADBackend.objects;
using ADBackend.Objects;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Logging;

namespace ADBackend.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        

        private readonly ILogger<ItemsController> _logger;
        private readonly IItemRepo  _itemRepo;
        private readonly IUserRepo _userRepo;

        public ItemsController(ILogger<ItemsController> logger, IItemRepo itemRepo, IUserRepo userRepo)
        {
            _logger = logger;
            _itemRepo = itemRepo;
            _userRepo = userRepo;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<ItemsObject>), 200)]
        public IActionResult Get()
        {
            return Ok(_itemRepo.GetAllItemsDS());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<ItemsObject>), 200)]
        public IActionResult GetById([FromRoute] int id)
        {
            return Ok(_itemRepo.GetItemByIdDS(id));
        }

        [HttpPost("create")]
        public IActionResult CreateItem([FromForm] ItemsObject itemsObject)
        {
            var tokenResult = ValidateFirebaseToken(itemsObject.Token);

            if (tokenResult == null)
                return Unauthorized();

            return Ok(_itemRepo.CreateItem(itemsObject));
        }

        [HttpPost("update")]
        public IActionResult UpdateItem([FromForm] ItemsObject itemsObject)
        {
            var tokenResult = ValidateFirebaseToken(itemsObject.Token);

            if (tokenResult == null)
                return Unauthorized();

            return Ok(_itemRepo.UpdateItem(itemsObject));
        }

        [HttpPost("delete")]
        public IActionResult DeleteItem([FromForm] int id, string token)
        {
            var tokenResult = ValidateFirebaseToken(token);

            if (tokenResult == null)
                return Unauthorized();

            return Ok(_itemRepo.DeleteItem(id));
        }

        [HttpPost("is-admin")]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult IsUserAdmin([FromBody] string token)
        {
            var tokenResult = ValidateFirebaseToken(token);

            if (tokenResult == null)
                return Unauthorized();
            return Ok(_userRepo.IsUserAdmin(tokenResult));
        }
        [HttpPost("create-order")]
        public IActionResult CreateOrder([FromBody] BasketObject basket)
        {
            var tokenResult = ValidateFirebaseToken(basket.Token);
            if (tokenResult == null)
                return Unauthorized();
            return Ok(_itemRepo.CreateOrder(basket, tokenResult));
        }
        [HttpPost("get-orders")]
        public IEnumerable<BasketObject> GetOrders([FromForm] string token)
        {
            var tokenResult = ValidateFirebaseToken(token);
          
            return _itemRepo.GetOrders(tokenResult);
        }
        [HttpPost("get-admin-orders")]
        public IEnumerable<BasketObject> GetAdminOrders([FromForm] string token)
        {
            var tokenResult = ValidateFirebaseToken(token);

            return _itemRepo.GetAdminOrders(tokenResult);
        }
        private string ValidateFirebaseToken(string token)
        {
            try
            {
                FirebaseToken decodedToken = FirebaseAuth.DefaultInstance
                    .VerifyIdTokenAsync(token).Result;
                return decodedToken.Uid;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            
        }
    }
}
