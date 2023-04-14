using System;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio.Application.UseCase.V1.PedidoOperation.Queries.GetElement
{
    public class PedidoIdentifierValidation : AbstractValidator<PedidoIdentifier>
    {
        public PedidoIdentifierValidation() 
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("El Id no puede estar Vacio")
                .Length(36)
                .WithMessage("Guid tiene que tener 36 Digitos")
                .Matches("[0-9a-fA-F]{8}-[0)a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{9}")
                .WithMessage("Id Sin formato guid.... example --> (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxx)"); 
            ;
        }
    }
}
