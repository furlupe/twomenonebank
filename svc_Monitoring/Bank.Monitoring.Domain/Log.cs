namespace Bank.Monitoring.Domain
{
    public class Log
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TraceId { get; set; }
        public string Message { get; set; }
    }
}
