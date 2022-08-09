using ApiPublicadora.Redis;
using ApiPublicadora.Services;
using MessageBus;
using MessageBus.IntegrationEvent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPublicadora.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRedisCacheService _redisService;
        private readonly ILogger<Conteudo> _logger;
        private readonly IApiConsumidoraService _apiConsumidoraService;

        private readonly IMessageBus _bus;

        public WeatherForecastController(ILogger<Conteudo> logger,
            IApiConsumidoraService apiConsumidoraService,
            IMessageBus bus,
            IRedisCacheService redisService)
        {
            _logger = logger;
            _apiConsumidoraService = apiConsumidoraService;
            _bus = bus;
            _redisService = redisService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            for (int i = 0; i < 4; i++)
            {
                await _redisService.SavarCacheAsync($"SEB-123{i}",
              JsonConvert.SerializeObject(new DebitoConta() { Valor = i }),
              TimeSpan.FromSeconds(180));
            }
           

            var stringValue = await _redisService.BuscarCache<DebitoConta>("SEB-1230");
            var foundKeys = await _redisService.ScanKeysAsync("SEB-123*", "4");
            var teste = await _redisService.BuscarCache<DebitoConta>(foundKeys);
            //var result = await _apiConsumidoraService.GetApiConsumidora();


            return Ok(foundKeys);
        }
    }
}
