using desafio.Domain.Entities;
using System;

namespace desafio.Domain.Dtos;

public record struct PedidoDto(Guid Id, int? NumeroDePedido, string CicloDelPedido, long? CodigoDeContratoInterno, EstadoDelPedido EstadoDelPedido,string CuentaCorriente, string Cuando ) { }
