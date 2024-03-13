﻿using ClaimProcessing.Shared.FotoUrls.Commands.CreateFotoUrl;
using FluentValidation;

namespace ClaimProcessing.Application.FotoUrls.Commands.CreateFotoUrl
{
    public class CreateFotoUrlCommandValidator : AbstractValidator<CreateFotoUrlCommand>
    {
        public CreateFotoUrlCommandValidator()
        {
            RuleFor(a => a.ClaimId).NotNull().GreaterThan(0);
            RuleFor(a => a.FileName).NotEmpty().MaximumLength(200);
        }
    }
}