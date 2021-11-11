using System.Net.Http;
using System.Threading.Tasks;
using PlatformService.Dtos;
using System.Text.Json;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;

namespace PlatformService.SyncDataServices.Http 
{

    public class HttpCommandDataClient : ICommandDataClient
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient http, IConfiguration config)
        {
            _httpClient = http;
            _configuration = config;
        }

        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(plat),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);
            if( response.IsSuccessStatusCode )
            {
                Console.WriteLine("--> Sync POST to CommandsService was OK!");
            } 
            else 
            {
                Console.WriteLine("--> Sync POST to CommandsService was NOT OK!");
            }
        }
    }


}