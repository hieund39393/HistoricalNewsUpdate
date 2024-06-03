using System;
using System.Globalization;

namespace HistoricalNewsUpdate.Common.Exceptions
{
    public class AppException : Exception
    {
        public const string ErrorCode = "error_code";
        public AppException()
        {

        }
        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture,
            message, args))
        {
        }

        public AppException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public AppException(string message, int code) : base(message)
        {
            Data.Add(ErrorCode, code);
        }
    }
}
