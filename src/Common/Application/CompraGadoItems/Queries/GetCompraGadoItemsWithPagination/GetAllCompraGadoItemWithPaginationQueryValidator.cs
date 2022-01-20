using FluentValidation;

namespace Application.CompraGadoItems.Queries.GetCompraGadoItemsWithPagination
{
    public class GetAllCompraGadoItemWithPaginationQueryValidator : AbstractValidator<GetAllCompraGadoItemsWithPaginationQuery>
    {
        public GetAllCompraGadoItemWithPaginationQueryValidator()
        {
            RuleFor(x=>x.IdCompraGadoItem)
                .NotNull()
                .NotEmpty().WithMessage("IdCompraGadoItem is required.");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}
