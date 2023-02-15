using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using desafio.Application.UseCase.V1.PedidoOperation.Commands.Create;
using desafio.Domain.Entities;
using desafio.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace desafio.Application.UseCase.V1.PedidoOperation.Commands.Create
{
    public class CreatePedidoCommand : IRequest<Response<CreatePedidoResponse>>
    {
        
        public long CodigoDeContratoInterno { get; set; }
        public string CuentaCorriente { get; set; }
        
    }

    public class CreatePedidoCommandHandler : IRequestHandler<CreatePedidoCommand, Response<CreatePedidoResponse>>
    {
        private readonly ITransactionalRepository _repository;
        private readonly ILogger<CreatePedidoCommandHandler> _logger;

        public CreatePedidoCommandHandler(ITransactionalRepository repository, ILogger<CreatePedidoCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<CreatePedidoResponse>> Handle(CreatePedidoCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();

            var entity = new Pedidos
            {
                Id = id,
                NumeroDePedido = null,
                CicloDelPedido = id.ToString(),
                CodigoDeContratoInterno = request.CodigoDeContratoInterno,
                EstadoDelPedido = (int?)EstadoPedidoEnum.CREADO,
                CuentaCorriente = request.CuentaCorriente,
                Cuando = DateTime.Now
        };
            _repository.Insert(entity);
            await _repository.SaveChangeAsync();
            _logger.LogDebug("El pedido se cargo correctamente");

            return new Response<CreatePedidoResponse>
            {
                Content = new CreatePedidoResponse
                {
                    Message = "Success",
                    PedidoId = entity.Id
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }


    }

}
