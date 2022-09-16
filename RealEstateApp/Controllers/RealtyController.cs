using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Models;
using RealEstateApp.Services.Interfaces;

namespace RealEstateApp.Controllers
{
    [Route("api/[controller]")]
    public class RealtyController : Controller
    {
        private readonly IRealtyService _realtyService;
        public RealtyController(IRealtyService realtyService)
        {
            _realtyService = realtyService;
        }

        [HttpGet("[action]/{input}")]
        public async Task<ActionResult<AutoCompleteModel>> GetAutoComplete(string input)
        {
            return Ok(await _realtyService.GetAutoComplete(input));
        }

        [HttpGet]
        public async Task<ActionResult<List<ListForSaleModel>>> ListForSale()
        {
            return (await _realtyService.ListForSale());
        }
    }
}
