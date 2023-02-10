using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using desafio.Application.UseCase.V1.PersonOperation.Commands.Create;
using desafio.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace desafio.Application.UseCase.V1.PedidoOperation.Commands.Create
{
    public class CreatePedidoCommand : IRequest<Response<CreatePedidoResponse>>
    {
        public int Id { get; set; }
        public int NumeroPedido { get; set; }
        public string CicloDelPedido { get; set; }
        public Int64 CodigoDeContratoInterno { get; set; }
        public DateTime Fecha { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
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
            var entity = new Pedido
            {
                Id = request.Id,
                NumeroPedido = request.NumeroPedido,
                CicloDelPedido = request.CicloDelPedido,
                CodigoDeContratoInterno = request.CodigoDeContratoInterno,
                Fecha = request.Fecha,
                EstadoPedido = request.EstadoPedido
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
