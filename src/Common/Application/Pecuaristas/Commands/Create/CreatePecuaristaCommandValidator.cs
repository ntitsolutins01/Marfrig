using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Pecuaristas.Commands.Create
{
    public class CreatePecuaristaCommandValidator : AbstractValidator<CreatePecuaristaCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreatePecuaristaCommandValidator(IApplicationDbContext context)
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
            return await _context.Pecuaristas.AllAsync(x => x.Nome != name, cancellationToken);
        }
    }
}
