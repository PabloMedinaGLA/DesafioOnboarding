using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio.Application.UseCase.V1.PedidoOperation.Commands.Create
{
    public class CreatePedidoValidation : AbstractValidator<CreatePedidoCommand>
    {
        public CreatePedidoValidation()
        {
            RuleFor(x => x.CodigoDeContratoInterno)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Valor Ingresado Invalido");

            RuleFor(x => x.CuentaCorriente)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Valor Ingresado Invalido");
        }
    }
}
