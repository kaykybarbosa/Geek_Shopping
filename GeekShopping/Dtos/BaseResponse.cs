namespace GeekShopping.Dtos
{
    public class BaseResponse
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
    }
}
