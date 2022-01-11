namespace Mango.Services.ProductApi.Dtos;

public class ResponseDto
{
    public bool IsSuccess { get; set; } = true;
    public object? Data { get; set; }
    public string? Message { get; set; }
    public List<string>? ErrorMessages { get; set; }
}