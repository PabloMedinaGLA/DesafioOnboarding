using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using desafio.Domain.Dtos;
using desafio.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace desafio.Application.UseCase.V1.PedidoOperation.Queries.GetElement
{
    public class PedidoIdentifier : IRequest<Response<PedidoDto>>
    {
        public PedidoIdentifier(string id) 
        {
            Id = id;
        }
        public string Id { get; set; }
    }

    public class GetElementIdHandler : IRequestHandler<PedidoIdentifier, Response<PedidoDto>>
    {
        private readonly IReadOnlyQuery _query;

        public GetElementIdHandler(IReadOnlyQuery query)
        {
            _query = query;
        }

        public async Task<Response<PedidoDto>> Handle(PedidoIdentifier request, CancellationToken cancellationToken)
        {
            var result = await _query.GetByIdAsync<Pedidos>(nameof(request.Id),request.Id);

            var sqlString = $"select * from dbo.EstadoDelpedido where id = '{result.EstadoDelPedido}'";
            var resultadoEstadoDelPedido = await _query.FirstOrDefaultQueryAsync<EstadoDelPedido>(sqlString);


            PedidoDto pedidodto = new PedidoDto()
            {

                Id = result.Id,
                NumeroDePedido = result.NumeroDePedido,
                CicloDelPedido = result.CicloDelPedido,
                CodigoDeContratoInterno = result.CodigoDeContratoInterno,
                EstadoDelPedido = new EstadoDelPedido()
                {
                    Id = resultadoEstadoDelPedido is null ? 1 : resultadoEstadoDelPedido.Id,
                    Descripcion = resultadoEstadoDelPedido is null ? "Vacio" : resultadoEstadoDelPedido.Descripcion
                },
                CuentaCorriente = result.CuentaCorriente,
                Cuando = result.Cuando.ToString("MM/dd/yyyy")



            };

            return new Response<PedidoDto>
            {
                Content = pedidodto,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
