using RestExercise8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RESTConsumer
{
    public class FlowerWorker
    {
        private readonly string URL = "http://localhost:5292/api/Flowers";

        public void DoWork()
        {
            Console.WriteLine("Unfiltered flowers");
            IEnumerable<Flower> flowers = GetAll(null, null).Result;
            foreach (Flower flower in flowers)
            {
                Console.WriteLine(flower.Species);
            }

            flowers = GetAll("ro", null).Result;
            Console.WriteLine("Filtered flowers");
            foreach (Flower flower in flowers)
            {
                Console.WriteLine(flower.Species);
            }
        }

        public async Task<IEnumerable<Flower>> GetAll(string? speciesFilter, string? colorFilter)
        {
            using (HttpClient client = new HttpClient())
            {
                //use this if the REST server expects an Amount header
                //client.DefaultRequestHeaders.Add("amount", "50");
                HttpResponseMessage response = await client.GetAsync(URL + CreateQueryParameters(speciesFilter, colorFilter));
                Console.WriteLine("Status code: " + response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<Flower> flowers = await response.Content.
                        ReadFromJsonAsync<IEnumerable<Flower>>();
                    return flowers;
                }
                else
                {
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    return new List<Flower>();
                }
            }
        }

        private string CreateQueryParameters(string? speciesFilter, string? colorFilter)
        {
            StringBuilder sb = new StringBuilder();
            if (speciesFilter != null)
            {
                sb.Append("?speciesFilter=" + speciesFilter);
            }
            if (colorFilter != null)
            {
                if (speciesFilter != null)
                {
                    sb.Append("&");
                }
                else
                {
                    sb.Append("?");
                }
                sb.Append("colorFilter=" + colorFilter);
            }
            return sb.ToString();
        }

        public async Task<Flower> GetById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                //use this if the REST server expects an Amount header
                //client.DefaultRequestHeaders.Add("amount", "50");
                HttpResponseMessage response = await client.GetAsync(URL + "/" + id);
                Console.WriteLine("Status code: " + response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    Flower flower = await response.Content.ReadFromJsonAsync<Flower>();
                    return flower;
                }
                else
                {
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    return null;
                }
            }
        }

        public async Task<Flower> Post(Flower newFlower)
        {
            using (HttpClient client = new HttpClient())
            {
                //use this if the REST server expects an Amount header
                //client.DefaultRequestHeaders.Add("amount", "50");
                JsonContent jsonData = JsonContent.Create(newFlower);
                HttpResponseMessage response = await client.PostAsync(URL,jsonData);
                Console.WriteLine("Status code: " + response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    Flower flower = await response.Content.ReadFromJsonAsync<Flower>();
                    return flower;
                }
                else
                {
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    return null;
                }
            }
        }

        public async Task<Flower> Put(int id, Flower updates)
        {
            using (HttpClient client = new HttpClient())
            {
                //use this if the REST server expects an Amount header
                //client.DefaultRequestHeaders.Add("amount", "50");
                JsonContent jsonData = JsonContent.Create(updates);
                HttpResponseMessage response = await client.PutAsync(URL + "/" + id, jsonData);
                Console.WriteLine("Status code: " + response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    Flower flower = await response.Content.ReadFromJsonAsync<Flower>();
                    return flower;
                }
                else
                {
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    return null;
                }
            }
        }

        public async Task<Flower> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                //use this if the REST server expects an Amount header
                //client.DefaultRequestHeaders.Add("amount", "50");
                HttpResponseMessage response = await client.DeleteAsync(URL + "/" + id);
                Console.WriteLine("Status code: " + response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    Flower flower = await response.Content.ReadFromJsonAsync<Flower>();
                    return flower;
                }
                else
                {
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    return null;
                }
            }
        }
    }
}
