using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Models;
using RealEstateApp.Services.Interfaces;

namespace RealEstateApp.Controllers
{
    public class RealtyController : Controller
    {
        private readonly IRealtyService _realtyService;
        public RealtyController(IRealtyService realtyService)
        {
            _realtyService = realtyService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var temp = await _realtyService.ListForSale();
            return View(await _realtyService.ListForSale());
        }

        [HttpPost]
        public async Task<ActionResult> Index(string? input)
        {
            await _realtyService.GetAutoComplete(input);
            return RedirectToAction("Index");
        }

        [HttpGet("[action]/{input}")]
        public async Task<ActionResult<AutoCompleteModel>> GetAutoComplete(string input)
        {
            return await _realtyService.GetAutoComplete(input);
        }

        [HttpGet]
        public async Task<ActionResult<List<ListForSaleModel>>> ListForSale()
        {
            return await _realtyService.ListForSale();
        }
    }
}
