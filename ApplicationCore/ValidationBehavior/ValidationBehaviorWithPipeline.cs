using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ValidationBehavior
{
    //public class ValidationBehaviorWithPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    //{
       
    //    private readonly IEnumerable<IValidator<TResponse>> _validators;
    //    public ValidationBehaviorWithPipeline(IEnumerable<IValidator<TResponse>> validators)
    //    {
    //        _validators = validators;
    //    }
    //    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    //    {
    //        //pre-precssing

    //        if (_validators.Any()) 
    //        {
    //          var validationContext =  new  ValidationContext<TRequest>(request);
    //          var result = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(validationContext, cancellationToken)));
    //          var failers = result.SelectMany(r => r.Errors).Where(f => f != null).ToList();

    //            if (failers.Count > 0) 
    //            {
    //                throw new ValidationException(failers);
    //            }
    //        }

    //        //next
    //        var response = await next();


    //        //post-processing
    //        return response;

    //    }
    //}
}
