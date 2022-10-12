using RestExercise8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RESTConsumer
{
    public class IPAWorker
    {
        private readonly string URL = "http://localhost:5292/api/IPAs";

        public void DoWork()
        {
            Console.WriteLine("Unfiltered ipas");
            IEnumerable<IPA> ipas = GetAll(null, null, null).Result;
            foreach (IPA ipa in ipas)
            {
                Console.WriteLine(ipa.Name);
            }

            ipas = GetAll(5, null, null).Result;
            Console.WriteLine("Filtered ipas");
            foreach (IPA ipa in ipas)
            {
                Console.WriteLine(ipa.Name);
            }
        }

        public async Task<IEnumerable<IPA>> GetAll(double? minimumProof, double? maximumProof, string? nameFilter)
        {
            using (HttpClient client = new HttpClient())
            {
                //use this if the REST server expects an Amount header
                //client.DefaultRequestHeaders.Add("amount", "50");
                HttpResponseMessage response = await client.GetAsync(URL + CreateQueryParameters(minimumProof, maximumProof, nameFilter));
                Console.WriteLine("Status code: " + response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<IPA> ipas = await response.Content.
                        ReadFromJsonAsync<IEnumerable<IPA>>();
                    return ipas;
                }
                else
                {
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    return new List<IPA>();
                }
            }
        }

        private string CreateQueryParameters(double? minimumProof, double? maximumProof, string? nameFilter)
        {
            StringBuilder sb = new StringBuilder();
            if (minimumProof != null)
            {
                sb.Append("?minimumProof=" + minimumProof);
            }
            if (maximumProof != null)
            {
                if (maximumProof != null)
                {
                    sb.Append("&");
                }
                else
                {
                    sb.Append("?");
                }
                sb.Append("maximumProof=" + maximumProof);
            }
            if (minimumProof != null || maximumProof != null)
            {
                if (nameFilter != null)
                {
                    sb.Append("&");
                }
                else
                {
                    sb.Append("?");
                }
                sb.Append("nameFilter=" + nameFilter);
            }
            return sb.ToString();
        }

        public async Task<IPA> GetById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                //use this if the REST server expects an Amount header
                //client.DefaultRequestHeaders.Add("amount", "50");
                HttpResponseMessage response = await client.GetAsync(URL + "/" + id);
                Console.WriteLine("Status code: " + response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    IPA ipa = await response.Content.ReadFromJsonAsync<IPA>();
                    return ipa;
                }
                else
                {
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    return null;
                }
            }
        }

        public async Task<IPA> Post(IPA newIPA)
        {
            using (HttpClient client = new HttpClient())
            {
                //use this if the REST server expects an Amount header
                //client.DefaultRequestHeaders.Add("amount", "50");
                JsonContent jsonData = JsonContent.Create(newIPA);
                HttpResponseMessage response = await client.PostAsync(URL, jsonData);
                Console.WriteLine("Status code: " + response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    IPA ipa = await response.Content.ReadFromJsonAsync<IPA>();
                    return ipa;
                }
                else
                {
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    return null;
                }
            }
        }

        public async Task<IPA> Put(int id, IPA updates)
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
                    IPA ipa = await response.Content.ReadFromJsonAsync<IPA>();
                    return ipa;
                }
                else
                {
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    return null;
                }
            }
        }

        public async Task<IPA> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                //use this if the REST server expects an Amount header
                //client.DefaultRequestHeaders.Add("amount", "50");
                HttpResponseMessage response = await client.DeleteAsync(URL + "/" + id);
                Console.WriteLine("Status code: " + response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    IPA ipa = await response.Content.ReadFromJsonAsync<IPA>();
                    return ipa;
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
