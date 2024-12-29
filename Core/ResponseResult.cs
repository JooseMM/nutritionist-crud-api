namespace AppointmentsAPI.Core;

public class ResponseResult<T>
{
    public bool IsSuccess {get;set;}
    public T? Value {get;set;}
    public string? Error {get;set;}
    public DateTime DateTime {get;set;}
    public int StatusCode {get;set;}

    public static ResponseResult<T> Success(T value, int statusCode) => new ResponseResult<T>
    {
        IsSuccess = true,
        Value = value,
	DateTime = DateTime.UtcNow,
	StatusCode = statusCode,
    };
    public static ResponseResult<T> Failure(string error, int statusCode) => new ResponseResult<T>
    {
        IsSuccess = false,
	StatusCode = statusCode,
        Error = error,
	DateTime = DateTime.UtcNow,
    };
}
