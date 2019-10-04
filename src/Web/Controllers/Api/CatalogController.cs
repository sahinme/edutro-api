using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    public class CatalogController : BaseApiController
    {

        private readonly IBasketService _basketService;
        public CatalogController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<Basket> List(int id)
        {
            return await _basketService.GetBasketAsync(id);
        }
    }
}