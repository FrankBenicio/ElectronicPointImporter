using Domain.Requests;
using FluentValidation;

namespace Domain.Validators
{
    public class ArchiveUploadRequestValidator : AbstractValidator<List<ArchiveUploadRequest>>
    {
        public ArchiveUploadRequestValidator()
        {
            RuleForEach(x => x)
                .ChildRules(item =>
                {
                    item.RuleFor(f => f.FileName)
                        .Must(f => !string.IsNullOrEmpty(f))
                        .WithMessage("FileName not found");

                    item.RuleFor(x => x.File)
                        .Must(f => f is not null)
                        .WithMessage("File not found");
                });
        }
    }
}
