using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClientSamokat.Components
{
    internal class Sha256Converter : IHashProvider
    {
        public string GetHash(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] buffer = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(buffer);
            return Convert.ToBase64String(hash);
        }
    }
}
