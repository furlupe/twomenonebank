namespace Bank.Credit.App.Dto
{
    public class PageDto<T>
        where T : class
    {
        public List<T> Values { get; set; }
        public int Current { get; set; }
        public int Total { get; set; }
        public int Size { get; set; }
    }
}
