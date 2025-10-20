using B_BusinessLogicWebAPILoGiud.Exceptions;
using ITS.Day2.BL.Exceptions;
using System.Net;
using System.Text.RegularExpressions;

namespace ITS.Day2.BL
{
    internal class SolutionLogging
    {
        public static readonly PlatformExceptionTemplate NotFound = new PlatformExceptionTemplate(HttpStatusCode.NotFound, "Entity not found", "{0} {1} not found");
        public static readonly ExternalApiErrorTemplate ServerError = new ExternalApiErrorTemplate(HttpStatusCode.NotFound, "Entity not found", "{0} {1} not found");
    }

    internal class  PlatformExceptionTemplate 
    {
        private static readonly Regex PlaceholderRegex = new(@"\{(\d+)\}", RegexOptions.Compiled);

        private readonly int expectedArgsCount;

        public HttpStatusCode StatusCode { get; }

        public string Title { get; }

        public string Template { get; }

        public PlatformExceptionTemplate(HttpStatusCode statusCode, string title, string template)
        {
            StatusCode = statusCode;
            Title = title;
            Template = template;

            expectedArgsCount = PlaceholderRegex
                .Matches(template)
                .Select(m => int.Parse(m.Groups[1].Value))
                .DefaultIfEmpty(-1)
                .Max() + 1;
        }

        public PlatformException ToException(params object[] args)
        {
            object[] usedArgs = Array.Empty<object>();

            if (expectedArgsCount > 0)
            {
                usedArgs = new object[expectedArgsCount];
                for (int i = 0; i < expectedArgsCount; i++)
                {
                    usedArgs[i] = i < args.Length ? args[i] : "";
                }
            }

            string message;
            try
            {
                message = string.Format(Template, usedArgs);
            }
            catch
            {
                message = Template;
            }

            return new PlatformException(StatusCode, Title, message, usedArgs);
        }
    }


    internal class ExternalApiErrorTemplate
    {
        private static readonly Regex PlaceholderRegex = new(@"\{(\d+)\}", RegexOptions.Compiled);

        private readonly int expectedArgsCount;

        public HttpStatusCode StatusCode { get; }

        public string Title { get; }

        public string Template { get; }

        public ExternalApiErrorTemplate(HttpStatusCode statusCode, string title, string template)
        {
            StatusCode = statusCode;
            Title = title;
            Template = template;

            expectedArgsCount = PlaceholderRegex
                .Matches(template)
                .Select(m => int.Parse(m.Groups[1].Value))
                .DefaultIfEmpty(-1)
                .Max() + 1;
        }

        public ExternalApiError ToException(params object[] args)
        {
            object[] usedArgs = Array.Empty<object>();

            if (expectedArgsCount > 0)
            {
                usedArgs = new object[expectedArgsCount];
                for (int i = 0; i < expectedArgsCount; i++)
                {
                    usedArgs[i] = i < args.Length ? args[i] : "";
                }
            }

            string message;
            try
            {
                message = string.Format(Template, usedArgs);
            }
            catch
            {
                message = Template;
            }

            return new ExternalApiError(StatusCode, Title, message, usedArgs);
        }
    }
}
