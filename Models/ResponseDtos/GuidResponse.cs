namespace AppointmentsAPI.Models.ResponseDtos;

public class GuidResponse
{
    Guid Id {get;set;}

    public GuidResponse(Guid id)
    {
        Id = id;
    }

}
