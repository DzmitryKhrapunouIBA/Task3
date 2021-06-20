namespace IBA.Task3
{
    /// <summary>
    /// Base auth service functions
    /// </summary>
    public static class AuthService
    {
        private const string _salt = "$2a$11$iOlBDNEkOb9n3D9F1PO3N.";

        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">If password is null or empty</exception>
        public static string GetHash(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new System.ArgumentNullException(nameof(password));
            
            // trim and change lower variant
            password = password.Trim().ToLower();

            return BCrypt.Net.BCrypt.HashPassword(password, _salt, false, BCrypt.Net.HashType.SHA512);
        }
    }
}