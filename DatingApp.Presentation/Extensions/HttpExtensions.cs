using System.Text.Json;
using DatingApp.Application.Common.Helpers;

namespace DatingApp.Presentation.Extensions
{
    public static class HttpExtensions
    {
        //this method is used to add pagination header to the response object
        public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header)
        {
            var jsonOptions = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            response.Headers.Add("Pagination", JsonSerializer.Serialize(header, jsonOptions));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}