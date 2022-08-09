using MessageBus;
using MessageBus.IntegrationEvent;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiPublicadora.Services
{
    public class ApiConsumidoraService : ServiceBase, IApiConsumidoraService
    {
        private readonly IMessageBus _bus;
        private readonly HttpClient _httpClient;

        public ApiConsumidoraService(HttpClient httpClient, IOptions<ApiServices> settings, IMessageBus bus)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ApiConsumidora);
            _bus = bus;
        }

        public async Task<Conteudo> GetApiConsumidora()
        {
            var response = await _httpClient.GetAsync("/Consumir/obter-resultado");

            TratarErrosResponse(response);
            var result = await DeserializarObjetoResponse<Conteudo>(response);

            var retorno = await _bus.RequestAsync<DescricaoIntegrationEvent, ValidationResultIntegrationEvent>(
                    new DescricaoIntegrationEvent()
                    {
                        Descricao = result.Descicao
                    });

            return new Conteudo { Descicao = retorno.Resultado };
        }

    }

    public interface IApiConsumidoraService
    {
        Task<Conteudo> GetApiConsumidora();
    }
}
