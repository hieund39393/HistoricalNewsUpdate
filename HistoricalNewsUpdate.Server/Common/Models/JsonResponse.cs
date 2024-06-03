using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace HistoricalNewsUpdate.Common.Models
{
    public class JsonResponse
    {
        [JsonProperty(PropertyName = "statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

    }

    public class JsonResponse<T>
    {
        [JsonProperty(PropertyName = "statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }



        /** Full optional constructor */
        public JsonResponse(int statusCode = 200, string message = "", int? pageIndex = null, T data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public static JsonResponse<T> Ok(T data)
        {
            return new JsonResponse<T>(StatusCodes.Status200OK, data: data);
        }
    }
}
