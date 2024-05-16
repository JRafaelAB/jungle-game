using System.Security.Cryptography;
using System.Text;

namespace Domain.Utils;

public static class Cryptography
{
    private const string PEPPER_ENVIRONMENT_KEY = "PEPPER";
    private const string SEPARATOR_TOKEN = "-";
    private static readonly Random _random = new (Guid.NewGuid().GetHashCode());
    
    public static string GenerateSalt(uint size)
    {
        StringBuilder str_build = new ();
        
        for (int i = 0; i < size; i++)
        {
            bool isNumber = _random.Next() % 2 == 0;
            bool isUpperCase = _random.Next() % 2 != 0;
            double seed = _random.NextDouble();
            char value;
            
            if (isNumber)
            {
                int shift = Convert.ToInt32(Math.Floor(10 * seed));
                value = Convert.ToChar(shift + 48);
            }
            else
            {
                if (isUpperCase)
                {
                    int shift = Convert.ToInt32(Math.Floor(25 * seed));
                    value = Convert.ToChar(shift + 65);
                }
                else
                {
                    int shift = Convert.ToInt32(Math.Floor(25 * seed));
                    value = Convert.ToChar(shift + 97);
                }
            }
            str_build.Append(value);
        }

        return str_build.ToString();
    }

    public static string EncryptPassword(string password, string salt)
    {
        password.ValidateStringArgumentNotNullOrEmpty(nameof(password));
        
        return Encrypt(password + EncryptSalt(salt));
    }   
    
    private static string EncryptSalt(string salt)
    {
        salt.ValidateStringArgumentNotNullOrEmpty(nameof(salt));
        string pepper = Environment.GetEnvironmentVariable(PEPPER_ENVIRONMENT_KEY) ?? string.Empty;
        
        return Encrypt(salt + pepper);
    }

    private static string Encrypt(string value)
    {
        byte[] saltBytes = Encoding.UTF8.GetBytes(value);
        byte[] hashBytes = SHA512.HashData(saltBytes);

        return BitConverter.ToString(hashBytes).Replace(SEPARATOR_TOKEN, string.Empty);
    }
}

