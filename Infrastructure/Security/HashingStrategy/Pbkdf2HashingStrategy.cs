using System;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Security.HashingStrategy;

public class Pbkdf2HashingStrategy : HashingStrategy
{

	public override string HashPassword(string password)
	{
		var salt = new byte[16];

		var rng = new Random();

		rng.NextBytes(salt);

		var saltString = Convert.ToBase64String(salt);

		using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 20000, HashAlgorithmName.SHA256);
		var hashedBytes = pbkdf2.GetBytes(32);
		var hashedPassword = Convert.ToBase64String(hashedBytes);

		return $"{saltString}:{hashedPassword}";
	}

	public override bool VerifyPassword(string providedPassword, string hashedAndSaltedPassword)
	{
		var parts = hashedAndSaltedPassword.Split(':');
		if (parts.Length != 2)
		{
			return false; 
		}

		var saltString = parts[0];
		var hashedPassword = parts[1];

		var salt = Convert.FromBase64String(saltString);

		using var pbkdf2 = new Rfc2898DeriveBytes(providedPassword, salt, 20000, HashAlgorithmName.SHA256);
		var hashedBytes = pbkdf2.GetBytes(32);
		var enteredPasswordHash = Convert.ToBase64String(hashedBytes);

		return string.Equals(enteredPasswordHash, hashedPassword);
	}
}