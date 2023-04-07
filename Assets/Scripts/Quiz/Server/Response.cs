namespace ShapesGame.Quiz.Server
{
    public readonly struct Response
    {
        public readonly bool IsSuccess;
        public readonly string ErrorMessage;
        public readonly string Result;

        public Response(bool isSuccess, string errorMessage = default, string result = default)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Result = result;
        }
    }
}