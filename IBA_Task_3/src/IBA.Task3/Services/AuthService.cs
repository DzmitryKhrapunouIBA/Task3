namespace IBA.Task3
{
    /// <summary>
    /// Базовые сервисные функции аутентификации
    /// </summary>
    public static class AuthService
    {
        private const string _salt = "$2a$11$iOlBDNEkOb9n3D9F1PO3N.";

        /// <summary>
        /// Получить хэш.
        /// </summary>
        /// <param name="password">Пароль.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Если пароль null или empty</exception>
        public static string GetHash(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new System.ArgumentNullException(nameof(password));
            
            password = password.Trim().ToLower();

            return BCrypt.Net.BCrypt.HashPassword(password, _salt, false, BCrypt.Net.HashType.SHA512);
        }
    }
}