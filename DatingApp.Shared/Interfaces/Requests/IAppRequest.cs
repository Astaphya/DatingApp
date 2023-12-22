using DatingApp.Shared.Interfaces.Responses;
using MediatR;

namespace DatingApp.Shared.Interfaces.Requests;

public interface IAppRequest<T> : IRequest<IAppResponse<T>>
{
}