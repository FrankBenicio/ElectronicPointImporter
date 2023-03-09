using Domain.Requests;
using FluentValidation;

namespace Domain.Validators
{
    public class ArchiveIdRequestValidator : AbstractValidator<ArchiveIdRequest>
    {
        public ArchiveIdRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("ArchiveId not found");
        }
    }
}
