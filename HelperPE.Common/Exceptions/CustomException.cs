namespace HelperPE.Common.Exceptions
{
    public class CustomException : Exception
    {
        public int Code { get; set; } = 500;
        public string Error { get; set; } = String.Empty;
        public new string Message { get; set; } = String.Empty;

        public CustomException(int code, string error, string message)
            : base(message)
        {
            Code = code;
            Error = error;
            Message = message;
        }
    }
}
