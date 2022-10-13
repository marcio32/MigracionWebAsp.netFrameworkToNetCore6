using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Web.Data.Dtos;
using Web.Data.Entities;

namespace Web.Data.Base
{
    public class BaseApi : ControllerBase
    {

        private readonly IHttpClientFactory _httpClient;
        public BaseApi(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory;
        }

        JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };


        public async Task<IActionResult> LoginToApi(string ControllerMethodUrl, object model, string token)
        {

            try
            {
                var client = _httpClient.CreateClient("useApi");
                if (token != "")
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.PostAsJsonAsync(ControllerMethodUrl, model);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    return Ok(content);


                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "BaseApi");
                return BadRequest("Surgio un problema por favor contacte a sistemas");
            }
        }

        public async Task<IActionResult> GetToApi(string ControllerMethodUrl, string token)
        {

            try
            {

                var client = _httpClient.CreateClient("useApi");
                if (token != "")
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync(ControllerMethodUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    return Ok(content);


                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "BaseApi");
                return BadRequest("Surgio un problema por favor contacte a sistemas");
            }
        }
    }
}
