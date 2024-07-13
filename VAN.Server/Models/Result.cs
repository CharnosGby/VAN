namespace VAN.Server.Models
{
    public class Result<T>
    {
        public int Code { get; init; }
        public string Message { get; init; } = default!;
        public T Data { get; init; } = default!;

        public Result() { }

        public Result(int code, string message, T data)
        {
            Code = code;
            Message = message;
            Data = data;
        }

        public enum CM
        {
            SUCCESS_200 = 200,
            ERROR_400 = 400,
            ERROR_404 = 404,
            ERROR_500 = 500,
            ERROR_403 = 403
        }
    }

    // 用于表示空数据
    public struct VoidType { }
}
