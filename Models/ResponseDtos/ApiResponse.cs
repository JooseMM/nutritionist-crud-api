namespace AppointmentsAPI.Models.ResponseDtos;

public class ApiResponse<T>
{
    public T? Data {get;set;}
    public bool isSuccess {get;set;}
    public DateTime DateTime {get;set;}
    public string? Errors {get;set;}
}
