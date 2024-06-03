namespace HistoricalNewsUpdate.Common.Models
{
    public class ApiResult<T>
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
