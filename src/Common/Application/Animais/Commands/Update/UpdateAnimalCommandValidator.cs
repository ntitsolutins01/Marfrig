using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Animais.Commands.Update;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Animais.Commands.Update
{
    public class UpdateAnimalCommandValidator : AbstractValidator<UpdateAnimalCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateAnimalCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .MustAsync(BeUniqueName).WithMessage("The specified city already exists. If you just want to activate the city leave the name field blank!");

            RuleFor(v => v.Id).NotNull();
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            //TODO: Control by uppercase and CultureInfo
            return await _context.Animais.AllAsync(x => x.Descricao != name, cancellationToken);
        }
    }
}
