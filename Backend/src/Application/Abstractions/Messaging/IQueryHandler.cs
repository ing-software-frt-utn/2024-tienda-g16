using ErrorOr;
using MediatR;

namespace Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, ErrorOr<TResponse>> 
    where TQuery : IQuery<TResponse>
{
    
}
