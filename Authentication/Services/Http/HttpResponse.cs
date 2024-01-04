namespace Services.Http
{
    public class HttpResponseDefault
    {
        public bool Status { get; set; }
    }

    public class HttpResponse<TBody>: HttpResponseDefault
    {
        public TBody? Result { get; set; }
    }
}
