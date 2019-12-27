namespace Rise.WebApp.Mvc.ViewModels
{
    public class ErrorViewModel
    {
        public string ErrorCode { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public ErrorViewModel(string errorCode, string title, string message)
        {
            ErrorCode = errorCode;
            Title = title;
            Message = message;
        }
    }
}
