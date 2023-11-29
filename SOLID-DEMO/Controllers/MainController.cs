using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Domain;
using Infrastructure.DataContext;
using Shared;
using Shared.OrderDtos;

namespace Server.Controllers;

[ApiController]
[Route("/api")]
public class MainController : ControllerBase
{
    public ShopContext _shopContext;

    public MainController(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    [HttpGet("/customers")]
    public async Task<IActionResult> GetCustomers()
    {
        return Ok(await _shopContext.Customers.ToListAsync());
    }

    [HttpGet("/customers/{email}")]
    public async Task<IActionResult> GetCustomer(string email)
    {
        return Ok(await _shopContext.Customers.FirstOrDefaultAsync(c => c.Email.Equals(email)));
    }

    [HttpPost("/customers/register")]
    public async Task<IActionResult> RegisterUser(Customer customer)
    {
        if (!customer.Email.Contains("@"))
            throw new ValidationException("Email is not an email");
        await _shopContext.AddAsync(customer);
        await _shopContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("/customers/login")]
    public async Task<IActionResult> LoginCustomer(string email, string password)
    {
        var customer = await _shopContext.Customers.FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Password.Equals(password));
        if (customer is not null)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("/customers/delete/{id}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        var customer = await _shopContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
        if (customer is null) return BadRequest();

        _shopContext.Customers.Remove(customer);
        await _shopContext.SaveChangesAsync();
        return Ok();
    }
    
}