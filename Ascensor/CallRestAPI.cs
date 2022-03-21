using Ascensor.WebAPI.DTO.Entities;
using Ascensor.WebAPI.DTO.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

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
        public List<AscensorEntity> MoveFromInside(int Asce_PisoInicial, int Asce_PisoFinal)
        {
            List<AscensorEntity> list = new List<AscensorEntity>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage httpResponse = client.GetAsync($"api/Ascensor/MoveFromInside/{Asce_PisoInicial}/{Asce_PisoFinal}").Result;

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
        public List<AscensorEntity> MoveFromOutside(int Asce_PisoInicial, int Asce_PisoFinal)
        {
            List<AscensorEntity> list = new List<AscensorEntity>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage httpResponse = client.GetAsync($"api/Ascensor/MoveFromOutside/{Asce_PisoInicial}/{Asce_PisoFinal}").Result;

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
        public List<AscensorEntity> ListOfPendingFloors(int Asce_PisoInicial, int Asce_PisoFinal)
        {
            List<AscensorEntity> list = new List<AscensorEntity>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage httpResponse = client.GetAsync($"api/Ascensor/ListOfPendingFloors/{Asce_PisoInicial}/{Asce_PisoFinal}").Result;

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
        public List<AscensorEntity> GetCurrentFloor()
        {
            List<AscensorEntity> list = new List<AscensorEntity>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage httpResponse = client.GetAsync("api/Ascensor/GetCurrentFloor").Result;

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