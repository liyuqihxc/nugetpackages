using System;

namespace TemplateName.WebApi.Exceptions
{
    public class ExceptionModel
    {
        public ExceptionModel(Exception e) : this(e.Message, e.GetType(), e.Source) {}

        public ExceptionModel(string message, Type exceptionType, string source)
        {
            Message = message;
            ExceptionType = exceptionType.FullName;
            Source = source;
        }

        public string Message { get; set; }

        public string ExceptionType { get; set; }

        public string Source { get; set; }
    }
}
