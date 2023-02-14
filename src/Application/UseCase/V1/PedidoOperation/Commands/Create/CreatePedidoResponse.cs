using System;

namespace desafio.Application.UseCase.V1.PedidoOperation.Commands.Create
{
    public record struct CreatePedidoResponse(Guid PedidoId, string Message) { }
}

