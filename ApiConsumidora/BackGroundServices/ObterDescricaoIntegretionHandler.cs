using MessageBus;
using MessageBus.IntegrationEvent;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiConsumidora.BackGroundServices
{
    public class ObterDescricaoIntegretionHandler : BackgroundService
    {
        private readonly IMessageBus _bus;

        public ObterDescricaoIntegretionHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
           return  Task.CompletedTask;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<DescricaoIntegrationEvent, ValidationResultIntegrationEvent>(async request => await Teste(request));
            _bus.AdvancedBus.Connected += OnConnect;
        }
        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ValidationResultIntegrationEvent> Teste(DescricaoIntegrationEvent request)
        {
            return new ValidationResultIntegrationEvent()
            {
                Resultado = "Sera que foi ?"
            };
        }
    }
}
