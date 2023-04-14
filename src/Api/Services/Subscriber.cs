using Andreani.ARQ.AMQStreams.Interface;
using Andreani.Scheme.Onboarding;
using desafio.Application.UseCase.V1.PedidoOperation.Commands.Update;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace desafio.Services
{
    public class Subscriber : ISubscriber
    {
        private ILogger<Subscriber> _logger;
        private Andreani.ARQ.AMQStreams.Interface.IPublisher _publisher;
        private readonly ISender _mediator;

        public Subscriber(ILogger<Subscriber> logger, Andreani.ARQ.AMQStreams.Interface.IPublisher publisher, ISender mediator)
        {
            _logger = logger;
            _publisher = publisher;
            _mediator = mediator;
        }
        public async Task ReciveCustomEvent(Pedido @event)
        {
            await _mediator.Send(@event);
        }
    }
}
