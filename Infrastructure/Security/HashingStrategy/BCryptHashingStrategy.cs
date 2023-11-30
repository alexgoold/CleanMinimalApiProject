namespace Infrastructure.Security.HashingStrategy;
using BC = BCrypt.Net.BCrypt;
public class BCryptHashingStrategy : HashingStrategy
{

    public override string HashPassword(string password)
    {
        var salt = BC.GenerateSalt(10);
        return BC.HashPassword(password, salt);
    }

    public override bool VerifyPassword(string providedPassword, string hashedPassword)
    {
        return BC.Verify(providedPassword, hashedPassword);
    }
}