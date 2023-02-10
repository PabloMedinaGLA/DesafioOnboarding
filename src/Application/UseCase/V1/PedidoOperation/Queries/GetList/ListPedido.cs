using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using desafio.Domain.Dtos;
using desafio.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace desafio.Application.UseCase.V1.PedidoOperation.Queries.GetList
{
    public record struct ListPedido : IRequest<Response<List<PedidoDto>>>
    {
    }

    public class ListPedidoHandler : IRequestHandler<ListPedido, Response<List<PedidoDto>>>
    {
        private readonly IReadOnlyQuery _query;

        public ListPedidoHandler(IReadOnlyQuery query)
        {
            _query = query;
        }

        public async Task<Response<List<PedidoDto>>> Handle(ListPedido request, CancellationToken cancellationToken)
        {
            var result = await _query.GetAllAsync<PedidoDto>(nameof(Pedido));

            return new Response<List<PedidoDto>>
            {
                Content = result.ToList(),
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
