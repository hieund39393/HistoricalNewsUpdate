namespace HistoricalNewsUpdate.Models.User
{
    public class CheckCodeRequest
    {
        public string Code { get; set; }
        public int UserId { get; set; }
    }
}
