using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Models;

namespace RealEstateApp.Services.Interfaces
{
    public interface IRealtyService
    {
        Task<ActionResult<AutoCompleteModel>> GetAutoComplete(string input);
        Task<List<ListForSaleModel>> ListForSale();
    }
}
