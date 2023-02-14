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
            var result = await _query.GetAllAsync<Pedidos>(nameof(Pedidos));
            List<PedidoDto> resultPedidos = new List<PedidoDto>();

            foreach (var item in result)
            {
                var sqlString = $"select * from dbo.EstadoDelpedido where id = '{item.EstadoDelPedido}'";
                var resultadoEstadoDelPedido = await _query.FirstOrDefaultQueryAsync<EstadoDelPedido>(sqlString);

                
                PedidoDto pedidodto = new PedidoDto()
                {

                    Id= item.Id,
                    NumeroDePedido = item.NumeroDePedido,
                    CicloDelPedido = item.CicloDelPedido,
                    CodigoDeContratoInterno = item.CodigoDeContratoInterno,
                    EstadoDelPedido = new EstadoDelPedido() { Id = resultadoEstadoDelPedido is null? 1 : resultadoEstadoDelPedido.Id , 
                                                              Descripcion = resultadoEstadoDelPedido is null? "Vacio" : resultadoEstadoDelPedido.Descripcion}


                };
                resultPedidos.Add(pedidodto);
            }

            return new Response<List<PedidoDto>>
            {
                Content = resultPedidos,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
