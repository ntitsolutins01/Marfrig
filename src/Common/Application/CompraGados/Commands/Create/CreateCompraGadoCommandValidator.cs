using FluentValidation;

namespace Application.CompraGados.Commands.Create
{
    public class CreateCompraGadoCommandValidator : AbstractValidator<CreateCompraGadoCommand>
    {
        public CreateCompraGadoCommandValidator()
        {
            RuleFor(v => v.Nome)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .NotEmpty().WithMessage("Name is required.");
        }
    }
}
