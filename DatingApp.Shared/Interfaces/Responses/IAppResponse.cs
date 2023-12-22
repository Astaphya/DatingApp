namespace DatingApp.Shared.Interfaces.Responses;

/// <summary>
/// Global reference for all responses of the application
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAppResponse<T> : IAppResponse
{
    public bool IsSuccess { get; set; }

    public List<string> Errors { get; set; }
    public IAppResponse<T> AddResult(T result);
    public IAppResponse<T> AddError(string error);
    public IAppResponse<T> AddErrors(List<string> errors);
    new T Result { get; }
}

public interface IAppResponse
{
    public bool IsSuccess { get; set; }
    public List<string> Errors { get; set; }
    public object Result { get; }
}