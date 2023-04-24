using Andreani.ARQ.Pipeline.Clases;
using Andreani.ARQ.WebHost.Controllers;
using desafio.Application.UseCase.V1.PedidoOperation.Commands.Create;
using desafio.Application.UseCase.V1.PedidoOperation.Queries.GetList;
using desafio.Application.UseCase.V1.PedidoOperation.Queries.GetElement;

using desafio.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace desafio.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class pedidoController : ApiControllerBase
{
    /// <summary>
    /// Creación de Nuevo Pedido
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreatePedidoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Create(CreatePedidoCommand body) => Result(await Mediator.Send(body));


    /// <summary>
    /// Listado de Pedidos
    /// </summary>
    /// <remarks>en los remarks podemos documentar información más detallada</remarks>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<PedidoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get() => Result(await Mediator.Send(new ListPedido()));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(List<PedidoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id) => Result(await Mediator.Send(new PedidoIdentifier(id)));
}

