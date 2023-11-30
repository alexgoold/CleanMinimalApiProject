namespace Infrastructure.Security.HashingStrategy;

public interface IHashingStrategy
{
    string HashPassword(string password);
    bool VerifyPassword(string providedPassword, string hashedPassword);
}