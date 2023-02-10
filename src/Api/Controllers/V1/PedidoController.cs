using Andreani.ARQ.Pipeline.Clases;
using Andreani.ARQ.WebHost.Controllers;

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
public class PedidoController
{
    [HttpGet]
}

