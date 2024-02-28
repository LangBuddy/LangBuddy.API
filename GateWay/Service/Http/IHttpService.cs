namespace Service.Http
{
    public interface IHttpService
    {
        Task<HttpResponseDefault> Send<TRequest, TResponse>(string endpoint, HttpMethod httpMethod, TRequest body, string? token = null);
        Task<HttpResponseDefault> Send<TRequest>(string endpoint, HttpMethod httpMethod, TRequest body, string? token = null);
        Task<HttpResponseDefault> Send<TResponse>(string endpoint, HttpMethod httpMethod, string? token = null);
        Task<HttpResponseDefault> Send(string endpoint, HttpMethod httpMethod, string? token = null);
    }
}
