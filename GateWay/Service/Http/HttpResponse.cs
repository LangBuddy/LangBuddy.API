using System.Net;

namespace Service.Http
{
    public class HttpResponseDefault
    {
        public bool Status { get; set; }
        public HttpStatusCode Code { get; set; }   
    }

    public class HttpResponse<TBody> : HttpResponseDefault
    {
        public TBody? Result { get; set; }
    }
}
