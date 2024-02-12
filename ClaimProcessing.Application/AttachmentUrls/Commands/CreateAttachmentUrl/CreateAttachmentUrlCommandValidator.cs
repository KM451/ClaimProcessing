using ClaimProcessing.Shared.AttachmentUrls.Commands.CreateAttachmentUrl;
using FluentValidation;

namespace ClaimProcessing.Application.AttachmentUrls.Commands.CreateAttachmentUrl
{
    public class CreateAttachmentUrlCommandValidator : AbstractValidator<CreateAttachmentUrlCommand>
    {
        public CreateAttachmentUrlCommandValidator()
        {
            RuleFor(a => a.ClaimId).NotNull().GreaterThan(0);
            RuleFor(a => a.Path).NotEmpty().MaximumLength(200);
        }
    }
}
