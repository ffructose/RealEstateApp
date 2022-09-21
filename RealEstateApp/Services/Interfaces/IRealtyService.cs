using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Models;

namespace RealEstateApp.Services.Interfaces
{
    public interface IRealtyService
    {
        Task<AutoCompleteModel> GetAutoComplete(string input);
        Task<List<ListForSaleModel>> ListForSale();
    }
}
