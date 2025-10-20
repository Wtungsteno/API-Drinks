using System.Net;

namespace B_BusinessLogicWebAPILoGiud.Exceptions
{
    public class ExternalApiError : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public string Title { get; }

        public object[] Args { get; }

        public ExternalApiError(HttpStatusCode statusCode, string title, string message, object[] args)
            : base(message)
        {
            StatusCode = statusCode;
            Title = title;
            Args = args;
        }
    }
}
