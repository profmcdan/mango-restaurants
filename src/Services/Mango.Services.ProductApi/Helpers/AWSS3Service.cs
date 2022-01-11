using Amazon.S3;
using Amazon.S3.Model;

namespace Mango.Services.ProductApi.Helpers;

public class AWSS3Service : IAWSS3Service
{
    private IAmazonS3 _amazonS3;
    private IConfiguration _configuration;

    public AWSS3Service(IAmazonS3 amazonS3, IConfiguration configuration)
    {
        _amazonS3 = amazonS3;
        _configuration = configuration;
    }

    public async Task<string> UploadFileAsync(IFormFile formFile)
    {
        var now = DateTime.Now.ToFileTimeUtc().ToString();
        var location = $"uploads/{now}-{formFile.FileName}";
        var awsBucketName = _configuration.GetValue<string>("AWS:BucketName");
        await using var stream = formFile.OpenReadStream();
        var putRequest = new PutObjectRequest
        {
            Key = location,
            BucketName = awsBucketName,
            InputStream = stream,
            AutoCloseStream = true,
            ContentType = formFile.ContentType
        };
        var response = await _amazonS3.PutObjectAsync(putRequest);
        return GetUploadedUrl(location);
    }

    private string GetUploadedUrl(string location)
    {
        var awsBucketName = _configuration.GetValue<string>("AWS:BucketName");
        var awsRegion = _configuration.GetValue<string>("AWS:Region");
        var result = $"https://{awsBucketName}.s3.{awsRegion}.amazonaws.com/{location}";
        return result;
    }
}