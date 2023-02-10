using desafio.Domain.Entities;
using System;

namespace desafio.Domain.Dtos;

public record struct PedidoDto(int Id, int NumeroPedido, string CicloDelPedido, Int64 CodigoDeContratoInterno, DateTime Cuando, EstadoPedido EstadoPedido) { }
