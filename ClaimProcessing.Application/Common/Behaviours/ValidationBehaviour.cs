﻿using FluentValidation;
using MediatR;

namespace ClaimProcessing.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var failures = _validators.Select(v => v.Validate(context)).SelectMany(result => result.Errors).Where(f => f != null).ToList();
                if (failures.Count != 0)
                {
                    throw new Exception();
                }
            }
            return await next();
        }
    }
}
