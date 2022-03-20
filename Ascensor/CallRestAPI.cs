using Ascensor.WebAPI.DTO.Entities;
using Ascensor.WebAPI.DTO.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ascensor
{
    public class CallRestAPI
    {
        public static string urlAPI = "https://localhost:44317/";

        public List<AscensorEntity> GetAll()
        {
            List<AscensorEntity> list = new List<AscensorEntity>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage httpResponse = client.GetAsync("api/Ascensor/GetAll").Result;

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var json = httpResponse.Content.ReadAsStringAsync().Result;
                        var response = JsonConvert.DeserializeObject<Response<List<AscensorEntity>>>(json);
                        if (response.Status)
                        {
                            list = response.Value;
                        }
                    }
                    else
                    {
                        return list;
                    }
                }
            }
            catch (Exception)
            {
                return list;
            }

            return list;
        }
    }
}