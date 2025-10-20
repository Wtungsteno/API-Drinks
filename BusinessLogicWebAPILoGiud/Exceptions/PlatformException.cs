using System.Net;

namespace ITS.Day2.BL.Exceptions
{
    public class PlatformException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public string Title { get; }

        public object[] Args { get; }

        public PlatformException(HttpStatusCode statusCode, string title, string message, object[] args)
            : base(message)
        {
            StatusCode = statusCode;
            Title = title;
            Args = args;
        }
    }
}
