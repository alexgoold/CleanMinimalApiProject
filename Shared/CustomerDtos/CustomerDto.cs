namespace Shared.CustomerDtos;

public class CustomerDto
{
    public Guid Id { get; init; }
    public string Email { get; set; }
    public string Password { get; set; }
}