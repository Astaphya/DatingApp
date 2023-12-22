using DatingApp.Shared.Interfaces.Responses;

namespace DatingApp.Shared.Models.Responses
{

    /// <inheritdoc cref="IAppRequest{T}"/>
    public class AppResponse<T> : IAppResponse<T>
    {
        /// <summary>
        /// Boolean flag if the API request have a positive/negative response
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Errors list (using keys for translation)
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Result is not null if <see cref="IsSuccess"/> is true
        /// </summary>
        public T? Result { get; set; }
        object IAppResponse.Result => Result;

        public IAppResponse<T> AddError(string error)
        {
            Errors.Add(error);
            IsSuccess = false;
            return this;
        }
        public IAppResponse<T> AddError(string error, T? result)
        {
            Errors.Add(error);
            IsSuccess = false;
            Result = result;
            return this;
        }
        public IAppResponse<T> AddErrors(List<string> errors)
        {
            Errors.AddRange(errors);
            IsSuccess = false;
            return this;
        }

        public AppResponse()
        {
        }

        public IAppResponse<T> AddResult(T result)
        {
            IsSuccess = true;
            Result = result;
            return this;
        }


    }
}
