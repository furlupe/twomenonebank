namespace Bank.Monitoring.App.Dto
{
    public class LogDto
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string RenderedMessage { get; set; }
        public object Properties { get; set; }
    }
}
