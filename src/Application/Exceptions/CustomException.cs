namespace Application.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; }
        public string Title { get; }
        public string Type { get; }

        public CustomException(string message, int statusCode = 500, string title = "خطای برنامه", string type = "https://yourdomain.com/errors/app")
            : base(message)
        {
            StatusCode = statusCode;
            Title = title;
            Type = type;
        }
    }
}
