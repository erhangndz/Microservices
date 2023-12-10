using System.Runtime.Serialization;

namespace FreeCourse.Web.Exceptions
{
    public class UnAuthorizeException : Exception
    {
        public UnAuthorizeException()
        {
        }

        public UnAuthorizeException(string? message) : base(message)
        {
        }

        public UnAuthorizeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnAuthorizeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
