namespace prolabcheck.domain.DTOs
{
    public class Response<T>
    {
        public bool success { get; set; } = true;
        public string message { get; set; } = "";
        public IList<T> result { get; set; }
    }
}
