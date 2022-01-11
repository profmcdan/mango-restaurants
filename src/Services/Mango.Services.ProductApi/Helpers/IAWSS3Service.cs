namespace Mango.Services.ProductApi.Helpers;

public interface IAWSS3Service
{
    Task<string> UploadFileAsync(IFormFile formFile);
}