namespace TaskManager.Service.Helpers
{
    using BCrypt.Net;

    public class PasswordHash
    {
        private static string GetRandomSalt() => BCrypt.GenerateSalt(12);

        public static string HashPassword(string password) => BCrypt.HashPassword(password, GetRandomSalt());

        public static bool ValidatePassword(string password, string correctHash) => BCrypt.Verify(password, correctHash);
    }
}
