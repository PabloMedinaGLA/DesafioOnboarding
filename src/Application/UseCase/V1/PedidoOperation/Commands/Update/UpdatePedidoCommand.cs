using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using Andreani.Scheme.Onboarding;
using desafio.Domain.Common;
using desafio.Domain.Entities;
using desafio.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace desafio.Application.UseCase.V1.PedidoOperation.Commands.Update;

public class UpdatePedidoCommand : IRequest<Response<string>>
{
    public Pedido Pedido { get; set; }

    public UpdatePedidoCommand(Pedido pedido)
    {
        Pedido = pedido;
    }

}
public class UpdatePedidoHandler : IRequestHandler<UpdatePedidoCommand, Response<string>>
{
    private readonly ITransactionalRepository _repository;
    private readonly IReadOnlyQuery _query;
    private readonly ILogger<UpdatePedidoHandler> _logger;

    public UpdatePedidoHandler(ITransactionalRepository repository, IReadOnlyQuery query, ILogger<UpdatePedidoHandler> logger)
    {
        _repository = repository;
        _query = query;
        _logger = logger;
    }

    public async Task<Response<string>> Handle(UpdatePedidoCommand request, CancellationToken cancellationToken)
    {
        var pedido = await _query.GetByIdAsync<Pedidos>(nameof(request.Pedido.id), request.Pedido.id);
        var response = new Response<string>();
        if (pedido is null)
        {
            response.AddNotification("#3123", nameof(request.Pedido.id), string.Format(ErrorMessage.NOT_FOUND_RECORD, "Pedido", request.Pedido.id));
            response.StatusCode = System.Net.HttpStatusCode.NotFound;
            return response;
        }
        pedido.NumeroDePedido = request.Pedido.numeroDePedido;
        pedido.EstadoDelPedido = (int)EstadoPedidoEnum.ASIGNADO;

        _repository.Update(pedido);
        await _repository.SaveChangeAsync();

        return response;
    }
}


