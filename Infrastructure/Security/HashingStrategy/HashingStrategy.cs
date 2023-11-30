namespace Infrastructure.Security.HashingStrategy;

public abstract class HashingStrategy : IHashingStrategy
{
    public abstract string HashPassword(string password);
    public abstract bool VerifyPassword(string providedPassword, string hashedPassword);
}
