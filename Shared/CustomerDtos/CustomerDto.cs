namespace Shared.CustomerDtos;

public class CustomerDto
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Password { get; set; }
}