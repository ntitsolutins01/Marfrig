using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Animais.Commands.Create
{
    public class CreateAnimalCommandValidator : AbstractValidator<CreateAnimalCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateAnimalCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .MustAsync(BeUniqueName).WithMessage("The specified city already exists.")
                .NotEmpty().WithMessage("Name is required.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            //TODO: Control by uppercase and CultureInfo
            return await _context.Animais.AllAsync(x => x.Descricao != name, cancellationToken);
        }
    }
}
