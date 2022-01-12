using System.Text;
using Mango.Web.Models;
using Mango.Web.Services.Contracts;
using Newtonsoft.Json;

namespace Mango.Web.Services.Implementations;

public class BaseService : IBaseService
{
    public ResponseDto ResponseModel { get ; set; }
    private IHttpClientFactory HttpClient { get; set; }
    
    public BaseService(IHttpClientFactory httpClient)
    {
        this.ResponseModel = new ResponseDto();
        this.HttpClient = httpClient;
    }

    public async Task<T> SendAsync<T>(ApiRequest apiRequest)
    {
        try
        {
            var client = HttpClient.CreateClient("MangoAPI");
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(apiRequest.Url);
            
            client.DefaultRequestHeaders.Clear();
            if (apiRequest.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8,
                    "application/json");
            }

            HttpResponseMessage apiResponse = null;
            switch (apiRequest.ApiType)
            {
                case Sd.ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case Sd.ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case Sd.ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            apiResponse = await client.SendAsync(message);
            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
            return apiResponseDto;
        }
        catch (Exception ex)
        {
            var dto = new ResponseDto
            {
                Message = "Error",
                ErrorMessages = new List<string> {Convert.ToString(ex.Message)},
                IsSuccess = false
            };

            var res = JsonConvert.SerializeObject(dto);
            var responseDto = JsonConvert.DeserializeObject<T>(res);
            return responseDto;
        }
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }
}