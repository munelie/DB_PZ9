namespace LibraryAPI.DTO
{
    /// <summary>
    /// Ответ API
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Error(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message
            };
        }
    }

    /// <summary>
    /// Ответ АПИ без данных
    /// </summary>
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public static ApiResponse Ok(string message = "успех")
        {
            return new ApiResponse
            {
                Success = true,
                Message = message
            };
        }

        public static ApiResponse Error(string message)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message
            };
        }
    }
}
