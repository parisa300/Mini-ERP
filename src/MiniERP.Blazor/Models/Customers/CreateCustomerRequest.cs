namespace MiniERP.Blazor.Models;

public class CreateCustomerRequest
{
    public string FullName { get; set; } = "";

    public string PhoneNumber { get; set; } = "";

    public string Email { get; set; } = "";

    public string Address { get; set; } = "";
}