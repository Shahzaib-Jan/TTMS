using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TTMS_OOP.Models;
using TTMS_OOP.DAL;

namespace TTMS_OOP.BLL
{
    public static class AuthManager
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        public static void EnsureDefaultAdmin()
        {
            List<UserCredential> creds = JsonRepository.LoadCredentials();
            if (creds.Count == 0)
            {
                creds.Add(new UserCredential
                {
                    Username = "admin",
                    PasswordHash = HashPassword("admin123"),
                    Role = "Admin"
                });
                JsonRepository.SaveCredentials(creds);
            }
        }

        public static UserCredential ValidateLogin(string username, string password)
        {
            List<UserCredential> creds = JsonRepository.LoadCredentials();
            string hash = HashPassword(password);
            return creds.Find(c =>
                c.Username.ToLower() == username.ToLower() &&
                c.PasswordHash == hash);
        }

        public static void UpdateCredentials(List<UserCredential> creds)
        {
            JsonRepository.SaveCredentials(creds);
        }

        public static List<UserCredential> GetCredentials()
        {
            return JsonRepository.LoadCredentials();
        }
    }
}
