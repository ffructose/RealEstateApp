using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Models;
using RealEstateApp.Services.Interfaces;
using System.Text.Json;

namespace RealEstateApp.Services
{
    public class RealtyService : IRealtyService
    {
        private static AutoCompleteModel autoCompleteModel = null;
        private static List<ListForSaleModel> listForSaleModel = null;

        public async Task<ActionResult<AutoCompleteModel>> GetAutoComplete(string input)
        {
            if (input == null) return null;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://realty-in-us.p.rapidapi.com/locations/auto-complete?input={input}"),
                Headers =
                {
                    { "X-RapidAPI-Key", "ca79429615msh4ac5d15a7b7bc99p1abbf7jsn13fe18139712" },
                    { "X-RapidAPI-Host", "realty-in-us.p.rapidapi.com" },
                },
            };
            if (request == null) return null;

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                autoCompleteModel = JsonSerializer.Deserialize<AutoCompleteModel>(body);
                return autoCompleteModel;
            }
        }

        public async Task<ActionResult<List<ListForSaleModel>>> ListForSale()
        {
            if (autoCompleteModel == null) return null;
            listForSaleModel = new List<ListForSaleModel>();
            var client = new HttpClient();
            for (int i = 0; i < /*autoCompleteModel.autocomplete.Count*/1; i++)
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://realty-in-us.p.rapidapi.com/properties/list-for-sale?state_code={autoCompleteModel.autocomplete[i].state_code}&city={autoCompleteModel.autocomplete[i].city}&offset=0&limit=200&sort=relevance"),
                    Headers =
                {
                    { "X-RapidAPI-Key", "ca79429615msh4ac5d15a7b7bc99p1abbf7jsn13fe18139712" },
                    { "X-RapidAPI-Host", "realty-in-us.p.rapidapi.com" },
                },
                };
                if (request == null) return null;

                using (var response = await client.SendAsync(request))//
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<ListForSaleModel>(body);
                    for (int j = 0; j < list.listings.Count; j++)
                    {
                        if (list.listings[j].photo == null)
                        {
                            list.listings.RemoveAt(j);
                            j--;
                            continue;
                        }
                        list.listings[j].city = autoCompleteModel.autocomplete[i].city;
                    }
                    listForSaleModel.Add(list);
                }
            }
            return listForSaleModel;
        }
    }
}
