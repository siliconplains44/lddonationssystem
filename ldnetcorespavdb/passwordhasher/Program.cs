using System;

using ldvdbbusinesslogic;

namespace passwordhasher
{
    class Program
    {
        static void Main(string[] args)
        {
            var hashedPassword = PasswordHasher.HashPassword(args[0]);
            Console.WriteLine("hashed password -> " + hashedPassword);
        }
    }
}
