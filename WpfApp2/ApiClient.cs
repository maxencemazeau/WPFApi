using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpfApp2
{

    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8080")
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Membre>> GetMembresAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("/membre");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Membre>>(json);
            }
            else
            {
                throw new Exception($"Error fetching data from API: {response.ReasonPhrase}");
            }
        }

        public async Task<bool> DeleteMembreAsync(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"/supprimerMembre/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Error deleting member with id {id}: {response.ReasonPhrase}");
            }
        }
    }
}
