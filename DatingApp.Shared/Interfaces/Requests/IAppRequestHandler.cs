using DatingApp.Shared.Interfaces.Responses;
using MediatR;

namespace DatingApp.Shared.Interfaces.Requests;

/// <summary>
/// Global Request handler for all queries/commands for the application
/// to wrap all responses in IAppResponse
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IAppRequestHandler<TRequest, TResponse> :
    IRequestHandler<TRequest, IAppResponse<TResponse>> where TRequest : IRequest<IAppResponse<TResponse>>
{
}