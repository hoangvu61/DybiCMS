namespace Web.Models
{
    public partial class DataResponse
    {
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
